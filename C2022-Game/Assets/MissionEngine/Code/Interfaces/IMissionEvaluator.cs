namespace TatmanGames.Missions.Interfaces
{
    public interface IMissionEvaluator
    {
        /// <summary>
        /// logic for determining if the mission is complete.  Generally all steps
        /// should be complete before the mission is complete.
        ///
        /// "Complete" is determined by other parts of the game
        /// </summary>
        /// <returns></returns>
        bool IsCompleted();
    }
}