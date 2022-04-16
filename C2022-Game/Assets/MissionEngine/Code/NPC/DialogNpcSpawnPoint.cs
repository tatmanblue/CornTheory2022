using TatmanGames.Character.Interfaces;
using TatmanGames.Missions.Scriptables;
using UnityEngine;

namespace TatmanGames.Missions.NPC
{
    public class DialogNpcSpawnPoint : MonoBehaviour, ISpawnPoint
    {
        public ISpawnData Data 
        {
            get
            {
                return spawnData;
            }
        }
        
        [SerializeField] private DialogNpcSpawnData spawnData;
    }
}