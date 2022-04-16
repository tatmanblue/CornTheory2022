using TatmanGames.Character.Interfaces;
using UnityEngine;

namespace TatmanGames.Character.Spawning
{
    public class SpawnEngine : ISpawnEngine
    {
        public string SpawnTag { get; private set; } = "SpawnPoint";
        public event GameObjectInstantiated OnNpcInstantiated;
        public event GameObjectAutoSpawningCompleted OnAutoSpawningComplete;

        public GameObject Instantiate(GameObject centersOn, ISpawnPoint point)
        {
            GameObject npc = GameObject.Instantiate(point.Data.SpawnableObject, centersOn.transform.position, centersOn.transform.rotation);
            FireOnInstantiated(point, npc);
            return npc;
        }

        private void FireOnInstantiated(ISpawnPoint point, GameObject npc)
        {
            GameObjectInstantiated instantiated = OnNpcInstantiated;
            if (null == instantiated)
                return;

            instantiated(point, npc);
        }

        private void FireAutoSpawningComplete()
        {
            GameObjectAutoSpawningCompleted completed = OnAutoSpawningComplete;
            if (null == completed)
                return;

            completed();
        }
    }
}