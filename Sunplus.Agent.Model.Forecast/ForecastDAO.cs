namespace Sunplus.Agent.Model.DL.Forecast
{
    using System;
    using System.Collections;
    using System.Data;
    using Sunplus.Agent.Model.DL.Definitions;

    [Obsolete("Should rename to Sunplus.Agent.Model.Forecast")]
    public class ForecastDAO : Sunplus.Agent.Model.DL.Base.DAOBase
    {
        private static string connection = DbTypes.ARCHIVE;

        public static string Connection
        {
            get { return ForecastDAO.connection; }
            set { ForecastDAO.connection = value; }
        }

        #region TopOddsByMatch

        /// <summary>
        /// Get the most odds of match
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="betType"></param>
        /// <param name="langKey"></param>
        /// <returns></returns>
        public DataTable Age_Forecast_OddsSelTopByMatch(int matchId, int betType, string langKey)
        {
            var parameters = new Hashtable
            {
                { "matchid", matchId },
                { "bettype", betType },
                { "language", langKey }
            };

            return ExecSpTable("Age_Forecast_OddsSelTopByMatch", parameters);
        }

        #endregion TopOddsByMatch

        #region Handicap

        /// <summary>
        /// Forecast for handicap
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="custId"></param>
        /// <param name="roleId"></param>
        /// <param name="betType"></param>
        /// <returns></returns>
        public DataTable Age_Forecast_SelHDP(int matchId, int custId, int roleId, int betType)
        {
            var parameters = new Hashtable
            {
                { "matchid", matchId },
                { "agentid", custId },
                { "roleid", roleId },
                { "bettype", betType }
            };

            return ExecSpTable("Age_Forecast_SelHDP", parameters);
        }

        #endregion Handicap

        #region OverUnder

        /// <summary>
        /// Forecast for Over under
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="custId"></param>
        /// <param name="roleId"></param>
        /// <param name="betType"></param>
        /// <returns></returns>
        public DataTable Age_Forecast_SelOU(int matchId, int custId, int roleId, int betType)
        {
            var parameters = new Hashtable
            {
                { "matchid", matchId },
                { "agentid", custId },
                { "roleid", roleId },
                { "bettype", betType }
            };
            return ExecSpTable("Age_Forecast_SelOU", parameters);
        }

        #endregion OverUnder

        #region NextGoal

        /// <summary>
        /// Forecast for next goal
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="custId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public DataTable Age_Forecast_Sel22(int matchId, int custId, int roleId)
        {
            var parameters = new Hashtable
            {
                { "matchid", matchId },
                { "custid", custId },
                { "roleid", roleId }
            };
            return ExecSpTable("Age_Forecast_Sel22", parameters);
        }

        #endregion NextGoal

        #region Running League

        /// <summary>
        /// Get running league for agent
        /// </summary>
        /// <param name="agentId"></param>
        /// <param name="betType"></param>
        /// <returns></returns>
        public DataTable Age_LeagueSelRunning_Ag(int agentId, int betType)
        {
            var parameters = new Hashtable
            {
                { "agid", agentId },
                { "bettypes", betType }
            };

            return ExecSpTable("Age_LeagueSelRunning_Ag", parameters);
        }

        /// <summary>
        /// Get running leagues for master
        /// </summary>
        /// <param name="masterId"></param>
        /// <param name="betType"></param>
        /// <returns></returns>
        public DataTable Age_LeagueSelRunning_MAg(int masterId, int betType)
        {
            var parameters = new Hashtable
            {
                { "magid", masterId },
                { "bettypes", betType }
            };

            return ExecSpTable("Age_LeagueSelRunning_MAg", parameters);
        }

        /// <summary>
        /// Get running leagues for super
        /// </summary>
        /// <param name="superId"></param>
        /// <param name="betType"></param>
        /// <returns></returns>
        public DataTable Age_LeagueSelRunning_SAg(int superId, int betType)
        {
            var parameters = new Hashtable
            {
                { "sagid", superId },
                { "bettypes", betType }
            };

            return ExecSpTable("Age_LeagueSelRunning_SAg", parameters);
        }

        #endregion Running League

        #region League

        /// <summary>
        /// Forecast league for agent
        /// </summary>
        /// <param name="leagueId"></param>
        /// <param name="agentId"></param>
        /// <param name="langKey"></param>
        /// <returns></returns>
        public DataTable Age_Forecast_SelByLeague_Ag(int leagueId, int agentId, string langKey)
        {
            var parameters = new Hashtable
            {
                { "language", langKey },
                { "baseoffsethdp", 0 },
                { "baseoffsetou", 0 },
                { "baseoffseththdp", 0 },
                { "baseoffsethtou", 0 },
                { "leagueid", leagueId },
                { "recommend", agentId }
            };

            return ExecSpTable("Age_Forecast_SelByLeague_Ag", parameters);
        }

        /// <summary>
        /// Forecast league for master
        /// </summary>
        /// <param name="leagueId"></param>
        /// <param name="masterId"></param>
        /// <param name="langKey"></param>
        /// <returns></returns>
        public DataTable Age_Forecast_SelByLeague_MAg(int leagueId, int masterId, string langKey)
        {
            var parameters = new Hashtable
            {
                { "language", langKey },
                { "baseoffsethdp", 0 },
                { "baseoffsetou", 0 },
                { "baseoffseththdp", 0 },
                { "baseoffsethtou", 0 },
                { "leagueid", leagueId },
                { "mrecommend", masterId }
            };

            return ExecSpTable("Age_Forecast_SelByLeague_MAg", parameters);
        }

        /// <summary>
        /// Forecast league for super
        /// </summary>
        /// <param name="leagueId"></param>
        /// <param name="superId"></param>
        /// <param name="langKey"></param>
        /// <returns></returns>
        public DataTable Age_Forecast_SelByLeague_SAg(int leagueId, int superId, string langKey)
        {
            var parameters = new Hashtable
            {
                { "language", langKey },
                { "baseoffsethdp", 0 },
                { "baseoffsetou", 0 },
                { "baseoffseththdp", 0 },
                { "baseoffsethtou", 0 },
                { "leagueid", leagueId },
                { "srecommend", superId }
            };

            return ExecSpTable("Age_Forecast_SelByLeague_SAg", parameters);
        }

        #endregion League

        #region 1x2ByLeague

        /// <summary>
        /// Forecast 1X2 league for agent
        /// </summary>
        /// <param name="leagueId"></param>
        /// <param name="agentId"></param>
        /// <returns></returns>
        public DataTable Age_Forecast1x2_SelByLeague_Ag(int leagueId, int agentId)
        {
            var parameters = new Hashtable
            {
                { "leagueid", leagueId },
                { "custid", agentId }
            };

            return ExecSpTable("Age_Forecast1x2_SelByLeague_Ag", parameters);
        }

        /// <summary>
        /// Forecast 1X2 by league for master
        /// </summary>
        /// <param name="leagueId"></param>
        /// <param name="masterId"></param>
        /// <returns></returns>
        public DataTable Age_Forecast1x2_SelByLeague_MAg(int leagueId, int masterId)
        {
            var parameters = new Hashtable
            {
                { "leagueid", leagueId },
                { "custid", masterId }
            };

            return ExecSpTable("Age_Forecast1x2_SelByLeague_MAg", parameters);
        }

        /// <summary>
        /// Forecast 1X2 by league for super
        /// </summary>
        /// <param name="leagueId"></param>
        /// <param name="superId"></param>
        /// <returns></returns>
        public DataTable Age_Forecast1x2_SelByLeague_SAg(int leagueId, int superId)
        {
            var parameters = new Hashtable
            {
                { "leagueid", leagueId },
                { "custid", superId }
            };

            return ExecSpTable("Age_Forecast1x2_SelByLeague_SAg", parameters);
        }

        #endregion 1x2ByLeague

        #region 1X2ByMatch

        /// <summary>
        /// Forecast 1X2 by match.
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="custId"></param>
        /// <param name="roleId"></param>
        /// <param name="betType"></param>
        /// <returns></returns>
        public DataTable Age_Forecast_Sel1X2(int matchId, int custId, int roleId, int betType)
        {
            var parameters = new Hashtable
            {
                { "matchid", matchId },
                { "custid", custId },
                { "bettype", betType },
                { "roleid", roleId }
            };

            return ExecSpTable("Age_Forecast_Sel1X2", parameters);
        }

        #endregion 1X2ByMatch

        #region For Score Map

        /// <summary>
        /// Get Score Map League
        /// </summary>
        /// <returns></returns>
        public DataTable Age_ScoreMap_SelLeague(int agentId)
        {
            var parameters = new Hashtable { { "agentid", agentId } };

            return ExecSpTable("Age_ScoreMap_SelLeague", parameters);
        }

        /// <summary>
        /// Get Match By League
        /// </summary>
        /// <returns></returns>
        public DataTable Age_ScoreMap_SelMatch(int agentId, int leagueId)
        {
            var parameters = new Hashtable
            {
                { "agentid", agentId },
                { "leagueid", leagueId }
            };

            return ExecSpTable("Age_ScoreMap_SelMatch", parameters);
        }

        /// <summary>
        /// Bet Type Score Map
        /// </summary>
        /// <returns></returns>
        public DataTable Age_ScoreMap_SelBettype()
        {
            return ExecSpTable("Age_ScoreMap_SelBettype", null);
        }

        /// <summary>
        /// Get Score Map Result for Agent
        /// </summary>
        /// <returns></returns>
        public DataSet Age_ScoreMap_Result_Ag(int customerId, string matchIds, string bettypes_live, string bettypes_dead)
        {
            var parameters = new Hashtable
            {
                { "recommend", customerId },
                { "matchids", matchIds },
                { "bettypesLive", bettypes_live },
                { "bettypesDead", bettypes_dead }
            };

            return ExecSpDataset("Age_ScoreMap_Result_Ag", parameters);
        }

        /// <summary>
        /// Get Score Map Result for Master
        /// </summary>
        /// <returns></returns>
        public DataSet Age_ScoreMap_Result_MAg(int customerId, string matchIds, string bettypesLive, string bettypesDead)
        {
            var parameters = new Hashtable
            {
                { "mrecommend", customerId },
                { "matchids", matchIds },
                { "bettypesLive", bettypesLive },
                { "bettypesDead", bettypesDead }
            };

            return ExecSpDataset("Age_ScoreMap_Result_MAg", parameters);
        }

        /// <summary>
        /// Get Score Map Result for Supper
        /// </summary>
        /// <returns></returns>
        public DataSet Age_ScoreMap_Result_SAg(int customerId, string matchIds, string bettypesLive, string bettypesDead)
        {
            var parameters = new Hashtable
            {
                { "srecommend", customerId },
                { "matchids", matchIds },
                { "bettypesLive", bettypesLive },
                { "bettypesDead", bettypesDead }
            };

            return ExecSpDataset("Age_ScoreMap_Result_SAg", parameters);
        }

        #endregion For Score Map
    }
}