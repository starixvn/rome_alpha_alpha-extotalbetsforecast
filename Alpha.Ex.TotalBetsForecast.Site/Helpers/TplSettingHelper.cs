using System.Web;
using TplNetCore;

namespace Alpha.Ex.TotalBetsForecast.Site.Helpers
{
    public class TplSettingHelper : ITplSettingHelper
    {
        public string PhysicalApplicationPath => HttpContext.Current.Request.PhysicalApplicationPath;

        public string ApplicationPath => HttpContext.Current.Request.ApplicationPath;

        public string ApplyAppPathModifier(string virtualPath) => HttpContext.Current.Response.ApplyAppPathModifier(virtualPath);

        public T GetSession<T>(string key)
        {
            return (T)HttpContext.Current.Session[key];
        }

        public void SetSession<T>(string key, T value)
        {
            HttpContext.Current.Session[key] = value;
        }
    }
}