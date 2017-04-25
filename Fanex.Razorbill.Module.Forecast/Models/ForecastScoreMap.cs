namespace Fanex.Razorbill.Module.Forecast.Models
{
    using Fanex.Razorbill.Module.Core.Services.Impl;

    /// <summary>
    /// Forecast Score Map.
    /// </summary>
    public class ForecastScoreMap
    {
        public ForecastScoreMap(int row, int column) 
        {
            RowNumber = row;
            ColumnNumber = column;
        }

        public int MatchId { get; set; }

        public int HomeId { get; set; }

        public string HomeName
        {
            get
            {
                return CacheRef.GetTeamName(HomeId);
            }
        }

        public int AwayId { get; set; }

        public string AwayName
        {
            get
            {
                return CacheRef.GetTeamName(AwayId);
            }
        }

        public int RowNumber { get; private set; }

        public int ColumnNumber { get; private set; }

        public int[,] HaftTime { get; set; }

        public int[,] FullTime { get; set; }
    }
}
