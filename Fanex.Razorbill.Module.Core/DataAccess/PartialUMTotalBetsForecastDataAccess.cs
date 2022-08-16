namespace Fanex.Razorbill.Module.Core.DataAccess
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using Alpha.PartialUM.DataAccess;
    using Alpha.PartialUM.Models;
    using Fanex.Data;

    public class PartialUMTotalBetsForecastDataAccess : IPartialUMDataAccess
    {
        public IEnumerable<UMInfo> GetUMInfoList(string market)
        {
            var inputParams = new Hashtable
            {
                ["marketname"] = market
            };

            using (var db = new ObjectDb("Acc_CheckUM_ByReport"))
            {
                using (var reader = db.ExecuteReader(inputParams))
                {
                    while (reader.Read())
                    {
                        yield return ToUMInfo(reader);
                    }
                }
            }
        }

        private static UMInfo ToUMInfo(IDataReader reader)
            => new UMInfo(
                reader["FunctionTypeName"].ToString(),
                Convert.ToDateTime(reader["BeginTime"]),
                Convert.ToDateTime(reader["EndTime"]));
    }
}
