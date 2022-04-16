using System.Collections;
using System.Collections.Generic;
using TatmanGames.Character.Interfaces;
using TatmanGames.Missions.Interfaces;
using UnityEngine;

namespace TatmanGames.Missions.Scriptables
{
    /// <summary>
    /// Ties spawn data to a mission so that mission controller can activate/deactivate the spawn
    /// as needed by the mission progress 
    /// </summary>
    [CreateAssetMenu(fileName = "MissionSpawnData", menuName = "Tatman Games/Mission Spawn Data")]
    public class DialogNpcSpawnData : ScriptableObject, ISpawnData, IMissionSpawnData
    {
        public int Id => id;

        public bool SpawnOnStart => automaticSpawning;
        
        public int MissionId => missionId;

        public int MissionStepId => missionStepId;

        public bool DestroyPointOnSpawn => true;

        public GameObject SpawnableObject => npcAvatar;
        
        [SerializeField] private int id;
        [SerializeField] private GameObject npcAvatar;
        [SerializeField] private bool automaticSpawning;
        [SerializeField] private int missionId = -1;
        [SerializeField] private int missionStepId = -1;

    }
}
