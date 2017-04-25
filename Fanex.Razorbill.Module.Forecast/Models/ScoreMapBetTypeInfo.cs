namespace Fanex.Razorbill.Module.Forecast.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Score Map bet type information.
    /// </summary>
    public class ScoreMapBetTypeInfo
    {
        public string BetType { get; set; }

        public IList<string> FollowedBetTypes { get; set; }
    }
}
