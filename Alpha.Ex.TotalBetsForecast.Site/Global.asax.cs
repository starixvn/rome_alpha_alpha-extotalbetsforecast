namespace Alpha.Ex.TotalBetsForecast.Site
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using System.Web.SessionState;
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
    using Fanex.Razorbill.Security.Services.Authentication;
    using Fanex.RemotingRef;
    using ONEbook.UM;
    using ONEbook.UM.AspNetMVC.Filters;
    using ONEbook.UM.Configurations;
    using ONEbook.UM.Helpers;
    using ONEbook.UM.NETFramework;
    using ONEbook.UM.Services;
    using ONEbook.UM.Transport;
    using Sunplus.Agent.Model.Entities;

    public class Global : System.Web.HttpApplication
    {
        private readonly string rngArea = "CustomerSetting/Casino";

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
                .UseCacheService(new UMCacheService(CacheService.CreateInstance(CacheType.MemoryCache)))
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
                Server.ClearError();
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
                    Session.Clear();
                    Response.Redirect(MicroSites.OldMainUrl("~/_Authorization/SignIn.aspx"), true);
                }
            }
        }

        private string GetRequestPath()
        {
            var url = Request.Path;
            if (IsRnGCasinoUri)
            {
                url = GetRnGCasinoUri();
            }

            return url.Remove(0, 1).ToLowerInvariant();
        }

        private string GetRnGCasinoUri()
        {
            var controller = Request.RequestContext.RouteData.Values["controller"];
            var action = Request.RequestContext.RouteData.Values["action"];

            return string.Format("/{0}/{1}/{2}", rngArea, controller, action);
        }

        private bool IsRnGCasinoUri
        {
            get
            {
                var url = Request.Path;
                return url.IndexOf(rngArea, StringComparison.OrdinalIgnoreCase) != -1;
            }
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
    }
}