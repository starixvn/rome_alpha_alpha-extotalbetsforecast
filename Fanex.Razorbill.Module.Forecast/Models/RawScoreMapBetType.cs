namespace Fanex.Razorbill.Module.Forecast.Models
{
    using System;

    /// <summary>
    /// Score Map raw data.
    /// </summary>
    [Serializable]
    public class RawScoreMapBetType
    {
        public int TypeId { get; set; }

        public int EventType { get; set; }

        public string FirstId { get; set; }

        public string InfoId { get; set; }
    }
}
