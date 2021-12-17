namespace Fanex.Razorbill.Module.Core.Web
{
    using System.Configuration;
    using Sunplus.Agent.Model.Entities;

    public static class DarkModeHelper
    {
        public static string GetDarkModeName(IUserSession userSession = null)
            => IsOfferDarkMode ? (userSession ?? UserSession.Current).DarkModeName : string.Empty;

        private static bool IsOfferDarkMode
            => "1" == ConfigurationManager.AppSettings["IsOfferDarkMode"];
    }
}
