namespace Fanex.Razorbill.Module.Common.Helpers
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Web;
    using Fanex.Razorbill.Module.Common.Entities;
    using Fanex.Razorbill.Module.Common.Services.IP;

    /// <summary>
    /// IP Helper.
    /// </summary>
    public class IPHelper
    {
        public static string CustomerIp()
        {
            var ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            return ip.Equals("::1") ? "127.0.0.1" : (string.IsNullOrEmpty(ip) ? "127.0.0.1" : ip.Split(',')[0]);
        }

        /// <summary>
        /// Checking IP valid.
        /// </summary>
        /// <param name="ip">Customer IP.</param>
        /// <returns>Return result.</returns>
        [SuppressMessage(
    "StyleCop.CSharp.StylecopPlus.Rules",
    "SP2100:ElementMustBeginWithUpperCaseLetter",
    Justification = "OK")]
        public static bool IsValidIp(string ip)
        {
            if (string.IsNullOrEmpty(ip))
            {
                return false;
            }

            var regexCharacters =
                @"(?<First>2[0-4]\d|25[0-5]|[01]?\d\d?)\.(?<Second>2[0-4]\d|25[0-5]|[01]?\d\d?)\.(?<Third>2[0-4]\d|25[0-5]|[01]?\d\d?)\.(?<Fourth>2[0-4]\d|25[0-5]|[01]?\d\d?)";
            Regex regex = new Regex(regexCharacters);

            return regex.IsMatch(ip, 0);
        }

        public static double ConvertIpToNumber(string ip)
        {
            double num = 0.0;
            if (IsValidIp(ip))
            {
                string[] strArray = ip.Split(new char[] { '.' });
                num = (((double.Parse(strArray[0]) * 16777216.0) + (double.Parse(strArray[1]) * 65536.0)) + (double.Parse(strArray[2]) * 256.0)) + double.Parse(strArray[3]);
            }

            return num;
        }

        /// <summary>
        /// Checking IP VN.
        /// </summary>
        /// <param name="ip">Customer IP.</param>
        /// <returns>Return result.</returns>
        public static bool IsVnIp(string ip)
        {
            if (string.IsNullOrEmpty(ip))
            {
                return false;
            }

            double ipNumber = ConvertIpToNumber(ip);
            List<IPRangeEntity> ips = (List<IPRangeEntity>)new IPService().GetIPVN();
            if (ips == null)
            {
                return false;
            }

            var ipRange = from q in ips where q.From <= ipNumber && q.To >= ipNumber select q;

            if (ipRange.Any())
            {
                return true;
            }

            return false;
        }

        public static string FormatIp(string ip)
        {
            return string.Format("<a class=\"iplink\" href=\"javascript:OpenIPInfo('{0}');\">{0}</a>", ip);
        }
    }
}