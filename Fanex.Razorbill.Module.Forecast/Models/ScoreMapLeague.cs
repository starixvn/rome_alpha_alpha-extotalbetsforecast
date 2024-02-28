namespace Fanex.Razorbill.Module.Forecast.Models
{
    using Fanex.Razorbill.Module.Core.Services.Impl;
    using AlphaPlus.Configurations;

    /// <summary>
    /// Score Map League.
    /// </summary>
    public class ScoreMapLeague
    {
        public ScoreMapLeague(int id, int leagueGroupId)
            : this(id, leagueGroupId, null)
        {
        }

        public ScoreMapLeague(int id, int leagueGroupId, string name)
        {
            Id = id;
            LeagueGroupId = leagueGroupId;
            var leagueName = AppSettings.IsSiteLabel777 ?
            CacheRef.GetLeagueName(Id, leagueGroupId) :
            CacheRef.GetLeagueName(Id);
            Name = name ?? leagueName;
        }

        public int Id { get; set; }

        public int LeagueGroupId { get; set; }

        public string Name { get; private set; }

        public int SportId { get; set; }
    }
}
