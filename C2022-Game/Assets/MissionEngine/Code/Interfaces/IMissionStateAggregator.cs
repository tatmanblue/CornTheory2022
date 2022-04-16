namespace TatmanGames.Missions.Interfaces
{
    /// <summary>
    /// Intended to be an inmemory store of mission states.
    ///
    /// Allows IMission and IMissionSteps share their state without requiring those
    /// implementations to actually maintain state since state belongs to the player
    ///
    /// Allows separation of IMissionEngine from state information and from player
    ///
    /// It is not meant to serialize state data such as game save
    /// </summary>
    public interface IMissionStateAggregator
    {
        /// <summary>
        /// sets mission state
        /// </summary>
        /// <param name="mission"></param>
        /// <param name="state"></param>
        void SetCompleteState(IMission mission, bool state);
        /// <summary>
        /// sets mission step state
        /// </summary>
        /// <param name="mission"></param>
        /// <param name="step"></param>
        /// <param name="state"></param>
        void SetCompleteState(IMission mission, IMissionStep step, bool state);
        /// <summary>
        /// allows for query of state. when step is null, the state of the mission is returned
        /// </summary>
        /// <param name="mission"></param>
        /// <param name="step"></param>
        /// <returns></returns>
        bool IsComplete(IMission mission, IMissionStep step);
    }
}