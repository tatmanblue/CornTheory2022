using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TatmanGames.Character.Interfaces
{
    /// <summary>
    /// This is the data the engine and controller will use to initialize and place
    /// an something into in the scene.
    ///
    /// TODO: not sure this should live in the character namespace any more
    /// </summary>
    public interface ISpawnData
    {
        /// <summary>
        /// an Id for the instance
        /// keep it unique so that consumers can identity the npc instance easily
        /// </summary>
        int Id { get; }
        bool SpawnOnStart { get; }
        bool DestroyPointOnSpawn { get; }
        GameObject SpawnableObject { get; }
    }
}
