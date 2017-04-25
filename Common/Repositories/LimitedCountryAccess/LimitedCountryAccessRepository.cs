namespace Fanex.Razorbill.Module.Common.Repositories.LimitedCountryAccess
{
    using Fanex.Common;
    using Fanex.Data;
    using Fanex.Razorbill.Module.Common.Criteria;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class LimitedCountryAccessRepository : ILimitedCountryAccessRepository
    {
        /// <summary>
        /// Get country code by IP.
        /// </summary>
        /// <returns>Return result.</returns>
        public string CountryCodeByIp()
        {
            var limitedCountryAccessCriteria = new LimitedCountryAccessCriteria();
            using (ObjectDb objectDb = new ObjectDb(limitedCountryAccessCriteria.GetSettingKey()))
            {
                objectDb.AssignParamValues(limitedCountryAccessCriteria.GetSpParams());

                objectDb.ExecuteNonQuery();

                return Converter.ToString2(objectDb.GetOuputParams()["countrycode"]);
            }
        }
    }
}
