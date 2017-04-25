namespace Fanex.Razorbill.Module.Forecast.Criteria
{
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Get Score Map Bet type criteria.
    /// </summary>
    public class GetScoreMapBetTypeCriteria : CriteriaBase
    {
        public override string GetSettingKey()
        {
            return "Age_ScoreMap_SelBettype";
        }
    }
}