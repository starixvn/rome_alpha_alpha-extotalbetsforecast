namespace Fanex.Razorbill.Module.Core.DataAccess
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using AlphaPlus.PartialUM.DataAccess;
    using AlphaPlus.PartialUM.Models;
    using Fanex.Data;

    public class PartialUMTotalBetsForecastDataAccess : IPartialUMDataAccess
    {
        public IEnumerable<UMInfo> GetUMInfoList(string siteName)
        {
            var inputParams = new Hashtable
            {
                ["sitename"] = siteName
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