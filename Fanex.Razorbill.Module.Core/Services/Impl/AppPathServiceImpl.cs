namespace Fanex.Razorbill.Module.Core.Services.Impl
{
    using System.Configuration;
    using System.Text.RegularExpressions;
    using System.Web;

    public class AppPathServiceImpl
    {
        public static string GetAppPath(string appName, string pathModifier)
        {
            var appPath = HttpContext.Current.Response.ApplyAppPathModifier(pathModifier);
            appPath = Regex.Replace(appPath, HttpRuntime.AppDomainAppVirtualPath, appName, RegexOptions.IgnoreCase);
            return appPath;
        }

        public static string GetMainAppPath(string relativePath = "~")
        {
            return GetAppPath(ConfigurationManager.AppSettings["MainAppPath"], relativePath);
        }
    }
}
