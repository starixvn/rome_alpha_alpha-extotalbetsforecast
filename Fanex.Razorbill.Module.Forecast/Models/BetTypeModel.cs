namespace Fanex.Razorbill.Module.Forecast.Models
{
    using System;
    using Fanex.Razorbill.Module.Common.Helpers;
    using Fanex.Razorbill.Module.Forecast.Resources;

    /// <summary>
    /// Bet type modal.
    /// </summary>
    [Serializable]
    public class BetTypeModel
    {
        public BetTypeModel(int id)
            : this(id, null)
        { 
        }

        public BetTypeModel(int id, string name)
        {
            Id = id;
            if (name == null)
            {
                Name = id == 5 ? ForecastLang.BetType1x2 : CacheRemotingRef.GetBettype(Id.ToString());
            }
            else
            {
                Name = name;
            }
        }

        public int Id { get; private set; }

        public string Name { get; private set; }
    }
}
