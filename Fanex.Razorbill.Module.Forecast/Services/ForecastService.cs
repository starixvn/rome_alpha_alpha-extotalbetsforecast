namespace Fanex.Razorbill.Module.Forecast.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Alpha.PartialUM.Helpers;
    using AlphaPlus.PartialUM.Services;
    using Core.Services.Impl;
    using Fanex.Caching;
    using Fanex.Razorbill.Module.Common.Services;
    using Fanex.Razorbill.Module.Core.DataAccess;
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
        private readonly IPartialUMService _partialUMService;

        public ForecastService(
            IForecastRepository forecastRepository = null,
            IPartialUMService partialUMService = null)
        {
            _forecastRepository = forecastRepository ?? new ForecastRepository();
            _partialUMService = partialUMService ?? new PartialUMService(new PartialUMTotalBetsForecastDataAccess(), new PartialUMSectionHelper());
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
                    BetType = GetBetTypeName(bettype.TypeId, true),
                    FollowedBetTypes = bettype.FirstId.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList()
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
                    BetType = GetBetTypeName(bettype.TypeId, true),
                    FollowedBetTypes = bettype.InfoId.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(s => GetBetTypeName(int.Parse(s))).ToList()
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
            var cacheService = CacheFactory.MemoryCacheService;
            var bettypeHash = cacheService.Get<IDictionary<int, bool>>(key);
            if (bettypeHash == null)
            {
                int bettypeId;
                bettypeHash = new Dictionary<int, bool>();

                var rawdata = GetRawScoreMapBetTypes();
                foreach (RawScoreMapBetType bettype in rawdata)
                {
                    bettype.FirstId.Split(new[] { ',' }).ToList().ForEach(item =>
                    {
                        bettypeId = int.Parse(item);
                        if (!bettypeHash.ContainsKey(bettypeId))
                        {
                            bettypeHash.Add(bettypeId, true);
                        }
                    });
                }

                cacheService.Set(key, bettypeHash);
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
            var cacheService = CacheFactory.MemoryCacheService;
            var data = cacheService.Get<IList<RawScoreMapBetType>>(key);
            if (data == null)
            {
                var criteria = new GetScoreMapBetTypeCriteria();

                _partialUMService.ThrowIfUM(criteria.GetSettingKey());

                data = _forecastRepository.GetScoreMapBetTypes(criteria);

                // 2 hours.
                int cacheInHours = 2;
                cacheService.Set(key, data, new CacheItemOptions().SetAbsoluteExpiration(TimeSpan.FromHours(2)));
            }

            return data;
        }

        private static string GetBetTypeName(int id, bool fromResource = false)
        {
            // 1 x 2.
            if (fromResource && id == 5)
            {
                return ForecastLang.BetType1x2;
            }

            return CacheRef.GetBettype(id);
        }
    }
}