namespace Fanex.Razorbill.Module.Common.Services.LimitedCountryAccess
{
    using System.Linq;
    using Fanex.Razorbill.Module.Common.Configurations;
    using Fanex.Razorbill.Module.Common.Helpers;
    using Fanex.Razorbill.Module.Common.Repositories.LimitedCountryAccess;

    /// <summary>
    /// Limited Country Access Service.
    /// </summary>
    public class LimitedCountryAccessService : ILimitedCountryAccessService
    {
        private readonly ILimitedCountryAccessRepository _limitedCountryAccessRepository;

        public LimitedCountryAccessService()
            : this(null)
        {
        }

        public LimitedCountryAccessService(ILimitedCountryAccessRepository limitedCountryAccessRepository)
        {
            _limitedCountryAccessRepository = limitedCountryAccessRepository ?? new LimitedCountryAccessRepository();
        }

        /// <summary>
        /// Validate Customer's IP.
        /// </summary>
        /// <returns>Return result.</returns>
        public bool ValidCountryAccess()
        {
            var limitedCountryAccessSection = LimtedCountryAccessSection.Instance.LimitedCountries;
            if (limitedCountryAccessSection == null ||
                limitedCountryAccessSection.Count == 0)
            {
                return false;
            }

            var limitedCountriesActive = from m in limitedCountryAccessSection.Cast<LimitedCountriesElement>().Where(m => m.Active) select m;

            if (!limitedCountriesActive.Any())
            {
                return false;
            }

            var customerCountryCode = from m in limitedCountriesActive
                                      where (m.Name.ToUpper().Equals(_limitedCountryAccessRepository.CountryCodeByIp().ToUpper()) || //// real blocking country ip
                                            (m.TestingIp.Split(',').Contains(IPHelper.CustomerIp()) && //// testing ip
                                                m.IsTesting == true))
                                      select m;
            if (customerCountryCode.Any())
            {
                return true;
            }

            return false;
        }
    }
}