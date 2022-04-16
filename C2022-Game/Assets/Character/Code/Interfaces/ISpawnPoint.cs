namespace TatmanGames.Character.Interfaces
{

    /// <summary>
    /// this goes on a gameobject in the scene to represent location
    /// an npc should appear.  Ideally the gameobject is just an empty gameobject.
    /// The location of the gameobject will be the location of the Npc created.
    ///
    /// INpcSpawnData data source for the data configuring
    /// the Npc.  In this library, a ScriptableObject is the INpcSpawnData
    /// </summary>
    public interface ISpawnPoint
    {
        ISpawnData Data { get; }
    }
}