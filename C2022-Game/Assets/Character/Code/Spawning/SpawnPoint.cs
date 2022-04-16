using TatmanGames.Character.Interfaces;
using TatmanGames.Character.Scriptables;
using UnityEngine;

namespace TatmanGames.Character.Spawning
{
    public class SpawnPoint : MonoBehaviour, ISpawnPoint
    {
        public ISpawnData Data
        {
            get
            {
                return spawnData;
            } 
            
        }
        
        [SerializeField] private SpawnData spawnData;
    }
}