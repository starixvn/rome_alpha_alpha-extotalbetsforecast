namespace Fanex.Razorbill.Module.Common.Repositories.IP
{
    using System.Collections.Generic;
    using System.Data;
    using Fanex.Common;
    using Fanex.Data;
    using Fanex.Razorbill.Module.Common.Entities;

    /// <summary>
    /// IP Repository.
    /// </summary>
    public class IPRepository : IIPRepository
    {
        /// <summary>
        /// Get VN IP.
        /// </summary>
        /// <returns>Return result.</returns>
        public IList<IPRangeEntity> GetVNIPs()
        {
            var ips = new List<IPRangeEntity>();
            using (ObjectDb objDb = new ObjectDb("GetIPVN"))
            {
                IDataReader reader = objDb.ExecuteReader();
                while (reader.Read())
                {
                    ips.Add(new IPRangeEntity()
                    {
                        From = Converter.To<double>(reader["ipfrom"]),
                        To = Converter.To<double>(reader["ipto"]),
                        IsManual = Converter.ToBool(reader["ismanual"])
                    });
                }
            }

            return ips;
        }
    }
}