namespace Fanex.Razorbill.Module.Forecast.Criteria
{
    using Fanex.NAP.Framework.Data;
    using Fanex.Razorbill.Module.Common.Services;

    /// <summary>
    /// Get Score Map Running League criteria.
    /// </summary>
    public class GetScoreMapRunningLeagueCriteria : CriteriaBase
    {
        [SpParam(Ignored = true)]
        public int LeagueId { get; set; }

        public int AgentId { get; set; } = CookielessSession.CustomerId;

        public override string GetSettingKey()
        {
            return "Age_ScoreMap_SelLeague";
        }
    }
}
