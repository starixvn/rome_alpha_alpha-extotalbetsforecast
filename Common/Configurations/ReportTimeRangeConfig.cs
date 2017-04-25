namespace Fanex.Razorbill.Module.Common.Configurations
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using Fanex.Razorbill.Module.Common.Enums;

    /// <summary>
    /// Report time range config.
    /// </summary>
    public static class ReportTimeRangeConfig
    {
        private static readonly IDictionary<string, ReportTimeRangeType> ReportTimeRangeUrls;

        static ReportTimeRangeConfig()
        {
            ReportTimeRangeUrls = InitializeReportTimeRangeUrls();
        }

        public static ReportTimeRangeType GetReportTimeRangeType(string url)
        {
            ReportTimeRangeType timeRange;
            if (!ReportTimeRangeUrls.TryGetValue(url.ToLower(), out timeRange))
            {
                timeRange = ReportTimeRangeType.Unknown;
            }

            return timeRange;
        }

        /// <summary>
        /// Initializes the report time range URLs.
        /// </summary>
        /// <returns>The report url hash.</returns>
        private static IDictionary<string, ReportTimeRangeType> InitializeReportTimeRangeUrls()
        {
            IDictionary<string, ReportTimeRangeType> timeRangeHash = new Dictionary<string, ReportTimeRangeType>();
            string virtualDirectory = ConfigurationManager.AppSettings["VirtualDirectory"];
            virtualDirectory = string.IsNullOrEmpty(virtualDirectory)
                ? string.Empty
                : "/" + virtualDirectory.ToLower();

            string url;
            string[] urlArray;
            UrlElement urlElement;
            foreach (var reportUrl in ReportTimeRangeSection.Instance.ReportUrls)
            {
                urlElement = (UrlElement)reportUrl;
                
                url = urlElement.Url;
                if (!string.IsNullOrEmpty(url))
                {
                    urlArray = url.ToLower().Split(new char[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string item in urlArray)
                    {
                        timeRangeHash.Add(virtualDirectory + item.Trim(), (ReportTimeRangeType)urlElement.ReportType);
                    }
                }
            }

            return timeRangeHash;
        }
    }
}