namespace Alpha.Ex.TotalBetsForecast.Site
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using System.Web.SessionState;
    using Alpha.Web;
    using Alpha.Web.Helpers;
    using Fanex.Data.Configuration;
    using Fanex.Logging.Extensions.Database;
    using Fanex.Logging.Extensions.Web;
    using Fanex.Razorbill.Security.Services.Authentication;
    using Fanex.RemotingRef;
    using Sunplus.Agent.Model.Entities;

    public class Global : System.Web.HttpApplication
    {
        private readonly string rngArea = "CustomerSetting/Casino";

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            TplCore.TplSettings.UrlVersion = ConfigurationManager.AppSettings["JsVersion"];

            DbSettingProviderManager.Current.Start();
            ShareSession.Init();

            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);
            RegisterGlobalFilters(GlobalFilters.Filters);
            MvcHandler.DisableMvcResponseHeader = true;

            var logConfig = new LoggingConfiguration().UseGetDbExceptionInfo(DatabaseInfoGetters.GetDbExceptionInfo);
            ExceptionHandler.UseConfig(logConfig);

            RefDataTypeContainer
                .ConnectTo(new Uri(ConfigurationManager.AppSettings["RemoteRefURL"]))
                .FromApplication("Alpha.Ex.TotalBetsForecast.Site");
        }

        public void Application_Error(object sender, EventArgs e)
        {
            if (!CheckUnderMainternanceTime())
            {
                Exception ex = Server.GetLastError();
                var currentCustomer = UserSession.Current;

                ExceptionHandler.Handle(ex,
                    new Dictionary<string, object>
                    {
                        ["CustId"] = currentCustomer?.CustId,
                        ["CustName"] = currentCustomer?.UserName,
                        ["SubAccountName"] = currentCustomer?.SubUserName,
                        ["AdminName"] = currentCustomer?.AdminName
                    });
            }

            if (!Sunplus.Agent.Model.BL.Definitions.AppSettings.LocalTest)
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

            if (UserSession.Current.CustId == 0)
            {
                Response.Redirect(MicroSites.OldMainUrl("~/_Authorization/SignIn.aspx"), true);
                return;
            }

            string url = GetRequestPath();

            //---Checking closed status---
            IAuthenticationService authencationService = new AuthenticationService(url);
            if (UserSession.Current.CustId != 0 && !string.IsNullOrWhiteSpace(url) &&
                !UserSession.Current.IsInternal)
            {
                if (authencationService.CheckingClosedStatus())
                {
                    Session.Clear();
                    Response.Redirect(MicroSites.OldMainUrl("~/_Authorization/SignIn.aspx"), true);
                    return;
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

            url = url.Remove(0, 1).ToLower();

            return url;
        }

        private string GetRnGCasinoUri()
        {
            var controller = Request.RequestContext.RouteData.Values["controller"];
            var action = Request.RequestContext.RouteData.Values["action"];

            var uri = string.Format("/{0}/{1}/{2}", rngArea, controller, action);

            return uri;
        }

        private bool IsRnGCasinoUri
        {
            get
            {
                var url = Request.Path.ToString();
                if (url.IndexOf(rngArea, StringComparison.OrdinalIgnoreCase) != -1) { return true; }

                return false;
            }
        }

        private static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        private bool CheckUnderMainternanceTime()
        {
            var isUm = false;
            try
            {
                isUm = Sunplus.Agent.Model.Services.MaintenanceService.IsUnderMaintenance();
            }
            catch
            {
            }

            return isUm;
        }
    }
}