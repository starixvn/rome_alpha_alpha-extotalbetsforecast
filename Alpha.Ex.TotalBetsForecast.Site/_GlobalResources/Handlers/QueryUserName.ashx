<%@ WebHandler Language="C#" Class="Sunplus.Agent.UI._AccountInfo.QueryUserName_Handler" %>

using System;
using System.Web;
using System.Data;
using Fanex.Age.UI;

using Sunplus.Agent.Model.BL.AccountInfo;

namespace Sunplus.Agent.UI._AccountInfo
{

    public class QueryUserName_Handler : Fanex.Age.UI.TplHandler, System.Web.SessionState.IReadOnlySessionState
    {
        public QueryUserName_Handler()
        {
            IsValidateRequest = false;
        }

        public override void RenderResult(ref JsonData data)
        {
            DataTable result = View as DataTable; // username, firstname, lastname

            DataRowCollection rows = result.Rows;
            int count = rows.Count;

            object[] suggessions = new object[count];

            for (int i = 0; i < count; i++)
            {
                suggessions[i] = new object[]{
                    HttpUtility.HtmlEncode(rows[i]["username"].ToString()),
                    HttpUtility.HtmlEncode (rows[i]["firstname"].ToString()),
                    HttpUtility.HtmlEncode(rows[i]["lastname"].ToString())
                };
            }

            data.Add("query", query);
            data.Add("suggestions", suggessions);
        }

        string query = string.Empty;
        int custid = 0;
        int issubaccount = 0;
        int excludemember = 0;
        int directdownline = 0;

        public override void ProcessRequest(ref System.Collections.Hashtable p)
        {
            query = Request["query"];
            custid = Request["custid"] == null ? CurrentUserSession.CustId : int.Parse(Request["custid"]);
            // Do not show sub-acc (hard-code SPs of @Thien)
            issubaccount = 0;// Request["issub"] == null ? 0 : int.Parse(Request["issub"]);
            excludemember = Request["ismember"] == null ? 0 : int.Parse(Request["ismember"]);
            directdownline = Request["isdir"] == null ? 0 : int.Parse(Request["isdir"]);
        }

        public override object ValidateRequest(System.Collections.Hashtable p)
        {
            if (string.IsNullOrEmpty(query))
            {
                return new Result(ErrorType.AccessDenied, string.Empty);
            }

            return null;
        }

        public override object InvokeData(System.Collections.Hashtable p)
        {
            AccountInfoBO bo = new AccountInfoBO(CurrentUserSession.NetSessionId);
            return bo.SearchCustomer(custid, query, issubaccount, excludemember, directdownline, 0);
        }
    }

}