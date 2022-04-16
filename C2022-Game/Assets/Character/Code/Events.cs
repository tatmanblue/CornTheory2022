using TatmanGames.Character.Interfaces;
using UnityEngine;

namespace TatmanGames.Character
{
    /// <summary>
    /// a gameobject spawn has been created 
    /// </summary>
    public delegate void GameObjectInstantiated(ISpawnPoint point, GameObject npc);
    /// <summary>
    /// this event occurs after the startup has completed and auto spawn is complete
    /// </summary>
    public delegate void GameObjectAutoSpawningCompleted();
}