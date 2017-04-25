namespace Fanex.Razorbill.Module.Common.Repositories
{
    using System;
    using System.Collections.Generic;
    using Fanex.Common;
    using Fanex.Data;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Report time range repository.
    /// </summary>
    public class ReportTimeRangeRepository : IReportTimeRangeRepository
    {
        /// <summary>
        /// Gets the date time constraint.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>
        /// The ReportTimeRange hash.
        /// </returns>
        public IDictionary<int, DateTime[]> GetReportTimeRange(ICriteria criteria)
        {
            int rptType;
            DateTime[] datetimes;
            IDictionary<int, DateTime[]> reportTimeRangeHash = new Dictionary<int, DateTime[]>();

            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = db.ExecuteReader(criteria.GetSpParams()))
                {
                    while (reader.Read())
                    {
                        rptType = Converter.ToInt(reader["RptType"]);
                        if (!reportTimeRangeHash.TryGetValue(rptType, out datetimes))
                        {
                            datetimes = new DateTime[2];
                            reportTimeRangeHash.Add(rptType, datetimes);
                        }

                        datetimes[0] = reader["StartDate"] == DBNull.Value ? DateTime.MinValue : Converter.ToDateTime(reader["StartDate"]);
                        datetimes[1] = reader["EndDate"] == DBNull.Value ? DateTime.MaxValue : Converter.ToDateTime(reader["EndDate"]);
                    }
                }
            }

            return reportTimeRangeHash;
        }
    }
}