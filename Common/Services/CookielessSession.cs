namespace Fanex.Razorbill.Module.Common.Services
{
    using System.Web;
    using Fanex.Razorbill.Module.Common.Enums;

    public static class CookielessSession
    {
        private const string LANGUAGE = "LANGUAGE";
        private const string ADMINID = "ADMINID";
        private const string CUSTID = "CUSTID";
        private const string SUBACCID = "SUBACCID";
        private const string ROLEID = "ROLEID";

        private const string HYPHEN = "_";
        private const string DEFAULTLANGUAGE = "en-US";

        public static string Language
        {
            get
            {
                object value = GetSessionValue(LANGUAGE);
                return (null == value) ? DEFAULTLANGUAGE : value.ToString();
            }
        }

        public static int AdminId
        {
            get
            {
                object value = GetSessionValue(ADMINID);
                return (null == value) ? 0 : (int)value;
            }
        }

        public static int CustomerId
        {
            get
            {
                object value = GetSessionValue(CUSTID);
                return (null == value) ? 0 : (int)value;
            }
        }

        public static int SubAccountId
        {
            get
            {
                object value = GetSessionValue(SUBACCID);
                return (null == value) ? 0 : (int)value;
            }
        }

        public static bool IsInternal
        {
            get
            {
                return AdminId != 0;
            }
        }

        public static bool IsSubAccount
        {
            get
            {
                return SubAccountId != 0;
            }
        }

        public static int RoleId
        {
            get
            {
                object value = GetSessionValue(ROLEID);
                return (null == value) ? 0 : (byte)value;
            }
        }

        public static LoggedInUserRole LoggedInRole
        {
            get
            {
                return (LoggedInUserRole)RoleId;
            }
        }

        private static object GetSessionValue(string key)
        {
            return HttpContext.Current.Session[HttpContext.Current.Session.SessionID + HYPHEN + key];
        }
    }
}