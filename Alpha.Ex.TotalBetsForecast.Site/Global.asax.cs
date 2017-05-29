using Alpha.Web;
using Alpha.Web.Helpers;
using Fanex.Data.Configuration;
using Fanex.Razorbill.Security.Services.Authentication;
using Sunplus.Agent.Model.Entities;
using Sunplus.Agent.Utilities;
using System;
using System.Configuration;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;

namespace Alpha.Ex.TotalBetsForecast.Site
{
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

            Logs.WriteInfo("Alpha - Ex Total Bets Forecast - Application Start");
        }

        public void Application_End()
        {

            HttpRuntime runtime = (HttpRuntime)typeof(System.Web.HttpRuntime).InvokeMember("_theRuntime",
                System.Reflection.BindingFlags.NonPublic
                | System.Reflection.BindingFlags.Static
                | System.Reflection.BindingFlags.GetField,
                null,
                null,
                null);

            if (runtime == null)
                return;

            string shutDownMessage = (string)runtime.GetType().InvokeMember("_shutDownMessage",
                System.Reflection.BindingFlags.NonPublic
                | System.Reflection.BindingFlags.Instance
                | System.Reflection.BindingFlags.GetField,
                null,
                runtime,
                null);

            string shutDownStack = (string)runtime.GetType().InvokeMember("_shutDownStack",
                System.Reflection.BindingFlags.NonPublic
                | System.Reflection.BindingFlags.Instance
                | System.Reflection.BindingFlags.GetField,
                null,
                runtime,
                null);

            if (!System.Diagnostics.EventLog.SourceExists("Alpha .NET Runtime"))
            {
                System.Diagnostics.EventLog.CreateEventSource("Alpha .NET Runtime", "Application");
            }

            System.Diagnostics.EventLog log = new System.Diagnostics.EventLog();
            log.Source = "Alpha .NET Runtime";
            var logMessage = string.Format("\r\n\r\n_SHUTDOWN_MESSAGE={0}\r\n\r\n_SHUTDOWN_STACK={1}",
                                         shutDownMessage,
                                         shutDownStack);

            Logs.WriteInfo(logMessage);

            log.WriteEntry(logMessage);
        }

        public void Application_Error(object sender, EventArgs e)
        {
            // 1. Don't log 404 error
            Exception ex = Server.GetLastError().GetBaseException();

            if (ex is HttpException)
            {
                if (((HttpException)ex).GetHttpCode() == 404)
                {
                    Response.Redirect("~/Index.aspx");
                }

                return;
            }

            // 2. Don't log during UM time
            if (!CheckUnderMainternanceTime())
            {
                int customerId = 0;
                if (HttpContext.Current.Handler is IRequiresSessionState)
                {
                    customerId = UserSession.Current.CustId;
                }

                Logs.FilterInfoWarnOrError(ex, customerId);
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