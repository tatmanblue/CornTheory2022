using TatmanGames.Common.ServiceLocator;
using TatmanGames.Missions.Interfaces;

namespace TatmanGames.Missions
{
    public static class MissionExtensions
    {
        /// <summary>
        /// returns the state of the mission as saved in the IMissionStateAggregator
        /// be aware of the purpose of the IMissionStateAggregator and incorrectly calling this might
        /// yield false
        ///
        /// example:
        /// current mission is #3 in the list, calling this on mission #2 may return false
        /// even though mission 3 cannot be active before mission 2 is complete.
        /// </summary>
        /// <param name="mission"></param>
        /// <returns></returns>
        public static bool CheckIsComplete(this IMission mission)
        {
            IMissionStateAggregator aggregator = GlobalServicesLocator.Instance.GetService<IMissionStateAggregator>();

            // is there any reason we should check that ActiveMission = mission
            return aggregator.IsComplete(mission, null);
        }

        /// <summary>
        /// returns the state of the mission as saved in the IMissionStateAggregator
        /// be aware of the purpose of the IMissionStateAggregator and incorrectly calling this might
        /// yield false
        /// </summary>
        /// <param name="step"></param>
        /// <returns></returns>
        public static bool CheckIsComplete(this IMissionStep step)
        {
            IMissionStateAggregator aggregator = GlobalServicesLocator.Instance.GetService<IMissionStateAggregator>();
            IMissionEngine engine = GlobalServicesLocator.Instance.GetService<IMissionEngine>();
            IMission activeMission = engine.ActiveMission;
            IMissionStep activeStep = engine.ActiveStep;
            
            // is there any reason we should check that ActiveMission = this.Mission
            // and ActiveStep.Id = this.Id
            
            return aggregator.IsComplete(activeMission, activeStep);
        }
    }
}