namespace Fanex.Razorbill.Module.Core.Web
{
    using AlphaPlus.User.Models;

    public static class DarkModeHelper
    {
        public static string GetDarkModeName(IUserSession userSession = null)
            => (userSession ?? UserSession.Current).DarkModeName;
    }
}