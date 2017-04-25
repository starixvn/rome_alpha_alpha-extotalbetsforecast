namespace Fanex.Razorbill.Module.Forecast.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.Caching;
    using Fanex.Razorbill.Module.Common.Helpers;
    using Fanex.Razorbill.Module.Forecast.Criteria;
    using Fanex.Razorbill.Module.Forecast.Models;
    using Fanex.Razorbill.Module.Forecast.Repositories;
    using Fanex.Razorbill.Module.Forecast.Resources;

    /// <summary>
    /// Forecast service.
    /// </summary>
    public class ForecastService : IForecastService
    {
        private const string BettypeScoreMapKey = "Fanex.Razorbill.Module.Forecast.BettypeScoreMap";
        private const string BettypeSupportScoreMapKey = "Fanex.Razorbill.Module.Forecast.BettypeSupportScoreMap";

        private readonly IForecastRepository _forecastRepository;

        public ForecastService(IForecastRepository forecastRepository = null)
        {
            _forecastRepository = forecastRepository ?? new ForecastRepository();
        }

        public IList<ScoreMapLeague> GetScoreMapRunningLeagues(GetScoreMapRunningLeagueCriteria criteria)
        {
            return _forecastRepository.GetScoreMapRunningLeagues(criteria);
        }

        public IList<ScoreMapMatch> GetScoreMapMatches(GetScoreMapMatchByLeagueCriteria criteria)
        {
            return _forecastRepository.GetScoreMapMatches(criteria);
        }

        /// <summary>
        /// Gets the score map bet types.
        /// </summary>
        /// <returns>
        /// The list of Score Map Bet Type.
        /// </returns>
        public IList<ScoreMapBetType>[] GetScoreMapBetTypes()
        {
            IList<ScoreMapBetType> liveBettypes = new List<ScoreMapBetType>();
            IList<ScoreMapBetType> deadBettypes = new List<ScoreMapBetType>();

            var rawdata = GetRawScoreMapBetTypes();
            foreach (var bettype in rawdata)
            {
                ScoreMapBetType info = new ScoreMapBetType
                {
                    BetTypeId = bettype.TypeId,
                    BetType = GetBetTypeName(bettype.TypeId.ToString(), true),
                    FollowedBetTypes = bettype.FirstId.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList()
                };

                if (bettype.EventType == 1)
                {
                    // Live.
                    liveBettypes.Add(info);
                }
                else
                {
                    // Dead.
                    deadBettypes.Add(info);
                }
            }

            return new[] { liveBettypes, deadBettypes };
        }

        /// <summary>
        /// Gets the score map bet type info.
        /// </summary>
        /// <returns>The array of list Score Map Bet Type information.</returns>
        public IList<ScoreMapBetTypeInfo>[] GetScoreMapBetTypeInfo()
        {
            IList<ScoreMapBetTypeInfo> liveBettypes = new List<ScoreMapBetTypeInfo>();
            IList<ScoreMapBetTypeInfo> deadBettypes = new List<ScoreMapBetTypeInfo>();

            var rawdata = GetRawScoreMapBetTypes();
            foreach (var bettype in rawdata)
            {
                ScoreMapBetTypeInfo info = new ScoreMapBetTypeInfo
                {
                    BetType = GetBetTypeName(bettype.TypeId.ToString(), true),
                    FollowedBetTypes = bettype.InfoId.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(s => GetBetTypeName(s)).ToList()
                };

                if (bettype.EventType == 1)
                {
                    // Live.
                    liveBettypes.Add(info);
                }
                else
                {
                    // Dead.
                    deadBettypes.Add(info);
                }
            }

            return new[] { liveBettypes, deadBettypes };
        }

        /// <summary>
        /// Gets the bet type support score map.
        /// </summary>
        /// <returns>The hash of bet type.</returns>
        public IDictionary<int, bool> GetBetTypeSupportScoreMap()
        {
            string key = BettypeSupportScoreMapKey;
            ICache cache = CacheService.CreateInstance(CacheType.MemoryCache);
            var bettypeHash = cache.Get<IDictionary<int, bool>>(key);
            if (bettypeHash == null)
            {
                int bettypeId;
                bettypeHash = new Dictionary<int, bool>();

                var rawdata = GetRawScoreMapBetTypes();
                foreach (RawScoreMapBetType bettype in rawdata)
                {
                    foreach (string item in bettype.FirstId.Split(new char[] { ',' }))
                    {
                        bettypeId = int.Parse(item);
                        if (!bettypeHash.ContainsKey(bettypeId))
                        {
                            bettypeHash.Add(bettypeId, true);
                        }
                    }
                }

                cache.Set<IDictionary<int, bool>>(key, bettypeHash);
            }

            return bettypeHash;
        }

        public IList<ForecastScoreMap> GetScoreMapResults(GetForecastScoreMapCriteria criteria)
        {
            criteria.HomeEndScore = criteria.HomeEndScore == 0 ? 6 : criteria.HomeEndScore;
            criteria.AwayEndScore = criteria.AwayEndScore == 0 ? 6 : criteria.AwayEndScore;

            return _forecastRepository.GetScoreMapResults(criteria);
        }

        /// <summary>
        /// Gets the raw score map bet types.
        /// </summary>
        /// <returns>The list of Raw Score Map Bet Type.</returns>
        private IList<RawScoreMapBetType> GetRawScoreMapBetTypes()
        {
            string key = BettypeScoreMapKey;
            ICache cache = CacheService.CreateInstance(CacheType.MemoryCache);
            var data = cache.Get<IList<RawScoreMapBetType>>(key);
            if (data == null)
            {
                data = _forecastRepository.GetScoreMapBetTypes(new GetScoreMapBetTypeCriteria());

                // 2 hours.
                int cacheInHours = 2;
                cache.Set<IList<RawScoreMapBetType>>(key, data, TimeSpan.FromHours(cacheInHours));
            }

            return data;
        }

        private string GetBetTypeName(string id, bool fromResource = false)
        {
            // 1 x 2.
            if (fromResource && id == "5")
            {
                return ForecastLang.BetType1x2;    
            }

            return CacheRemotingRef.GetBettype(id);
        }
    }
}
