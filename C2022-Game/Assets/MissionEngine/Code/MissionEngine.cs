using System.Collections.Generic;
using TatmanGames.Common.ServiceLocator;
using TatmanGames.Missions.Interfaces;

namespace TatmanGames.Missions
{
    public class MissionEngine : IMissionEngine
    {
        public MissionEngineStates State { get; private set; } = MissionEngineStates.Stopped;
        public IMission ActiveMission { get; private set; } = null;
        public IMissionStep ActiveStep { get; private set; } = null;
        public List<IMission> AllMissions { get; private set; }
        
        public event MissionStarted OnMissionStarted;
        public event MissionStepStarted OnMissionStepStarted;
        public event MissionPostStarted OnMissionPostStarted;
        public event MissionStepCompleted OnMissionStepCompleted;
        public event AllMissionStepsCompleted OnAllMissionStepsCompleted;
        public event MissionCompleted OnMissionCompleted;
        public event MissionEngineInitialized OnEngineInitialized;
        public event MissionEngineStopped OnMissionEngineStopped;
        
        public void Initialize()
        {
            IMissionLoader loader = GlobalServicesLocator.Instance.GetService<IMissionLoader>();
            
            AllMissions = loader?.ReadAllMissions();
            AllMissions?.Sort();
            FireEngineInitialized();
        }

        public void StartMissions()
        {
            IMissionLoader loader = GlobalServicesLocator.Instance.GetService<IMissionLoader>();
            IMissionPlayerData playerData =
                GlobalServicesLocator.Instance.GetService<IMissionPlayerData>(); 
            if (null == playerData)
                throw new MissionEngineError("there is no PlayerData to initialize mission engine");
            
            IMission activeMission = loader?.ReadMission(playerData.ActiveMissionId);
            if (null == activeMission)
                return;

            ActiveMission = AllMissions.Find(m => m.Id == activeMission.Id);
            if (null != ActiveMission)
            {
                FireMissionStarted();
                ProcessActiveMission(playerData.ActiveMissionStepId);
            }
        }
        
        public void CompleteActiveMission()
        {
            if (null == ActiveMission)
                throw new MissionEngineError("there is no active mission");
            
            if (false == ActiveMission?.IsCompleted())
                throw new MissionEngineError($"mission id {ActiveMission.Id} is not complete.");
            
            // this is pretty powerful function, theres no checks in place
            FireMissionCompleted();
            
            // get next mission and make it the active one
            int activeMissionIndex = AllMissions.IndexOf(ActiveMission) + 1;
            if (activeMissionIndex >= AllMissions.Count)
            {
                ActiveMission = null;
                ActiveStep = null;
                FireMissionEngineStopped();
                return;
            }

            ActiveMission = AllMissions[activeMissionIndex];
            FireMissionStarted();
            ProcessActiveMission();
        }

        public void CompleteActiveMissionStep()
        {
            if (null == ActiveStep)
                throw new MissionEngineError("there is not an active mission step");

            if (false == ActiveStep?.IsCompleted())
                throw new MissionEngineError(
                    $"mission step {ActiveStep.Id} of mission {ActiveMission.Id} is not complete");

            FireMissionStepCompleted();
            int activeStepIndex = ActiveMission.Steps.IndexOf(ActiveStep) + 1;
            if (activeStepIndex >= ActiveMission.Steps.Count)
            {
                ActiveStep = null;
                FireAllMissionStepsCompleted();
                return;
            }

            ActiveStep = ActiveMission.Steps[activeStepIndex];
            FireMissionStepStarted();
        }
        
        private void ProcessActiveMission(int stepId = 1)
        {

            if (0 == ActiveMission.Steps.Count) return;

            ActiveStep = ActiveMission.Steps.Find(s =>
                s.Id == stepId);

            if (null == ActiveStep) return;

            FireMissionStepStarted();
            
        }

        private void FireMissionStarted()
        {
            MissionStarted started = OnMissionStarted;
            if (null == started) return;

            started(ActiveMission);
        }

        private void FireMissionStepStarted()
        {
            MissionStepStarted stepStarted = OnMissionStepStarted;
            if (null == stepStarted) return;

            stepStarted(ActiveStep);
        }

        private void FireMissionPostStarted(IMission m, IMissionStep s)
        {
            MissionPostStarted postStart = OnMissionPostStarted;
            if (null == postStart) return;
            postStart(m, s);
        }

        private void FireMissionCompleted()
        {
            MissionCompleted completed = OnMissionCompleted;
            if (null == completed) return;

            completed(ActiveMission);
        }
        
        private void FireMissionStepCompleted()
        {
            MissionStepCompleted stepCompleted = OnMissionStepCompleted;
            if (null == stepCompleted) return;

            stepCompleted(ActiveStep);
        }

        private void FireAllMissionStepsCompleted()
        {
            AllMissionStepsCompleted all = OnAllMissionStepsCompleted;
            if (null == all)
                return;

            all(ActiveMission);
        }

        private void FireEngineInitialized()
        {
            MissionEngineInitialized initialized = OnEngineInitialized;

            if (null == initialized) return;

            State = MissionEngineStates.Started;
            initialized();
        }
        
        private void FireMissionEngineStopped()
        {
            MissionEngineStopped stopped = OnMissionEngineStopped;
            if (null == stopped) return;
            State = MissionEngineStates.Stopped;
            stopped();
        }
    }
}