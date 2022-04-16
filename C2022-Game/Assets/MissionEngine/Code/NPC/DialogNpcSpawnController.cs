using TatmanGames.Character;
using TatmanGames.Character.Interfaces;
using TatmanGames.Common.ServiceLocator;
using TatmanGames.Missions.Interfaces;
using UnityEngine;

namespace TatmanGames.Missions.NPC
{
    public class DialogNpcSpawnController : ISpawnController
    {
        public bool CanSpawnAtStartup(ISpawnPoint point)
        {
            ISpawnData data = point.Data as ISpawnData;
            if (null == data)
                return true;

            return data.SpawnOnStart;
        }

        public GameObject[] GetAllNpcSpawnPoints()
        {
            ISpawnEngine engine = GlobalServicesLocator.Instance.GetService<ISpawnEngine>();
            return GameObject.FindGameObjectsWithTag(engine?.SpawnTag);
        }
    }
}