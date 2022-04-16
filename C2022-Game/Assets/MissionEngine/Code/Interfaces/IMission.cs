using System;
using System.Collections.Generic;

namespace TatmanGames.Missions.Interfaces
{
    public interface IMission : IComparable
    {
        /// <summary>
        /// ID for this mission, should be unique so you can write code to react to events
        /// specific for the mission but system doesn't enforce it.
        ///
        /// The system orders events by this id, from lowest to highest and lowest being the
        /// first mission
        /// </summary>
        int Id { get; }
        /// <summary>
        /// id of a mission that is the "parent", again used for reference
        /// </summary>
        int ParentId { get; }
        /// <summary>
        /// another way to uniquely id missions
        /// </summary>
        string Uuid { get; }
        /// <summary>
        /// name of the mission, should be text thats legible to the player 
        /// </summary>
        string Name { get; }
        /// <summary>
        /// details of the mission, should be text thats legible to the player
        /// </summary>
        string Description { get; }
        /// <summary>
        /// ordered steps for the mission. 
        /// </summary>
        List<IMissionStep> Steps { get; }

        /// <summary>
        /// logic for determining if the mission is complete.  Generally all steps
        /// should be complete before the mission is complete.
        ///
        /// "Complete" is determined by other parts of the game
        ///
        /// Todo is this needed now that we have extension methods
        /// </summary>
        /// <returns></returns>
        bool IsCompleted();
    }
}