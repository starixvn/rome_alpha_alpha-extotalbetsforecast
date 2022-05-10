namespace Alpha.Ex.TotalBetsForecast.Site
{
    using Alpha.Ex.TotalBetsForecast.Site.App_Start;
    using Alpha.Logging;
    using Alpha.Web;
    using Alpha.Web.Exceptions;
    using Alpha.Web.Filters;
    using Alpha.Web.Helpers;
    using Fanex.Caching;
    using Fanex.Data.Configuration;
    using Fanex.Logging.Extensions.Database;
    using Fanex.Logging.Extensions.Web;
    using Fanex.Razorbill.Module.Common.Services;
    using Fanex.Razorbill.Security.Services.Authentication;
    using Fanex.RemotingRef;
    using ONEbook.UM;
    using ONEbook.UM.AspNetMVC.Filters;
    using ONEbook.UM.Configurations;
    using ONEbook.UM.Helpers;
    using ONEbook.UM.NETFramework;
    using ONEbook.UM.Services;
    using ONEbook.UM.Transport;
    using Sunplus.Agent.Model.Auth;
    using Sunplus.Agent.Model.BL.Authorization;
    using Sunplus.Agent.Model.Entities;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.SessionState;

    public class Global : System.Web.HttpApplication
    {      

        protected void Application_Start(object sender, EventArgs e)
        {
            TplCore.TplSettings.UrlVersion = ConfigurationManager.AppSettings["JsVersion"];

            LoggingConfig.Init(getDbExceptionInfo: DatabaseInfoGetters.GetDbExceptionInfo);

            DbSettingProviderManager.Current.Start();
            ShareSession.Init();

            AreaRegistration.RegisterAllAreas();
            RouterConfig.RegisterRoutes(RouteTable.Routes);

            var umConfig = new UMClientConfiguration().Initialize();
            UMClientManager
                .UseConfig(umConfig)
                .UseCacheService(new UMCacheService(new CacheService()))
                .UseRestClient(new UMRestClient(umConfig))
                .UseHttpContext(new HttpContextHelper());
            RegisterGlobalFilters(GlobalFilters.Filters);
            MvcHandler.DisableMvcResponseHeader = true;

            RefDataTypeContainer
                .ConnectTo(new Uri(ConfigurationManager.AppSettings["RemoteRefURL"]))
                .FromApplication("Alpha.Ex.TotalBetsForecast.Site");
        }

        public void Application_Error(object sender, EventArgs e)
        {
            if (!CheckUnderMainternanceTime())
            {
                Exception ex = Server.GetLastError();

                var innerException = ex.InnerException;
                if (innerException != null)
                {
                    var partialUMException = innerException as PartialUMException;
                    if (partialUMException != null)
                    {
                        HttpContext.Current.ClearError();
                        Response.Redirect(MicroSites.MainUrl($"~/PartialUM/Index?umfrom={partialUMException.UMFrom}&umto={partialUMException.UMTo}"));
                        return;
                    }
                }

#pragma warning disable CS0618 // Type or member is obsolete
                var currentCustomer = UserSession.Current;
#pragma warning restore CS0618 // Type or member is obsolete

                ExceptionHandler.Handle(ex,
                    new Dictionary<string, object>
                    {
                        ["CustId"] = currentCustomer?.CustId,
                        ["CustName"] = currentCustomer?.UserName,
                        ["SubAccountName"] = currentCustomer?.SubUserName,
                        ["AdminName"] = currentCustomer?.AdminName
                    });
            }

#pragma warning disable CS0618 // Type or member is obsolete
            if (!Sunplus.Agent.Model.BL.Definitions.AppSettings.LocalTest)
#pragma warning restore CS0618 // Type or member is obsolete
            {
                Server.Transfer("~/Error.aspx");
            }
        }

        void Application_AcquireRequestState(object sender, EventArgs e)
        {
            if (!(HttpContext.Current.Handler is IRequiresSessionState))
            {
                return;
            }

            string executeFileName = Request.AppRelativeCurrentExecutionFilePath;
            if (executeFileName.EndsWith(".aspx"))
            {
                Response.AddHeader("Cache-Control", "no-cache, no-store, must-revalidate");
            }

            var userSession = UserSession.Current;

#pragma warning disable CS0618 // Type or member is obsolete
            if (userSession.CustId == 0)
#pragma warning restore CS0618 // Type or member is obsolete
            {
                Response.Redirect(MicroSites.OldMainUrl("~/_Authorization/SignIn.aspx"), true);
                return;
            }

#pragma warning disable CS0618 // Type or member is obsolete
            var nicknameChecker = new NicknameCheckerImpl(userSession);
#pragma warning restore CS0618 // Type or member is obsolete

            if (nicknameChecker.IsForceLoginByNickname)
            {
                Response.Redirect(MicroSites.MainUrl("~/Nickname/Index"), true);
                return;
            }

            string url = GetRequestPath();

            //---Checking closed status---
            IAuthenticationService authencationService = new AuthenticationService(url);
#pragma warning disable CS0618 // Type or member is obsolete
            if (userSession.CustId != 0 && !string.IsNullOrWhiteSpace(url) &&
#pragma warning restore CS0618 // Type or member is obsolete
#pragma warning disable CS0618 // Type or member is obsolete
                !userSession.IsInternal)
#pragma warning restore CS0618 // Type or member is obsolete
            {
                var closedOrLockedStatus = authencationService.CheckClosedOrLockedStatus();

                if (closedOrLockedStatus.IsClosed || closedOrLockedStatus.IsLocked)
                {
                    TerminateSessionAndSignOut();
                    return;
                }
            }

            if (CheckDuplicationLogin(userSession))
            {
                return;
            }

            if (userSession.IsProtectedMode)
            {
                TerminateSessionAndSignOut();
            }
        }

        private void TerminateSessionAndSignOut()
        {
            Session.Clear();

            if (!Session.IsCookieless)
            {
                FormsAuthentication.SignOut();
            }

            Response.Redirect(MicroSites.OldMainUrl(FormsAuthentication.LoginUrl), true);
        }

        private string GetRequestPath()
        {
            var url = Request.Path;         

            return url.Remove(0, 1).ToLowerInvariant();
        }      

        private static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AfterLoginFlowFilter());
            filters.Add(new PartialUMExceptionFilter(true));
            filters.Add(new CheckUMFilterAttribute(
                new MaintenanceService(UMClientManager.RestClient, UMClientManager.CacheService, UMClientManager.UMClientConfigs, UMClientManager.HttpContext), null));
        }

        private static bool CheckUnderMainternanceTime()
        {
            var isUm = false;
            try
            {
                isUm = Sunplus.Agent.Model.Services.MaintenanceService.IsUnderMaintenance();
            }
            catch (Exception e)
            {
                ExceptionHandler.Handle(e);
            }

            return isUm;
        }

        private bool CheckDuplicationLogin(IUserSession userSession)
        {
            if (userSession.IsInternal
                || "1" == ConfigurationManager.AppSettings["DisabledCheckDuplicationLogin"])
            {
                return false;
            }

            var custId = userSession.CustId;

            if (custId <= 0 || custId == 68287 || custId == 68289 || custId == 1373389 || custId == 1373580 || custId == 1374738 || custId == 4317260 || custId == 2)
            {
                return false;
            }

            var kickoutInfo = GetKickoutInfo(userSession);

            if (kickoutInfo != null && kickoutInfo.IsKickedOut)
            {
                Response.Redirect(MicroSites.MainUrl("~/default/kickout?reason=" + kickoutInfo.KickOutReasonResourceKey), true);
                return true;
            }

            return false;
        }

        private static KickoutInfo GetKickoutInfo(IUserSession userSession)
        {

            var cacheService = CacheFactory.MemoryCacheService;
            var recheckInCacheKey = string.Format(
                "Alpha.Auth.CustContext.RecheckInCacheKey_{0}_{1}_{2}",
                userSession.CustId,
                userSession.SubAccId,
                userSession.OnUserId);
            var kickoutInfo = cacheService.Get<KickoutInfo>(recheckInCacheKey);

            if (kickoutInfo == null)
            {
                var bo = new AuthorizationBO(userSession.NetSessionId);
                kickoutInfo = bo.Recheckin(userSession.OnUserId);

                cacheService.Set(recheckInCacheKey, kickoutInfo, new CacheItemOptions().SetAbsoluteExpiration(DateTime.Now.AddSeconds(30))); 
            }

            return kickoutInfo;
        }
    }
}