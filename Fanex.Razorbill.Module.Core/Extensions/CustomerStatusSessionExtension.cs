namespace Fanex.Razorbill.Module.Core.Extensions
{
    using System;
    using Fanex.CustomerSetting.Module.Core.Web;
    using Sunplus.Agent.Model.Entities;

    public class CustomerStatusSessionExtension : ICurrentSession
    {
        public string Captcha
        {
            get;

            set;
        }

        public string CurrencyGroup
        {
            get;

            set;
        }

        public int CustId
        {
            get;

            set;
        }

        public int FromSystem
        {
            get { return 2; }
            set { }
        }

        public int GroupId
        {
            get;

            set;
        }

        public bool IsInternalAdmin
        {
            get;

            set;
        }

        public bool IsITAdmin
        {
            get;

            set;
        }

        public bool IsLogin
        {
            get;

            set;
        }

        public string Language
        {
            get
            {
                return UserSession.Current.Language;
            }

            set
            {
            }
        }

        public string NetSessionId
        {
            get;

            set;
        }

        public int OnUserId
        {
            get;

            set;
        }

        public int UserId
        {
            get;

            set;
        }

        public string UserName
        {
            get;

            set;
        }
    }
}
