namespace Fanex.Razorbill.Module.Common.Criteria
{
    using System.Collections;
    using Fanex.NAP.Framework.Data;
    using Fanex.Razorbill.Module.Common.Helpers;

    /// <summary>
    /// Limited Country Access Criteria.
    /// </summary>
    public class LimitedCountryAccessCriteria : CriteriaBase
    {
        public override string GetSettingKey()
        {
            return "Age_CheckValidCountry";
        }

        public override Hashtable GetSpParams()
        {
            return new Hashtable 
            { 
                { "ip", IPHelper.CustomerIp() },
                { "countrycode", string.Empty }
            };
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}