namespace Sunplus.Agent.Model.BL.TotalBets
{
    using System;
    using System.Data;
    using Sunplus.Agent.Model.DL.TotalBets;

    [Obsolete("Should rename to Sunplus.Agent.Model.TotalBets")]
    public class TotalBetsBO : Sunplus.Agent.Model.BL.Base.BOBase
    {
        #region Variable
        private static readonly TotalBetsDAO TotalBetsDataAccess = new TotalBetsDAO();
        #endregion

        public TotalBetsBO(string netSessionId)
            : base(netSessionId)
        { 
        }

        #region TotalGoal
        /// <summary>
        /// Get data for total goal page
        /// </summary>
        /// <returns></returns>
        public DataTable GetTotalGoal()
        {
            DataTable totalGoal = null;

            // parameter
            int customerID = CurrentSession.CustId;
            string language = CurrentSession.LangKey;

            // execute
            if (CurrentSession.IsAgent)
            {
                totalGoal = TotalBetsDataAccess.Age_TotalBets_Sel6_Ag(customerID, language);
            }
            else if (CurrentSession.IsMaster)
            {
                totalGoal = TotalBetsDataAccess.Age_TotalBets_Sel6_MAg(customerID, language);
            }
            else if (CurrentSession.IsSuper)
            {
                totalGoal = TotalBetsDataAccess.Age_TotalBets_Sel6_SAg(customerID, language);
            }
            else
            {
                totalGoal = new DataTable();
            }

            return totalGoal;
        }
        #endregion

        #region HDPOU
        /// <summary>
        /// Get data for HDPOU page
        /// </summary>
        /// <returns></returns>
        public DataTable GetHDPOU()
        {
            DataTable hpdOU = null;

            // parameter
            int customerID = CurrentSession.CustId;
            string language = CurrentSession.LangKey;

            // execute
            if (CurrentSession.IsAgent)
            {
                hpdOU = TotalBetsDataAccess.Age_TotalBets_Sel1378_Ag(customerID, language);
            }
            else if (CurrentSession.IsMaster)
            {
                hpdOU = TotalBetsDataAccess.Age_TotalBets_Sel1378_MAg(customerID, language);
            }
            else if (CurrentSession.IsSuper)
            {
                hpdOU = TotalBetsDataAccess.Age_TotalBets_Sel1378_SAg(customerID, language);
            }
            else
            {
                hpdOU = new DataTable();
            }

            return hpdOU;
        }
        #endregion

        #region MoneyLine

        /// <summary>
        /// Get data for money line page
        /// </summary>
        /// <returns></returns>
        public DataTable GetMoneyLine()
        {
            DataTable moneyline = null;

            // parameter
            int customerID = CurrentSession.CustId;
            string language = CurrentSession.LangKey;

            // execute
            if (CurrentSession.IsAgent)
            {
                moneyline = TotalBetsDataAccess.Age_TotalBets_Sel2021_Ag(customerID, language);
            }
            else if (CurrentSession.IsMaster)
            {
                moneyline = TotalBetsDataAccess.Age_TotalBets_Sel2021_MAg(customerID, language);
            }
            else if (CurrentSession.IsSuper)
            {
                moneyline = TotalBetsDataAccess.Age_TotalBets_Sel2021_SAg(customerID, language);
            }
            else
            {
                moneyline = new DataTable();
            }

            return moneyline;
        }
        #endregion

        #region OddEvent_OneXTwo

        /// <summary>
        /// Get data for money line page
        /// </summary>
        /// <returns></returns>
        public DataTable GetOddEventOneXTwo()
        {
            DataTable oddEventOneXTwo = null;

            // parameter
            int customerID = CurrentSession.CustId;
            string language = CurrentSession.LangKey;

            // execute
            if (CurrentSession.IsAgent)
            {
                oddEventOneXTwo = TotalBetsDataAccess.Age_TotalBets_Sel25_Ag(customerID, language);
            }
            else if (CurrentSession.IsMaster)
            {
                oddEventOneXTwo = TotalBetsDataAccess.Age_TotalBets_Sel25_MAg(customerID, language);
            }
            else if (CurrentSession.IsSuper)
            {
                oddEventOneXTwo = TotalBetsDataAccess.Age_TotalBets_Sel25_SAg(customerID, language);
            }
            else
            {
                oddEventOneXTwo = new DataTable();
            }

            return oddEventOneXTwo;
        }
        #endregion

        #region OutRight
        /// <summary>
        /// Get data for outright page
        /// </summary>
        /// <returns></returns>
        public DataTable GetOutRight()
        {
            DataTable outright = null;

            // parameter
            int customerID = CurrentSession.CustId;
            string language = CurrentSession.LangKey;

            // execute
            if (CurrentSession.IsAgent)
            {
                outright = TotalBetsDataAccess.Age_TotalBets_Sel10_Ag(customerID, language);
            }
            else if (CurrentSession.IsMaster)
            {
                outright = TotalBetsDataAccess.Age_TotalBets_Sel10_MAg(customerID, language);
            }
            else if (CurrentSession.IsSuper)
            {
                outright = TotalBetsDataAccess.Age_TotalBets_Sel10_SAg(customerID, language);
            }
            else
            {
                outright = new DataTable();
            }

            return outright;
        }
        #endregion

        #region HTFT
        /// <summary>
        /// Get data for HTFT page
        /// </summary>
        /// <returns></returns>
        public DataTable GetHTFT()
        {
            DataTable haftTimeFullTimeData = null;

            // parameter
            int customerID = CurrentSession.CustId;
            string language = CurrentSession.LangKey;

            // execute
            if (CurrentSession.IsAgent)
            {
                haftTimeFullTimeData = TotalBetsDataAccess.Age_TotalBets_Sel16_Ag(customerID, language);
            }
            else if (CurrentSession.IsMaster)
            {
                haftTimeFullTimeData = TotalBetsDataAccess.Age_TotalBets_Sel16_MAg(customerID, language);
            }
            else if (CurrentSession.IsSuper)
            {
                haftTimeFullTimeData = TotalBetsDataAccess.Age_TotalBets_Sel16_SAg(customerID, language);
            }
            else
            {
                haftTimeFullTimeData = new DataTable();
            }

            return haftTimeFullTimeData;
        }
        #endregion

        #region FGLG
        /// <summary>
        /// Get data for FGLG page
        /// </summary>
        /// <returns></returns>
        public DataTable GetFGLG()
        {
            DataTable firstGoalLastGoalData = null;

            // parameter
            int customerID = CurrentSession.CustId;
            string language = CurrentSession.LangKey;

            // execute
            if (CurrentSession.IsAgent)
            {
                firstGoalLastGoalData = TotalBetsDataAccess.Age_TotalBets_Sel14_Ag(customerID, language);
            }
            else if (CurrentSession.IsMaster)
            {
                firstGoalLastGoalData = TotalBetsDataAccess.Age_TotalBets_Sel14_MAg(customerID, language);
            }
            else if (CurrentSession.IsSuper)
            {
                firstGoalLastGoalData = TotalBetsDataAccess.Age_TotalBets_Sel14_SAg(customerID, language);
            }
            else
            {
                firstGoalLastGoalData = new DataTable();
            }

            return firstGoalLastGoalData;
        }
        #endregion

        #region Home/Draw/AwayNoBet
        /// <summary>
        /// Get data for Home/Draw/AwayNoBet page
        /// </summary>
        /// <returns></returns>
        public DataTable GetHomeDrawAwayNoBet()
        {
            DataTable homeDrawAwayNoBet = null;

            // parameter
            int customerID = CurrentSession.CustId;
            string language = CurrentSession.LangKey;

            // execute
            if (CurrentSession.IsAgent)
            {
                homeDrawAwayNoBet = TotalBetsDataAccess.Age_TotalBets_Sel121_Ag(customerID, language);
            }
            else if (CurrentSession.IsMaster)
            {
                homeDrawAwayNoBet = TotalBetsDataAccess.Age_TotalBets_Sel121_MAg(customerID, language);
            }
            else if (CurrentSession.IsSuper)
            {
                homeDrawAwayNoBet = TotalBetsDataAccess.Age_TotalBets_Sel121_SAg(customerID, language);
            }
            else
            {
                homeDrawAwayNoBet = new DataTable();
            }

            return homeDrawAwayNoBet;
        }
        #endregion

        #region NextGoal
        /// <summary>
        /// Get data for NextGoal page
        /// </summary>
        /// <returns></returns>
        public DataTable GetNextGoal()
        {
            DataTable nextGoal = null;

            // parameter
            int customerID = CurrentSession.CustId;
            string language = CurrentSession.LangKey;

            // execute
            if (CurrentSession.IsAgent)
            {
                nextGoal = TotalBetsDataAccess.Age_TotalBets_Sel22_Ag(customerID, language);
            }
            else if (CurrentSession.IsMaster)
            {
                nextGoal = TotalBetsDataAccess.Age_TotalBets_Sel22_MAg(customerID, language);
            }
            else if (CurrentSession.IsSuper)
            {
                nextGoal = TotalBetsDataAccess.Age_TotalBets_Sel22_SAg(customerID, language);
            }
            else
            {
                nextGoal = new DataTable();
            }

            return nextGoal;
        }
        #endregion

        #region HorseRacing
        /// <summary>
        /// Get data for NextGoal page
        /// </summary>
        /// <returns></returns>
        public DataTable GetHorseRacing()
        {
            DataTable horseRacing = null;

            // parameter
            int customerID = CurrentSession.CustId;
            string language = CurrentSession.LangKey;

            // execute
            if (CurrentSession.IsAgent)
            {
                horseRacing = TotalBetsDataAccess.Age_TotalBets_SelHR_Ag(customerID, language);
            }
            else if (CurrentSession.IsMaster)
            {
                horseRacing = TotalBetsDataAccess.Age_TotalBets_SelHR_MAg(customerID, language);
            }
            else if (CurrentSession.IsSuper)
            {
                horseRacing = TotalBetsDataAccess.Age_TotalBets_SelHR_SAg(customerID, language);
            }
            else
            {
                horseRacing = new DataTable();
            }

            return horseRacing;
        }
        #endregion

        #region Financial
        /// <summary>
        /// Get data for Financial page
        /// </summary>
        /// <returns></returns>
        public DataTable GetFinancial()
        {
            DataTable financial = null;

            // parameter
            int customerID = CurrentSession.CustId;
            string language = CurrentSession.LangKey;

            // execute
            if (CurrentSession.IsAgent)
            {
                financial = TotalBetsDataAccess.Age_TotalBets_Sel201_Ag(customerID, language);
            }
            else if (CurrentSession.IsMaster)
            {
                financial = TotalBetsDataAccess.Age_TotalBets_Sel201_MAg(customerID, language);
            }
            else if (CurrentSession.IsSuper)
            {
                financial = TotalBetsDataAccess.Age_TotalBets_Sel201_SAg(customerID, language);
            }
            else
            {
                financial = new DataTable();
            }

            return financial;
        }
        #endregion

        #region NumberGame
        /// <summary>
        /// Get data for number game page
        /// </summary>
        /// <returns></returns>
        public DataTable GetNumberGame()
        {
            DataTable numberGame = null;

            // parameter
            int customerID = CurrentSession.CustId;
            string language = CurrentSession.LangKey;

            // execute
            if (CurrentSession.IsAgent)
            {
                numberGame = TotalBetsDataAccess.Age_TotalBets_Sel161_Ag(customerID, language);
            }
            else if (CurrentSession.IsMaster)
            {
                numberGame = TotalBetsDataAccess.Age_TotalBets_Sel161_MAg(customerID, language);
            }
            else if (CurrentSession.IsSuper)
            {
                numberGame = TotalBetsDataAccess.Age_TotalBets_Sel161_SAg(customerID, language);
            }
            else
            {
                numberGame = new DataTable();
            }

            return numberGame;
        }
        #endregion

        #region LiveCasino
        /// <summary>
        /// Get data for LiveCasino page
        /// </summary>
        /// <returns></returns>
        public DataTable GetLiveCasino()
        {
            DataTable liveCasino = null;

            // parameter
            int customerID = CurrentSession.CustId;
            string language = CurrentSession.LangKey;

            // execute
            if (CurrentSession.IsAgent)
            {
                liveCasino = TotalBetsDataAccess.Age_TotalBets_Sel162_Ag(customerID, language);
            }
            else if (CurrentSession.IsMaster)
            {
                liveCasino = TotalBetsDataAccess.Age_TotalBets_Sel162_MAg(customerID, language);
            }
            else if (CurrentSession.IsSuper)
            {
                liveCasino = TotalBetsDataAccess.Age_TotalBets_Sel162_SAg(customerID, language);
            }
            else
            {
                liveCasino = new DataTable();
            }

            return liveCasino;
        }
        #endregion

        #region VirtualSports
        /// <summary>
        /// Get data for VirtualSports page
        /// </summary>
        /// <returns></returns>
        public DataTable GetVirtualSports()
        {
            DataTable virtualSports = null;

            // parameter
            int customerID = CurrentSession.CustId;
            string language = CurrentSession.LangKey;

            // execute
            if (CurrentSession.IsAgent)
            {
                virtualSports = TotalBetsDataAccess.Age_TotalBets_Sel180_Ag(customerID, language);
            }
            else if (CurrentSession.IsMaster)
            {
                virtualSports = TotalBetsDataAccess.Age_TotalBets_Sel180_MAg(customerID, language);
            }
            else if (CurrentSession.IsSuper)
            {
                virtualSports = TotalBetsDataAccess.Age_TotalBets_Sel180_SAg(customerID, language);
            }
            else
            {
                virtualSports = new DataTable();
            }

            return virtualSports;
        }
        #endregion

        #region Other bettypes
        /// <summary>
        /// Get data for Other bettypes page
        /// </summary>
        /// <returns></returns>
        public DataTable GetOtherBettypes()
        {
            DataTable otherBettypes = null;

            // parameter
            int customerID = CurrentSession.CustId;
            string language = CurrentSession.LangKey;

            // execute
            if (CurrentSession.IsAgent)
            {
                otherBettypes = TotalBetsDataAccess.Age_TotalBets_SelOthers_Ag(customerID, language);
            }
            else if (CurrentSession.IsMaster)
            {
                otherBettypes = TotalBetsDataAccess.Age_TotalBets_SelOthers_MAg(customerID, language);
            }
            else if (CurrentSession.IsSuper)
            {
                otherBettypes = TotalBetsDataAccess.Age_TotalBets_SelOthers_SAg(customerID, language);
            }
            else
            {
                otherBettypes = new DataTable();
            }

            return otherBettypes;
        }

        #endregion
    }
}
