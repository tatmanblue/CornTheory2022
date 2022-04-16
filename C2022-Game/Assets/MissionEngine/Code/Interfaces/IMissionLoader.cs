using System.Collections.Generic;

namespace TatmanGames.Missions.Interfaces
{
    public interface IMissionLoader
    {
        List<IMission> ReadAllMissions();
        IMission ReadMission(int id);
    }
}