namespace Sunplus.Agent.Model.BL.Forecast
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Fanex.Caching;
    using Sunplus.Agent.Model.BL.Cache;
    using Sunplus.Agent.Model.DL.Forecast;

    [Obsolete("Should rename to Sunplus.Agent.Model.Forecast")]
    public class ForecastBO : Sunplus.Agent.Model.BL.Base.BOBase
    {
        #region Variable

        private const string BetTypeScoreMapKey = "BETTYPESCOREMAP";
        private const string BetTypeListScoreMapKey = "BETTYPESLISTCOREMAP";
        private static readonly ForecastDAO ForecastDataAccess = new ForecastDAO();

        #endregion Variable

        public ForecastBO(string netSessionId)
            : base(netSessionId)
        {
        }

        #region TopOddsByMatch

        /// <summary>
        /// Get the most odds of match
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="betType"></param>
        /// <returns></returns>
        public DataTable GetFCTopOddsByMatch(int matchId, int betType)
        {
            string language = CurrentSession.LangKey;
            return ForecastDataAccess.Age_Forecast_OddsSelTopByMatch(matchId, betType, language);
        }

        #endregion TopOddsByMatch

        #region Handicap

        /// <summary>
        /// Forecast for handicap
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="betType"></param>
        /// <returns></returns>
        public DataTable GetFCHD(int matchId, int betType)
        {
            // parameter
            int customerId = CurrentSession.CustId;
            int roleId = CurrentSession.RoleId;
            //// execute
            return ForecastDataAccess.Age_Forecast_SelHDP(matchId, customerId, roleId, betType);
        }

        #endregion Handicap

        #region OverUnder

        /// <summary>
        /// Forecast for Over under
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="betType"></param>
        /// <returns></returns>
        public DataTable GetFCOU(int matchId, int betType)
        {
            // parameter
            int customerId = CurrentSession.CustId;
            int roleId = CurrentSession.RoleId;
            //// execute
            return ForecastDataAccess.Age_Forecast_SelOU(matchId, customerId, roleId, betType);
        }

        #endregion OverUnder

        #region NextGoal

        /// <summary>
        /// Forecast for next goal
        /// </summary>
        /// <param name="matchId"></param>
        /// <returns></returns>
        public DataTable GetFCNextGoal(int matchId)
        {
            //// parameter
            int customerId = CurrentSession.CustId;
            int roleId = CurrentSession.RoleId;
            //// execute
            return ForecastDataAccess.Age_Forecast_Sel22(matchId, customerId, roleId);
        }

        #endregion NextGoal

        #region Running Leagues

        /// <summary>
        /// Get running leagues for FC
        /// </summary>
        /// <param name="betType"></param>
        /// <returns></returns>
        public DataTable GetFCRunningLeagues(int betType)
        {
            DataTable runningLeagues = null;
            //// parameter
            int customerId = CurrentSession.CustId;

            //// execute
            if (CurrentSession.IsAgent)
            {
                runningLeagues = ForecastDataAccess.Age_LeagueSelRunning_Ag(customerId, betType);
            }
            else if (CurrentSession.IsMaster)
            {
                runningLeagues = ForecastDataAccess.Age_LeagueSelRunning_MAg(customerId, betType);
            }
            else if (CurrentSession.IsSuper)
            {
                runningLeagues = ForecastDataAccess.Age_LeagueSelRunning_SAg(customerId, betType);
            }
            else
            {
                runningLeagues = new DataTable();
            }

            return runningLeagues;
        }

        #endregion Running Leagues

        #region League

        /// <summary>
        /// Forecast league for agent
        /// </summary>
        /// <param name="leagueId"></param>
        /// <returns></returns>
        public DataTable GetFCByLeague(int leagueId)
        {
            DataTable leagues = null;
            //// parameter
            int customerId = CurrentSession.CustId;
            string language = CurrentSession.LangKey;
            //// execute
            if (CurrentSession.IsAgent)
            {
                leagues = ForecastDataAccess.Age_Forecast_SelByLeague_Ag(leagueId, customerId, language);
            }
            else if (CurrentSession.IsMaster)
            {
                leagues = ForecastDataAccess.Age_Forecast_SelByLeague_MAg(leagueId, customerId, language);
            }
            else if (CurrentSession.IsSuper)
            {
                leagues = ForecastDataAccess.Age_Forecast_SelByLeague_SAg(leagueId, customerId, language);
            }
            else
            {
                leagues = new DataTable();
            }

            return leagues;
        }

        #endregion League

        #region 1x2ByLeague

        /// <summary>
        /// Forecast 1X2 league for agent
        /// </summary>
        /// <param name="leagueId"></param>
        /// <returns></returns>
        public DataTable GetFC1X2ByLeague(int leagueId)
        {
            DataTable leagues = null;

            // parameter
            int customerId = CurrentSession.CustId;

            // execute
            if (CurrentSession.IsAgent)
            {
                leagues = ForecastDataAccess.Age_Forecast1x2_SelByLeague_Ag(leagueId, customerId);
            }
            else if (CurrentSession.IsMaster)
            {
                leagues = ForecastDataAccess.Age_Forecast1x2_SelByLeague_MAg(leagueId, customerId);
            }
            else if (CurrentSession.IsSuper)
            {
                leagues = ForecastDataAccess.Age_Forecast1x2_SelByLeague_SAg(leagueId, customerId);
            }
            else
            {
                leagues = new DataTable();
            }

            return leagues;
        }

        #endregion 1x2ByLeague

        #region 1X2ByMatch

        /// <summary>
        /// Forecast 1X2 by match for agent
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="betType"></param>
        /// <returns></returns>
        public DataTable GetFC1X2ByMatch(int matchId, int betType)
        {
            DataTable matches = null;

            // parameter
            int customerId = CurrentSession.CustId;
            int roleId = CurrentSession.RoleId;

            // execute
            matches = ForecastDataAccess.Age_Forecast_Sel1X2(matchId, customerId, roleId, betType);

            return matches;
        }

        #endregion 1X2ByMatch

        #region Score Map

        #region League in Score Map

        /// <summary>
        /// Forecast league for agent
        /// </summary>
        /// <param name="leagueId"></param>
        /// <returns></returns>
        public DataTable GetLeagueScoreMap()
        {
            DataTable leagues = null;
            int agentId = CurrentSession.CustId;
            leagues = ForecastDataAccess.Age_ScoreMap_SelLeague(agentId);
            if (leagues != null)
            {
                leagues = this.MakeSortLeagueTable(leagues);
            }

            return leagues;
        }

        private DataTable MakeSortLeagueTable(DataTable dt)
        {
            // Create a new DataTable.
            System.Data.DataTable mainTable = new DataTable("mainTable");
            DataColumn column;
            DataRow row;

            // Create first column.
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "leagueid";
            column.ReadOnly = true;
            column.Unique = true;
            mainTable.Columns.Add(column);

            // Create second column.
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "leaguename";
            column.AutoIncrement = false;
            column.Caption = "leaguename";
            column.ReadOnly = false;
            column.Unique = false;
            mainTable.Columns.Add(column);

            int mainId = 0;
            foreach (DataRow dr in dt.Rows)
            {
                row = mainTable.NewRow();
                mainId = Convert.ToInt32(dr["leagueid"]);
                row["leagueid"] = mainId;
                row["leaguename"] = CacheRef.GetLeagueName(mainId);
                mainTable.Rows.Add(row);
            }

            // Sort & Create Table
            DataView dv = mainTable.DefaultView;
            dv.Sort = "leaguename";
            return dv.ToTable();
        }

        #endregion League in Score Map

        #region Get Match By League

        /// <summary>
        /// Get Match By League
        /// </summary>
        /// <param name="leagueId"></param>
        /// <returns></returns>
        public DataTable GetMatchByLeague(int leagueId)
        {
            DataTable matchByLeague = null;
            int agentId = CurrentSession.CustId;
            matchByLeague = ForecastDataAccess.Age_ScoreMap_SelMatch(agentId, leagueId);
            if (matchByLeague != null)
            {
                matchByLeague = this.MakeSortMatchTable(matchByLeague);
            }

            return matchByLeague;
        }

        private DataTable MakeSortMatchTable(DataTable dt)
        {
            // Create a new DataTable.
            System.Data.DataTable mainTable = new DataTable("mainTable");
            DataColumn column;
            DataRow row;

            // Create first column.
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "matchid";
            column.ReadOnly = true;
            column.Unique = true;
            mainTable.Columns.Add(column);

            // Create second column.
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "matchname";
            column.AutoIncrement = false;
            column.Caption = "matchname";
            column.ReadOnly = false;
            column.Unique = false;
            mainTable.Columns.Add(column);

            foreach (DataRow dr in dt.Rows)
            {
                row = mainTable.NewRow();
                row["matchid"] = Convert.ToInt32(dr["matchid"]);
                row["matchname"] = CacheRef.GetTeamName(dr["homeid"]) + "-vs-" + CacheRef.GetTeamName(dr["awayid"]);
                mainTable.Rows.Add(row);
            }

            // Sort & Create Table
            DataView dv = mainTable.DefaultView;
            dv.Sort = "matchname";
            return dv.ToTable();
        }

        #endregion Get Match By League

        #region Bet Type Score Map

        /// <summary>
        /// Bet Type Score Map
        /// </summary>
        /// <returns></returns>
        public DataTable GetBetTypeScoreMap()
        {
            ICache cache = CacheService.CreateInstance(CacheType.MemoryCache);
            object oldData = cache.Get<DataTable>(BetTypeScoreMapKey);
            if (oldData == null)
            {
                DataTable bettypeIds = ForecastDataAccess.Age_ScoreMap_SelBettype();

                // Store in cache
                cache.Set<DataTable>(BetTypeScoreMapKey, bettypeIds, TimeSpan.FromHours(2));

                return bettypeIds;
            }

            return oldData as DataTable;
        }

        public List<int> GetListBetTypeScoreMap()
        {
            ICache cache = CacheService.CreateInstance(CacheType.MemoryCache);
            object oldData = cache.Get<List<int>>(BetTypeListScoreMapKey);
            if (oldData == null)
            {
                string firstId;
                char[] splitChars = new char[] { ',' };

                List<int> bettypeIds = new List<int>();
                DataTable dt = this.GetBetTypeScoreMap();
                DataRowCollection dr = dt.Rows;
                foreach (DataRow items in dr)
                {
                    bettypeIds.Add(Convert.ToInt32(items["typeid"]));
                    firstId = Convert.ToString(items["1stId"]);
                    if (firstId != "0")
                    {
                        string[] firstIdArray = firstId.Split(splitChars, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string id in firstIdArray)
                        {
                            bettypeIds.Add(int.Parse(id));
                        }
                    }
                }

                // Store in cache
                cache.Set<List<int>>(BetTypeListScoreMapKey, bettypeIds, TimeSpan.FromHours(2));

                return bettypeIds;
            }

            return oldData as List<int>;
        }

        #endregion Bet Type Score Map

        #region Get Score Map Result

        /// <summary>
        /// Get Score Map Result
        /// </summary>
        /// <returns></returns>
        public DataSet GetScoreMapResult(string matchIds, string bettypes_live, string bettypes_dead)
        {
            DataSet scoreMapResult = null;
            int customerId = CurrentSession.CustId;
            if (CurrentSession.IsAgent)
            {
                scoreMapResult = ForecastDataAccess.Age_ScoreMap_Result_Ag(customerId, matchIds, bettypes_live, bettypes_dead);
            }
            else if (CurrentSession.IsMaster)
            {
                scoreMapResult = ForecastDataAccess.Age_ScoreMap_Result_MAg(customerId, matchIds, bettypes_live, bettypes_dead);
            }
            else if (CurrentSession.IsSuper)
            {
                scoreMapResult = ForecastDataAccess.Age_ScoreMap_Result_SAg(customerId, matchIds, bettypes_live, bettypes_dead);
            }
            else
            {
                scoreMapResult = new DataSet();
            }

            return scoreMapResult;
        }

        #endregion Get Score Map Result

        #endregion Score Map
    }
}