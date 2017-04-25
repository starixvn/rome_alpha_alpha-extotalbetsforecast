namespace Fanex.Razorbill.Module.Common.Services
{
    using System;
    using System.Collections.Generic;

    public interface IReportTimeRangeService
    {
        IDictionary<int, DateTime[]> GetHashReportTimeRange();
    }
}