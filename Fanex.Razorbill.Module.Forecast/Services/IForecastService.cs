namespace Fanex.Razorbill.Module.Forecast.Services
{
    using System.Collections.Generic;
    using Fanex.Razorbill.Module.Forecast.Criteria;
    using Fanex.Razorbill.Module.Forecast.Models;

    public interface IForecastService
    {
        IList<ScoreMapLeague> GetScoreMapRunningLeagues(GetScoreMapRunningLeagueCriteria criteria);

        IList<ScoreMapMatch> GetScoreMapMatches(GetScoreMapMatchByLeagueCriteria criteria);

        IList<ScoreMapBetType>[] GetScoreMapBetTypes();

        IList<ScoreMapBetTypeInfo>[] GetScoreMapBetTypeInfo();

        IDictionary<int, bool> GetBetTypeSupportScoreMap();

        IList<ForecastScoreMap> GetScoreMapResults(GetForecastScoreMapCriteria criteria);
    }
}
