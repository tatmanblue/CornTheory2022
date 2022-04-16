using TatmanGames.Common.ServiceLocator;
using TatmanGames.Missions.Interfaces;

namespace TatmanGames.Missions.Demo
{
    public class DemoPlayerData : IMissionPlayerData
    {
        public int ActiveMissionId { get; set; } = 1;

        public int ActiveMissionStepId { get; set; } = 0;

        public void Initialize()
        {
            IMissionEngine engine = GlobalServicesLocator.Instance.GetService<IMissionEngine>();
            IMission mission = engine.AllMissions.Find(m => m.Id == ActiveMissionId);
            if (0 == ActiveMissionStepId && 0 < mission.Steps.Count)
                ActiveMissionStepId = 1;
            
            engine.StartMissions();
        }
    }
}