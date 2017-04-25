namespace Sunplus.Agent.Model.DL.TotalBets
{
    using System;
    using System.Collections;
    using System.Data;

     [Obsolete("Should rename to Sunplus.Agent.Model.TotalBets")]
    public class TotalBetsDAO : Sunplus.Agent.Model.DL.Base.DAOBase
    {
        #region TotalGoal
        /// <summary>
        /// 
        /// </summary>
        /// <param name="custId"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public DataTable Age_TotalBets_Sel6_Ag(int custId, string language)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("custid", custId);
            parameters.Add("language", language);

            return ExecSpTable("Age_TotalBets_Sel6_Ag", parameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="custId"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public DataTable Age_TotalBets_Sel6_MAg(int custId, string language)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("custid", custId);
            parameters.Add("language", language);

            return ExecSpTable("Age_TotalBets_Sel6_MAg", parameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="custId"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public DataTable Age_TotalBets_Sel6_SAg(int custId, string language)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("custid", custId);
            parameters.Add("language", language);

            return ExecSpTable("Age_TotalBets_Sel6_SAg", parameters);
        }
        #endregion

        #region HDPOU

        /// <summary>
        /// Get handicap over under for agent
        /// </summary>
        /// <param name="custId"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public DataTable Age_TotalBets_Sel1378_Ag(int custId, string language)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("custid", custId);
            parameters.Add("language", language);

            return ExecSpTable("Age_TotalBets_Sel1378_Ag", parameters);
        }

        /// <summary>
        /// Get handicap over under for master
        /// </summary>
        /// <param name="custId"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public DataTable Age_TotalBets_Sel1378_MAg(int custId, string language)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("custid", custId);
            parameters.Add("language", language);

            return ExecSpTable("Age_TotalBets_Sel1378_MAg", parameters);
        }

        /// <summary>
        /// Get handicap over under for super
        /// </summary>
        /// <param name="custId"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public DataTable Age_TotalBets_Sel1378_SAg(int custId, string language)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("custid", custId);
            parameters.Add("language", language);

            return ExecSpTable("Age_TotalBets_Sel1378_SAg", parameters);
        }
        #endregion

        #region MoneyLine

        /// <summary>
        /// Get money line for agent
        /// </summary>
        /// <param name="custId"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public DataTable Age_TotalBets_Sel2021_Ag(int custId, string language)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("custid", custId);
            parameters.Add("language", language);

            return ExecSpTable("Age_TotalBets_Sel2021_Ag", parameters);
        }

        /// <summary>
        /// Get money line for agent
        /// </summary>
        /// <param name="custId"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public DataTable Age_TotalBets_Sel2021_MAg(int custId, string language)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("custid", custId);
            parameters.Add("language", language);

            return ExecSpTable("Age_TotalBets_Sel2021_MAg", parameters);
        }

        /// <summary>
        /// Get money line for agent
        /// </summary>
        /// <param name="custId"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public DataTable Age_TotalBets_Sel2021_SAg(int custId, string language)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("custid", custId);
            parameters.Add("language", language);

            return ExecSpTable("Age_TotalBets_Sel2021_SAg", parameters);
        }

        #endregion

        #region OddEvent_OneXTwo

        /// <summary>
        /// Get odd event and one x two for agent
        /// </summary>
        /// <param name="custId"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public DataTable Age_TotalBets_Sel25_Ag(int custId, string language)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("custid", custId);
            parameters.Add("language", language);

            return ExecSpTable("Age_TotalBets_Sel25_Ag", parameters);
        }

        /// <summary>
        /// Get odd event and one x two for master
        /// </summary>
        /// <param name="custId"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public DataTable Age_TotalBets_Sel25_MAg(int custId, string language)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("custid", custId);
            parameters.Add("language", language);

            return ExecSpTable("Age_TotalBets_Sel25_MAg", parameters);
        }

        /// <summary>
        /// Get odd event and one x two for super
        /// </summary>
        /// <param name="custId"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public DataTable Age_TotalBets_Sel25_SAg(int custId, string language)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("custid", custId);
            parameters.Add("language", language);

            return ExecSpTable("Age_TotalBets_Sel25_SAg", parameters);
        }
        #endregion

        #region OutRight
        /// <summary>
        /// Get outright for agent
        /// </summary>
        /// <param name="custId"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public DataTable Age_TotalBets_Sel10_Ag(int custId, string language)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("custid", custId);
            parameters.Add("language", language);

            return ExecSpTable("Age_TotalBets_Sel10_Ag", parameters);
        }

        /// <summary>
        /// Get outright for master
        /// </summary>
        /// <param name="custId"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public DataTable Age_TotalBets_Sel10_MAg(int custId, string language)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("custid", custId);
            parameters.Add("language", language);

            return ExecSpTable("Age_TotalBets_Sel10_MAg", parameters);
        }

        /// <summary>
        /// Get outright for super
        /// </summary>
        /// <param name="custId"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public DataTable Age_TotalBets_Sel10_SAg(int custId, string language)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("custid", custId);
            parameters.Add("language", language);

            return ExecSpTable("Age_TotalBets_Sel10_SAg", parameters);
        }
        #endregion

        #region HTFT

        /// <summary>
        /// Get HTFT for agent
        /// </summary>
        /// <param name="custId"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public DataTable Age_TotalBets_Sel16_Ag(int custId, string language)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("custid", custId);
            parameters.Add("language", language);

            return ExecSpTable("Age_TotalBets_Sel16_Ag", parameters);
        }

        /// <summary>
        /// Get HTFT for master
        /// </summary>
        /// <param name="custId"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public DataTable Age_TotalBets_Sel16_MAg(int custId, string language)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("custid", custId);
            parameters.Add("language", language);

            return ExecSpTable("Age_TotalBets_Sel16_MAg", parameters);
        }

        /// <summary>
        /// Get HTFT for super
        /// </summary>
        /// <param name="custId"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public DataTable Age_TotalBets_Sel16_SAg(int custId, string language)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("custid", custId);
            parameters.Add("language", language);

            return ExecSpTable("Age_TotalBets_Sel16_SAg", parameters);
        }
        #endregion

        #region FGLG

        /// <summary>
        /// Get FGLG for Agent
        /// </summary>
        /// <param name="custId"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public DataTable Age_TotalBets_Sel14_Ag(int custId, string language)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("custid", custId);
            parameters.Add("language", language);

            return ExecSpTable("Age_TotalBets_Sel14_Ag", parameters);
        }

        /// <summary>
        /// Get FGLG for Master
        /// </summary>
        /// <param name="custId"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public DataTable Age_TotalBets_Sel14_MAg(int custId, string language)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("custid", custId);
            parameters.Add("language", language);

            return ExecSpTable("Age_TotalBets_Sel14_MAg", parameters);
        }

        /// <summary>
        /// Get FGLG for super
        /// </summary>
        /// <param name="custId"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public DataTable Age_TotalBets_Sel14_SAg(int custId, string language)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("custid", custId);
            parameters.Add("language", language);

            return ExecSpTable("Age_TotalBets_Sel14_SAg", parameters);
        }
        #endregion

        #region Home/Draw/AwayNoBet

        /// <summary>
        /// Get Home/Draw/AwayNoBet for Agent
        /// </summary>
        /// <param name="custId"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public DataTable Age_TotalBets_Sel121_Ag(int custId, string language)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("custid", custId);
            parameters.Add("language", language);

            return ExecSpTable("Age_TotalBets_Sel121_Ag", parameters);
        }

        /// <summary>
        /// Get Home/Draw/AwayNoBet for Master
        /// </summary>
        /// <param name="custId"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public DataTable Age_TotalBets_Sel121_MAg(int custId, string language)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("custid", custId);
            parameters.Add("language", language);

            return ExecSpTable("Age_TotalBets_Sel121_MAg", parameters);
        }

        /// <summary>
        /// Get Home/Draw/AwayNoBet for super
        /// </summary>
        /// <param name="custId"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public DataTable Age_TotalBets_Sel121_SAg(int custId, string language)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("custid", custId);
            parameters.Add("language", language);

            return ExecSpTable("Age_TotalBets_Sel121_SAg", parameters);
        }
        #endregion

        #region NextGoal

        /// <summary>
        /// Get NextGoal for agent
        /// </summary>
        /// <param name="custId"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public DataTable Age_TotalBets_Sel22_Ag(int custId, string language)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("custid", custId);
            parameters.Add("language", language);
            return ExecSpTable("Age_TotalBets_Sel22_Ag", parameters);
        }

        /// <summary>
        /// Get NextGoal for master
        /// </summary>
        /// <param name="custId"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public DataTable Age_TotalBets_Sel22_MAg(int custId, string language)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("custid", custId);
            parameters.Add("language", language);

            return ExecSpTable("Age_TotalBets_Sel22_MAg", parameters);
        }

        /// <summary>
        /// Get NextGoal for super
        /// </summary>
        /// <param name="custId"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public DataTable Age_TotalBets_Sel22_SAg(int custId, string language)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("custid", custId);
            parameters.Add("language", language);

            return ExecSpTable("Age_TotalBets_Sel22_SAg", parameters);
        }
        #endregion

        #region HorseRacing

        /// <summary>
        /// Get HorseRacing for agent
        /// </summary>
        /// <param name="custId"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public DataTable Age_TotalBets_SelHR_Ag(int custId, string language)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("custid", custId);
            parameters.Add("language", language);
            return ExecSpTable("Age_TotalBets_SelHR_Ag", parameters);
        }

        /// <summary>
        /// Get HorseRacing for master
        /// </summary>
        /// <param name="custId"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public DataTable Age_TotalBets_SelHR_MAg(int custId, string language)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("custid", custId);
            parameters.Add("language", language);

            return ExecSpTable("Age_TotalBets_SelHR_MAg", parameters);
        }

        /// <summary>
        /// Get HorseRacing for super
        /// </summary>
        /// <param name="custId"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public DataTable Age_TotalBets_SelHR_SAg(int custId, string language)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("custid", custId);
            parameters.Add("language", language);

            return ExecSpTable("Age_TotalBets_SelHR_SAg", parameters);
        }
        #endregion

        #region Financial

        /// <summary>
        /// Get Financial for agent
        /// </summary>
        /// <param name="custId"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public DataTable Age_TotalBets_Sel201_Ag(int custId, string language)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("custid", custId);
            parameters.Add("language", language);
            return ExecSpTable("Age_TotalBets_Sel201_Ag", parameters);
        }

        /// <summary>
        /// Get Financial for master
        /// </summary>
        /// <param name="custId"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public DataTable Age_TotalBets_Sel201_MAg(int custId, string language)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("custid", custId);
            parameters.Add("language", language);

            return ExecSpTable("Age_TotalBets_Sel201_MAg", parameters);
        }

        /// <summary>
        /// Get Financial for super
        /// </summary>
        /// <param name="custId"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public DataTable Age_TotalBets_Sel201_SAg(int custId, string language)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("custid", custId);
            parameters.Add("language", language);

            return ExecSpTable("Age_TotalBets_Sel201_SAg", parameters);
        }
        #endregion

        #region NumberGame
        /// <summary>
        /// 
        /// </summary>
        /// <param name="custId"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public DataTable Age_TotalBets_Sel161_Ag(int custId, string language)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("custid", custId);
            parameters.Add("language", language);
            return ExecSpTable("Age_TotalBets_Sel161_Ag", parameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="custId"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public DataTable Age_TotalBets_Sel161_MAg(int custId, string language)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("custid", custId);
            parameters.Add("language", language);
            return ExecSpTable("Age_TotalBets_Sel161_MAg", parameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="custId"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public DataTable Age_TotalBets_Sel161_SAg(int custId, string language)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("custid", custId);
            parameters.Add("language", language);

            return ExecSpTable("Age_TotalBets_Sel161_SAg", parameters);
        }
        #endregion

        #region LiveCasino
        /// <summary>
        /// 
        /// </summary>
        /// <param name="custId"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public DataTable Age_TotalBets_Sel162_Ag(int custId, string language)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("custid", custId);
            parameters.Add("language", language);
            return ExecSpTable("Age_TotalBets_Sel162_Ag", parameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="custId"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public DataTable Age_TotalBets_Sel162_MAg(int custId, string language)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("custid", custId);
            parameters.Add("language", language);
            return ExecSpTable("Age_TotalBets_Sel162_MAg", parameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="custId"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public DataTable Age_TotalBets_Sel162_SAg(int custId, string language)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("custid", custId);
            parameters.Add("language", language);

            return ExecSpTable("Age_TotalBets_Sel162_SAg", parameters);
        }
        #endregion

        #region VirtualSports
        /// <summary>
        /// 
        /// </summary>
        /// <param name="custId"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public DataTable Age_TotalBets_Sel180_Ag(int custId, string language)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("custid", custId);
            parameters.Add("language", language);
            return ExecSpTable("Age_TotalBets_Sel180_Ag", parameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="custId"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public DataTable Age_TotalBets_Sel180_MAg(int custId, string language)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("custid", custId);
            parameters.Add("language", language);
            return ExecSpTable("Age_TotalBets_Sel180_MAg", parameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="custId"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public DataTable Age_TotalBets_Sel180_SAg(int custId, string language)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("custid", custId);
            parameters.Add("language", language);

            return ExecSpTable("Age_TotalBets_Sel180_SAg", parameters);
        }
        #endregion

        #region GetOtherBettypes

        /// <summary>
        /// Get Other Bettypes for Agent
        /// </summary>
        /// <param name="custId"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public DataTable Age_TotalBets_SelOthers_Ag(int custId, string language)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("custid", custId);
            parameters.Add("language", language);

            return ExecSpTable("Age_TotalBets_SelOthers_Ag", parameters);
        }

        /// <summary>
        /// Get Other Bettypes for Master
        /// </summary>
        /// <param name="custId"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public DataTable Age_TotalBets_SelOthers_MAg(int custId, string language)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("custid", custId);
            parameters.Add("language", language);

            return ExecSpTable("Age_TotalBets_SelOthers_MAg", parameters);
        }

        /// <summary>
        /// Get Other Bettypes for Super
        /// </summary>
        /// <param name="custId"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public DataTable Age_TotalBets_SelOthers_SAg(int custId, string language)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("custid", custId);
            parameters.Add("language", language);

            return ExecSpTable("Age_TotalBets_SelOthers_SAg", parameters);
        }

        #endregion
    }
}