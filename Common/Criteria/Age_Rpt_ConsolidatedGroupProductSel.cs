using Fanex.NAP.Framework.Data;

namespace Fanex.Razorbill.Module.Common.Criteria
{
    public class Age_Rpt_ConsolidatedGroupProductSel : CriteriaBase
    {
        public Age_Rpt_ConsolidatedGroupProductSel()
        {
            // Show all.
            IsShow = -1;
        }

        public int CustId { get; set; }

        [SpParam(Ignored = true)]
        public int IsShow { get; set; }

        public override string GetSettingKey()
        {
            return "Age_Rpt_ConsolidatedGroupProductSel";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}