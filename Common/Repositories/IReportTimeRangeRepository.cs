namespace Fanex.Razorbill.Module.Common.Repositories
{
    using System;
    using System.Collections.Generic;
    using Fanex.NAP.Framework.Data;

    public interface IReportTimeRangeRepository
    {
        IDictionary<int, DateTime[]> GetReportTimeRange(ICriteria criteria);
    }
}