namespace Fanex.Razorbill.Module.Forecast.Repositories
{
    using System.Collections.Generic;
    using Fanex.Razorbill.Module.Forecast.Criteria;
    using Fanex.Razorbill.Module.Forecast.Models;

    public interface IForecastRepository
    {
        IList<ScoreMapLeague> GetScoreMapRunningLeagues(GetScoreMapRunningLeagueCriteria criteria);

        IList<ScoreMapMatch> GetScoreMapMatches(GetScoreMapMatchByLeagueCriteria criteria);

        IList<RawScoreMapBetType> GetScoreMapBetTypes(GetScoreMapBetTypeCriteria criteria);

        IList<ForecastScoreMap> GetScoreMapResults(GetForecastScoreMapCriteria criteria);
    }
}
