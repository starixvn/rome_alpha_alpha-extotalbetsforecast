using Alpha.Web;
using Fanex.Data.Configuration;
using Sunplus.Agent.Model.Entities;
using Sunplus.Agent.Utilities;
using System;
using System.Web;
using System.Web.SessionState;

namespace Alpha.Ex.TotalBetsForecast.Site
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            DbSettingProviderManager.Current.Start();
            ShareSession.Init();
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

        void Application_Error(object sender, EventArgs e)
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

        bool CheckUnderMainternanceTime()
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