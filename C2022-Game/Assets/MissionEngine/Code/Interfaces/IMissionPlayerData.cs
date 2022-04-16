namespace TatmanGames.Missions.Interfaces
{
    /// <summary>
    /// This represents the read side of reading player data on scene load.
    /// </summary>
    public interface IMissionPlayerData
    {
        int ActiveMissionId { get; }   
        int ActiveMissionStepId { get; }

        void Initialize();
    }
}