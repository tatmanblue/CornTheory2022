using UnityEngine;

namespace TatmanGames.Character.Interfaces
{
    public interface ISpawnController
    {
        bool CanSpawnAtStartup(ISpawnPoint point);
        GameObject[] GetAllNpcSpawnPoints();
    }
}