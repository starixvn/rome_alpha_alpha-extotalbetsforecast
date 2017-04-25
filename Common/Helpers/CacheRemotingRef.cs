namespace Fanex.Razorbill.Module.Common.Helpers
{
    using System.Threading;
    using Fanex.Common;
    using RemotingRef;

    /// <summary>
    /// Cache remote.
    /// </summary>
    public static class CacheRemotingRef
    {
        public static string CurrentTWLanguage
        {
            get
            {
                switch (Thread.CurrentThread.CurrentCulture.Name)
                {
                    case "en-US": return "en";
                    case "zh-TW": return "ch"; // Traditional
                    case "zh-CN": return "cs"; // Simplified
                    case "ja-JP": return "jp";
                    case "th-TH": return "th"; // Thai
                    case "ko-KR": return "ko"; // Korean
                    case "vi-VN": return "vn"; // Vietnamese
                    default: return "en";
                }
            }
        }

        public static string GetBettype(string bettypeId)
        {
            return RefData.GetBetTypeName(bettypeId, CurrentTWLanguage);
        }

        public static string GetBettype(object bettypeId, object betID)
        {
            string resourceId = string.Empty;
            if (betID != null && betID.ToString().Length > 1)
            {
                resourceId = betID.ToString().Substring(1);
            }

            return RefData.GetBetTypeName(bettypeId.ToString(), CurrentTWLanguage, string.Empty, resourceId);
        }

        public static string GetSportName(object sportID)
        {
            return RefData.GetSportName(Converter.ToString2(sportID), CurrentTWLanguage, string.Empty);
        }

        public static string GetResourceDataByRefIdAndResourceId(string refId, string resourceId, string defaultData)
        {
            return RefData.GetResourceDataByRefIdAndResourceId(refId, resourceId, CurrentTWLanguage, defaultData);
        }

        public static string[] GetCombinationTypeCombo(string betTeam, string betCheck, int hdp1, int hdp2)
        {
            return RefData.GetCombinationTypeCombo(betTeam, betCheck, hdp1, hdp2, CurrentTWLanguage);
        }

        public static string GetProductName(int productId)
        {
            return RefData.GetResourceDataByRefIdAndResourceId("Product", productId.ToString(), CurrentTWLanguage, string.Empty);
        }
    }
}