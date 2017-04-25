namespace Fanex.Razorbill.Module.Forecast.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using Fanex.Common;
    using Fanex.Data;
    using Fanex.Razorbill.Module.Common.Helpers;
    using Fanex.Razorbill.Module.Forecast.Criteria;
    using Fanex.Razorbill.Module.Forecast.Models;

    /// <summary>
    /// Forecast repository.
    /// </summary>
    public class ForecastRepository : IForecastRepository
    {
        /// <summary>
        /// Gets the score map running leagues.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The Score Map League list.</returns>
        public IList<ScoreMapLeague> GetScoreMapRunningLeagues(GetScoreMapRunningLeagueCriteria criteria)
        {
            IList<ScoreMapLeague> leagues = new List<ScoreMapLeague>();
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = db.ExecuteReader(criteria.GetSpParams()))
                {
                    while (reader.Read())
                    {
                        leagues.Add(new ScoreMapLeague(Converter.ToInt(reader["leagueid"]))
                        {
                            SportId = Converter.ToInt(reader["sporttype"])
                        });
                    }
                }
            }

            return leagues;
        }

        /// <summary>
        /// Gets the score map matches.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The Score Map Match list.</returns>
        public IList<ScoreMapMatch> GetScoreMapMatches(GetScoreMapMatchByLeagueCriteria criteria)
        {
            IList<ScoreMapMatch> matches = new List<ScoreMapMatch>();
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = db.ExecuteReader(criteria.GetSpParams()))
                {
                    while (reader.Read())
                    {
                        matches.Add(new ScoreMapMatch(
                            Converter.ToInt(reader["matchid"]),
                            Converter.ToInt(reader["homeid"]),
                            Converter.ToInt(reader["awayid"])));
                    }
                }
            }

            return matches;
        }

        /// <summary>
        /// Gets the score map bet types.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The array of ScoreMapBetType.</returns>
        public IList<RawScoreMapBetType> GetScoreMapBetTypes(GetScoreMapBetTypeCriteria criteria)
        {
            IList<RawScoreMapBetType> rawdata = new List<RawScoreMapBetType>();

            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = db.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        rawdata.Add(new RawScoreMapBetType 
                        {
                            TypeId = Converter.ToInt(reader["TypeId"]),
                            EventType = Converter.ToInt(reader["EvenType"]),
                            FirstId = Converter.ToString2(reader["1stId"]),
                            InfoId = Converter.ToString2(reader["InfoId"]),
                        });
                    }
                }
            }

            return rawdata;
        }

        /// <summary>
        /// Gets the score map results.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The list of Forecast Score Map.</returns>
        [SuppressMessage("StyleCop.CSharp.StylecopPlus.Rules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "OK here.")]
        public IList<ForecastScoreMap> GetScoreMapResults(GetForecastScoreMapCriteria criteria)
        {
            int rowNumber = criteria.HomeEndScore - criteria.HomeBeginScore + 1;
            int columnNumber = criteria.AwayEndScore - criteria.AwayBeginScore + 1;

            ForecastScoreMap scoreMap;
            object haftTime, fullTime;
            int matchId, home, away;
            IDictionary<int, ForecastScoreMap> matchHash = new Dictionary<int, ForecastScoreMap>();
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = db.ExecuteReader(criteria.GetSpParams()))
                {
                    while (reader.Read())
                    {
                        matchId = Converter.ToInt(reader["matchid"]);
                        if (!matchHash.TryGetValue(matchId, out scoreMap))
                        {
                            scoreMap = new ForecastScoreMap(rowNumber, columnNumber) 
                            {
                                MatchId = matchId,
                                HaftTime = new int[rowNumber, columnNumber],
                                FullTime = new int[rowNumber, columnNumber]
                            };

                            matchHash.Add(matchId, scoreMap);    
                        }

                        home = Converter.ToInt(reader["home"]);
                        away = Converter.ToInt(reader["away"]);
                        
                        haftTime = reader["HT"];
                        if (haftTime != DBNull.Value)
                        {
                            scoreMap.HaftTime[away, home] = (int)Math.Round(Converter.ToDecimal(haftTime), MidpointRounding.AwayFromZero);
                        }
                        else
                        {
                            scoreMap.HaftTime = null;
                        }

                        fullTime = reader["FT"];
                        if (fullTime != null)
                        {
                            scoreMap.FullTime[away, home] = (int)Math.Round(Converter.ToDecimal(fullTime), MidpointRounding.AwayFromZero);
                        }
                        else
                        {
                            scoreMap.FullTime = null;
                        }
                    }

                    UpdateHomeAwayTeam(reader, matchHash, rowNumber, columnNumber);
                }
            }

            return matchHash.Values.ToList();
        }

        /// <summary>
        /// Updates the home away team.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="matchHash">The match hash.</param>
        /// <param name="row">The row number.</param>
        /// <param name="column">The column number.</param>
        private void UpdateHomeAwayTeam(IDataReader reader, IDictionary<int, ForecastScoreMap> matchHash, int row, int column)
        {
            if (reader.NextResult())
            {
                int matchId;
                ForecastScoreMap scoreMap;
                while (reader.Read())
                {
                    matchId = Converter.ToInt(reader["matchid"]);
                    if (!matchHash.TryGetValue(matchId, out scoreMap))
                    {
                        scoreMap = new ForecastScoreMap(row, column) { MatchId = matchId };
                        matchHash.Add(matchId, scoreMap);
                    }

                    scoreMap.HomeId = Converter.ToInt(reader["homeid"]);
                    scoreMap.AwayId = Converter.ToInt(reader["awayid"]);
                }
            }
        }
    }
}
