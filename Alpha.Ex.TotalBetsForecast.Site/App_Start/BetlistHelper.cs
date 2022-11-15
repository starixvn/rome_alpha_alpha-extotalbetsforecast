using Alpha.Web.Helpers;
using Sunplus.Agent.Model.Entities;
using Sunplus.Agent.Utilities;
using System;
using System.Configuration;
using System.Linq;
using System.Web;

public static class BetlistHelper
{
    public static string MapBetlistsDomain(string appPath = "~/")
    {
        var Current = UserSession.Current;
        var cookieNameCurrentPage = GetCookieNameOfCurrentPage(Current.CustId, Current.SubAccId, Encryption.PearsonEncode);
        var cookieless = HttpContext.Current.Session.IsCookieless ? 
            $"/(S({HttpContext.Current.Request.Cookies[$"{cookieNameCurrentPage}-betlist-{HttpContext.Current.Session.SessionID}"].Value}))" : 
            string.Empty;

        return IsStandaloneReport ? $"{BetlistDomainUrl}{cookieless}{appPath?.Replace("~", string.Empty)}" : MicroSites.BetListsUrl(appPath);
    }

    internal static string GetCookieNameOfCurrentPage(
            int custId,
            int subAccId,
            Func<string, string> encodeDataFunction)
    {
        var joinStrings = new[]
        {
                custId.ToString(),
                subAccId.ToString(),
                "NewMain"
            };

        var cacheUrlCookieName = string.Join("_", joinStrings);

        return encodeDataFunction?.Invoke(cacheUrlCookieName) ?? cacheUrlCookieName;
    }

    internal static string BetlistDomainUrl
    {
        get
        {
            var mainDomain = HttpContext.Current.Request.Headers["Host"];
            if (HttpContext.Current.Session.IsCookieless)
            {
                var subDomain = mainDomain.Split('.').ElementAt(0);
                mainDomain = mainDomain.Replace($"{subDomain}.", string.Empty);
            }

            return BetlistDomain.IndexOf("{0}") != -1 ?
                string.Format(BetlistDomain, mainDomain) : BetlistDomain;
        }
    }

    internal static string BetlistDomain => ConfigurationManager.AppSettings["Betlist:Domain"] ?? string.Empty;

    internal static string MainDomain => UserSession.Current.GetProperty<string>("MainDomain");

    internal static bool IsStandaloneReport
            => !string.IsNullOrWhiteSpace(MainDomain) && !string.IsNullOrWhiteSpace(BetlistDomain);
}