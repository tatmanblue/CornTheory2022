namespace TatmanGames.Missions.Interfaces
{
    public interface IMissionStepEvaluator
    {
        /// <summary>
        /// Evaluates if a step can be considered completed
        /// </summary>
        /// <returns></returns>
        bool IsCompleted();
    }
}