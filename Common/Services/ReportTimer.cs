namespace Fanex.Razorbill.Module.Common.Services
{
    using System;
    using System.Collections.Generic;
    using Fanex.Caching;
    using Fanex.Razorbill.Module.Common.Configurations;
    using Fanex.Razorbill.Module.Common.Enums;

    /// <summary>
    /// Report time helper.
    /// </summary>
    public static class ReportTimer
    {
        private const int CacheMinutes = 30;
        private const string ReportTimeRangeKey = "__ReportTimeRange__";

        private static ICache Cache
        {
            get
            {
                return CacheService.CreateInstance(CacheType.MemoryCache);
            }
        }

        private static DateTime CacheTime
        {
            get
            {
                return DateTime.Now.AddMinutes(CacheMinutes);
            }
        }

        private static IReportTimeRangeService ReportTimeRangeService
        {
            get
            {
                return new ReportTimeRangeService();
            }
        }

        private static DateTime[] DefaultDateRange
        {
            get
            {
                DateTime currentDate = DateTime.Today;
                return new DateTime[] { currentDate, currentDate };
            }
        }

        /// <summary>
        /// Gets the report time range.
        /// </summary>
        /// <param name="reportType">Type of the report.</param>
        /// <returns>The array of date.</returns>
        public static DateTime[] GetReportTimeRange(ReportTimeRangeType reportType)
        {
            if (reportType == ReportTimeRangeType.Unknown)
            {
                ////throw new Exception("ReportTimeRangeType is NOT in a valid type. Type = " + reportType.ToString());
                return DefaultDateRange;
            }

            ICache cache = Cache;
            string key = ReportTimeRangeKey;
            var reportTimeRangeHash = cache.Get<IDictionary<int, DateTime[]>>(key);
            if (reportTimeRangeHash == null)
            {
                reportTimeRangeHash = ReportTimeRangeService.GetHashReportTimeRange();
                cache.Set<IDictionary<int, DateTime[]>>(key, reportTimeRangeHash, CacheTime);
            }

            return reportTimeRangeHash[(int)reportType];
        }

        /// <summary>
        /// Gets the report time range.
        /// </summary>
        /// <param name="url">The URL string.</param>
        /// <param name="isHistory">If set to <c>true</c> [is history].</param>
        /// <returns>The array of date.</returns>
        public static DateTime[] GetReportTimeRange(string url, bool isHistory = false)
        {
            if (isHistory)
            {
                url = url.Replace(".aspx", string.Empty) + "History";
            }

            ReportTimeRangeType timeRange = ReportTimeRangeConfig.GetReportTimeRangeType(url);
            return GetReportTimeRange(timeRange);
        }
    }
}