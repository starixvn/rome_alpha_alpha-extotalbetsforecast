namespace Fanex.Razorbill.Module.Common.Services
{
    using System;
    using System.Collections.Generic;
    using Fanex.NAP.Framework.Data;
    using Fanex.Razorbill.Module.Common.Criteria;
    using Fanex.Razorbill.Module.Common.Repositories;

    /// <summary>
    /// Report time range service.
    /// </summary>
    public class ReportTimeRangeService : IReportTimeRangeService
    {
        private readonly IReportTimeRangeRepository _reportTimeRangeRepository;

        public ReportTimeRangeService()
            : this(null)
        { 
        }

        public ReportTimeRangeService(IReportTimeRangeRepository reportTimeRangeRepository)
        {
            _reportTimeRangeRepository = reportTimeRangeRepository ?? new ReportTimeRangeRepository();
        }

        /// <summary>
        /// Gets the report time range.
        /// </summary>
        /// <returns>
        /// The report time range hash table.
        /// </returns>
        public IDictionary<int, DateTime[]> GetHashReportTimeRange()
        {
            ICriteria criteria = new ReportTimeRangeCriteria();
            return _reportTimeRangeRepository.GetReportTimeRange(criteria);
        }
    }
}