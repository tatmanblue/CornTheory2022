
using UnityEngine;

namespace TatmanGames.Character.Interfaces
{
    /// <summary>
    ///
    /// </summary>
    public interface ISpawnEngine
    {
        /// <summary>
        /// this is value is the same as a "tag" applied to gameobjects in the unity engine
        /// </summary>
        string SpawnTag { get; }
        event GameObjectInstantiated OnNpcInstantiated;
        event GameObjectAutoSpawningCompleted OnAutoSpawningComplete;
        
        GameObject Instantiate(GameObject centersOn, ISpawnPoint point);
    }
}