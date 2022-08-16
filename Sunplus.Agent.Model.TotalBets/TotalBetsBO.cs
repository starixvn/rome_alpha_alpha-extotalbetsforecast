namespace Sunplus.Agent.Model.BL.TotalBets
{
    using System;
    using System.Data;
    using Alpha.PartialUM.Services;
    using Fanex.Razorbill.Module.Core.DataAccess;
    using Sunplus.Agent.Model.DL.TotalBets;

    [Obsolete("Should rename to Sunplus.Agent.Model.TotalBets")]
    public class TotalBetsBO : Sunplus.Agent.Model.BL.Base.BOBase
    {
        #region Variable
        private static readonly TotalBetsDAO TotalBetsDataAccess = new TotalBetsDAO();
        private readonly IPartialUMService partialUMService;
        #endregion

        public TotalBetsBO(string netSessionId)
            : base(netSessionId)
        {
            partialUMService = new PartialUMService(new PartialUMTotalBetsForecastDataAccess());
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
                partialUMService.ThrowIfUM(nameof(TotalBetsDataAccess.Age_TotalBets_Sel6_Ag));
                totalGoal = TotalBetsDataAccess.Age_TotalBets_Sel6_Ag(customerID, language);
            }
            else if (CurrentSession.IsMaster)
            {
                partialUMService.ThrowIfUM(nameof(TotalBetsDataAccess.Age_TotalBets_Sel6_MAg));
                totalGoal = TotalBetsDataAccess.Age_TotalBets_Sel6_MAg(customerID, language);
            }
            else if (CurrentSession.IsSuper)
            {
                partialUMService.ThrowIfUM(nameof(TotalBetsDataAccess.Age_TotalBets_Sel6_SAg));
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

            // execute
            if (CurrentSession.IsAgent)
            {
                partialUMService.ThrowIfUM(nameof(TotalBetsDataAccess.Age_TotalBets_Sel1378_Member));
                hpdOU = TotalBetsDataAccess.Age_TotalBets_Sel1378_Member(customerID);
            }
            else if (CurrentSession.IsMaster)
            {
                partialUMService.ThrowIfUM(nameof(TotalBetsDataAccess.Age_TotalBets_Sel1378_Agent));
                hpdOU = TotalBetsDataAccess.Age_TotalBets_Sel1378_Agent(customerID);
            }
            else if (CurrentSession.IsSuper)
            {
                partialUMService.ThrowIfUM(nameof(TotalBetsDataAccess.Age_TotalBets_Sel1378_Master));
                hpdOU = TotalBetsDataAccess.Age_TotalBets_Sel1378_Master(customerID);
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
                partialUMService.ThrowIfUM(nameof(TotalBetsDataAccess.Age_TotalBets_Sel2021_Ag));
                moneyline = TotalBetsDataAccess.Age_TotalBets_Sel2021_Ag(customerID, language);
            }
            else if (CurrentSession.IsMaster)
            {
                partialUMService.ThrowIfUM(nameof(TotalBetsDataAccess.Age_TotalBets_Sel2021_MAg));
                moneyline = TotalBetsDataAccess.Age_TotalBets_Sel2021_MAg(customerID, language);
            }
            else if (CurrentSession.IsSuper)
            {
                partialUMService.ThrowIfUM(nameof(TotalBetsDataAccess.Age_TotalBets_Sel2021_SAg));
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
                partialUMService.ThrowIfUM(nameof(TotalBetsDataAccess.Age_TotalBets_Sel25_Ag));
                oddEventOneXTwo = TotalBetsDataAccess.Age_TotalBets_Sel25_Ag(customerID, language);
            }
            else if (CurrentSession.IsMaster)
            {
                partialUMService.ThrowIfUM(nameof(TotalBetsDataAccess.Age_TotalBets_Sel25_MAg));
                oddEventOneXTwo = TotalBetsDataAccess.Age_TotalBets_Sel25_MAg(customerID, language);
            }
            else if (CurrentSession.IsSuper)
            {
                partialUMService.ThrowIfUM(nameof(TotalBetsDataAccess.Age_TotalBets_Sel25_SAg));
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
                partialUMService.ThrowIfUM(nameof(TotalBetsDataAccess.Age_TotalBets_Sel10_Ag));
                outright = TotalBetsDataAccess.Age_TotalBets_Sel10_Ag(customerID, language);
            }
            else if (CurrentSession.IsMaster)
            {
                partialUMService.ThrowIfUM(nameof(TotalBetsDataAccess.Age_TotalBets_Sel10_MAg));
                outright = TotalBetsDataAccess.Age_TotalBets_Sel10_MAg(customerID, language);
            }
            else if (CurrentSession.IsSuper)
            {
                partialUMService.ThrowIfUM(nameof(TotalBetsDataAccess.Age_TotalBets_Sel10_SAg));
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
                partialUMService.ThrowIfUM(nameof(TotalBetsDataAccess.Age_TotalBets_Sel16_Ag));
                haftTimeFullTimeData = TotalBetsDataAccess.Age_TotalBets_Sel16_Ag(customerID, language);
            }
            else if (CurrentSession.IsMaster)
            {
                partialUMService.ThrowIfUM(nameof(TotalBetsDataAccess.Age_TotalBets_Sel16_MAg));
                haftTimeFullTimeData = TotalBetsDataAccess.Age_TotalBets_Sel16_MAg(customerID, language);
            }
            else if (CurrentSession.IsSuper)
            {
                partialUMService.ThrowIfUM(nameof(TotalBetsDataAccess.Age_TotalBets_Sel16_SAg));
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
                partialUMService.ThrowIfUM(nameof(TotalBetsDataAccess.Age_TotalBets_Sel14_Ag));
                firstGoalLastGoalData = TotalBetsDataAccess.Age_TotalBets_Sel14_Ag(customerID, language);
            }
            else if (CurrentSession.IsMaster)
            {
                partialUMService.ThrowIfUM(nameof(TotalBetsDataAccess.Age_TotalBets_Sel14_MAg));
                firstGoalLastGoalData = TotalBetsDataAccess.Age_TotalBets_Sel14_MAg(customerID, language);
            }
            else if (CurrentSession.IsSuper)
            {
                partialUMService.ThrowIfUM(nameof(TotalBetsDataAccess.Age_TotalBets_Sel14_SAg));
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
                partialUMService.ThrowIfUM(nameof(TotalBetsDataAccess.Age_TotalBets_Sel121_Ag));
                homeDrawAwayNoBet = TotalBetsDataAccess.Age_TotalBets_Sel121_Ag(customerID, language);
            }
            else if (CurrentSession.IsMaster)
            {
                partialUMService.ThrowIfUM(nameof(TotalBetsDataAccess.Age_TotalBets_Sel121_MAg));
                homeDrawAwayNoBet = TotalBetsDataAccess.Age_TotalBets_Sel121_MAg(customerID, language);
            }
            else if (CurrentSession.IsSuper)
            {
                partialUMService.ThrowIfUM(nameof(TotalBetsDataAccess.Age_TotalBets_Sel121_SAg));
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
                partialUMService.ThrowIfUM(nameof(TotalBetsDataAccess.Age_TotalBets_Sel161_Ag));
                numberGame = TotalBetsDataAccess.Age_TotalBets_Sel161_Ag(customerID, language);
            }
            else if (CurrentSession.IsMaster)
            {
                partialUMService.ThrowIfUM(nameof(TotalBetsDataAccess.Age_TotalBets_Sel161_MAg));
                numberGame = TotalBetsDataAccess.Age_TotalBets_Sel161_MAg(customerID, language);
            }
            else if (CurrentSession.IsSuper)
            {
                partialUMService.ThrowIfUM(nameof(TotalBetsDataAccess.Age_TotalBets_Sel161_SAg));
                numberGame = TotalBetsDataAccess.Age_TotalBets_Sel161_SAg(customerID, language);
            }
            else
            {
                numberGame = new DataTable();
            }

            return numberGame;
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
