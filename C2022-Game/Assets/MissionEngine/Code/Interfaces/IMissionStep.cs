using System;

namespace TatmanGames.Missions.Interfaces
{
    /// <summary>
    /// inherit the IMission so that we have a distinct type
    /// (not sure if this is the best approach)
    /// </summary>
    public interface IMissionStep : IComparable
    {
        int Id { get; }
        int ParentId { get; }
        int MissionId { get; }
        string Uuid { get; }
        string Name { get; }
        string Description { get; }
        bool ShowUIOnActivate { get; }

        /// <summary>
        /// allows references to query state of the mission step
        /// Todo is this needed now that we have extension methods
        /// </summary>
        /// <returns></returns>
        bool IsCompleted();
    }
}