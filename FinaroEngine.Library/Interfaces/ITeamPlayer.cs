using System;
using System.Collections.Generic;
using System.Text;

namespace FinaroEngine.Library
{
    public interface ITeamPlayer
    {
        IEnumerable<TeamPlayer> GetTeamPlayers(int entityTypeId, int entityLeagueId);
    }
}
