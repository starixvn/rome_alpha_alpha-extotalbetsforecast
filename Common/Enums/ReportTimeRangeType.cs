namespace Fanex.Razorbill.Module.Common.Enums
{
    public enum ReportTimeRangeType
    {
        /// <summary>
        /// Default date.
        /// </summary>
        Unknown = 0,

        /// <summary>
        ///  From the latest BalanceUp date.
        /// </summary>
        BalanceUp = 1,

        /// <summary>
        /// First day of previous 3 Months.
        /// </summary>
        Previous3Months = 2,

        /// <summary>
        /// From 05/20/2013 (special case for report FB Financial).
        /// </summary>
        FBFinancial = 3,

        /// <summary>
        /// First date of previous 18 Months.
        /// </summary>
        Previous18Months = 4,

        /// <summary>
        /// First date of first month of previous 5 Years.
        /// </summary>
        Previous5Years = 5
    }
}