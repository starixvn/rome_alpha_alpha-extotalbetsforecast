namespace Fanex.Razorbill.Module.Forecast.Criteria
{
    using System.Collections;
    using Fanex.NAP.Framework.Data;
    using Fanex.Razorbill.Module.Common.Enums;
    using Fanex.Razorbill.Module.Common.Services;

    /// <summary>
    /// Get Forecast Score Map criteria.
    /// </summary>
    public class GetForecastScoreMapCriteria : CriteriaBase
    {
        private LoggedInUserRole _role;
        private int _customerId;

        public GetForecastScoreMapCriteria()
        {
            _role = CookielessSession.LoggedInRole;
            _customerId = CookielessSession.CustomerId;
        }

        public string MatchIds { get; set; }

        public string Bettypes_Live { get; set; }

        public string Bettypes_Dead { get; set; }

        public int HomeBeginScore { get; set; }

        public int HomeEndScore { get; set; }

        public int AwayBeginScore { get; set; }

        public int AwayEndScore { get; set; }

        public bool IsRefresh { get; set; }

        /// <summary>
        /// Gets the setting key.
        /// </summary>
        /// <returns>The stored procedure key.</returns>
        public override string GetSettingKey()
        {
            if (_role == LoggedInUserRole.Super)
            {
                return "Age_ScoreMap_Result_SAg";
            }
            else if (_role == LoggedInUserRole.Master)
            {
                return "Age_ScoreMap_Result_MAg";
            }
            else if (_role == LoggedInUserRole.Agent)
            {
                return "Age_ScoreMap_Result_Ag";
            }

            return null;
        }

        /// <summary>
        /// Gets the stored procedure parameters.
        /// </summary>
        /// <returns>The input parameters.</returns>
        public override Hashtable GetSpParams()
        {
            Hashtable inputParams = new Hashtable 
            {
                { "matchids",  MatchIds },
                { "bettypes_live",  Bettypes_Live },
                { "bettypes_dead",  Bettypes_Dead },
                { "homeBeginScore",  HomeBeginScore },
                { "homeEndScore",  HomeEndScore },
                { "awayBeginScore",  AwayBeginScore },
                { "awayEndScore",  AwayEndScore },
            };

            if (_role == LoggedInUserRole.Super)
            {
                inputParams.Add("srecommend", _customerId);
            }
            else if (_role == LoggedInUserRole.Master)
            {
                inputParams.Add("mrecommend", _customerId);
            }
            else if (_role == LoggedInUserRole.Agent)
            {
                inputParams.Add("recommend", _customerId);
            }

            return inputParams;
        }
    }
}
