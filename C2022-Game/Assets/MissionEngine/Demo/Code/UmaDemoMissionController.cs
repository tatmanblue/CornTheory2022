using System;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using TatmanGames.Character.Interfaces;
using TatmanGames.Common;
using TatmanGames.Common.Scene;
using TatmanGames.Common.ServiceLocator;
using TatmanGames.Missions.Interfaces;
using TatmanGames.Missions.NPC;
using TatmanGames.Missions.Scriptables;
using TatmanGames.ScreenUI.Interfaces;
using TatmanGames.ScreenUI.UI;
using ILogger = TatmanGames.Common.Interfaces.ILogger;

namespace TatmanGames.Missions.Demo
{
    /// <summary>
    /// demos a controller initializing and interacting with the mission engine
    ///
    /// it seems plausable in production the MissionLoader would be a separate type
    /// </summary>
    public class UmaDemoMissionController : MonoBehaviour, IMissionLoader
    {
        [SerializeField] private List<MissionData> missions = new List<MissionData>();
        [SerializeField] private TMP_Text missionState;
        [SerializeField] private GameObject missionDialog;
        [SerializeField] private Canvas canvas;
        
        private IMissionEngine engine = null;
        private IPopupHandler popupHandler = null;
        private void Awake()
        {
            var dialogEvents = new PopupEventsManager();
            
            ServicesLocator services = GlobalServicesLocator.Instance;
            popupHandler = new PopupHandler(dialogEvents);
            popupHandler.Canvas = canvas;
            
            services.AddService<IPopupHandler>(popupHandler);
            services.AddService<IPopupEventsManager>(dialogEvents);
            services.AddService<IDialogEvents>(dialogEvents);
            services.AddReplaceService<ILogger>(new DebugLogging());
            
            dialogEvents.OnButtonPressed += DialogEventsOnButtonPressed;
            
            GlobalServicesLocator.Instance.AddReplaceService<ISpawnController>(new DialogNpcSpawnController());
            
            try
            {
                engine = GlobalServicesLocator.Instance.GetService<IMissionEngine>();
            }
            catch (ServiceLocatorException)
            {
                if (null == engine)
                {
                    engine = new MissionEngine();
                    GlobalServicesLocator.Instance.AddService<IMissionEngine>(engine);
                }
            }

            GlobalServicesLocator.Instance.AddReplaceService<IMissionStateAggregator>(new MissionStateAggregator());
            GlobalServicesLocator.Instance.AddReplaceService<IMissionLoader>(this);
            GlobalServicesLocator.Instance.AddReplaceService<IMissionPlayerData>(new DemoPlayerData());

            engine.OnEngineInitialized += OnMissionEngineInitialized;
            engine.OnMissionEngineStopped += OnMissionEngineStopped;
            engine.OnMissionStarted += OnMissionStarted;
            engine.OnMissionStepStarted += OnMissionStepStarted;
            engine.OnMissionStepCompleted += OnMissionStepCompleted;
            engine.OnAllMissionStepsCompleted += OnAllMissionStepsCompleted;
            engine.OnMissionCompleted += OnMissionCompleted;
            engine.Initialize();
            
        }

        private void OnDestroy()
        {
            // todo: could order of unsubscription cause a problem?
            engine.OnEngineInitialized -= OnMissionEngineInitialized;
            engine.OnMissionEngineStopped -= OnMissionEngineStopped;
            engine.OnMissionStarted -= OnMissionStarted;
            engine.OnMissionStepStarted -= OnMissionStepStarted;
            engine.OnMissionStepCompleted -= OnMissionStepCompleted;
            engine.OnAllMissionStepsCompleted -= OnAllMissionStepsCompleted;
            engine.OnMissionCompleted -= OnMissionCompleted;
        }

        #region dialog events
        private bool DialogEventsOnButtonPressed(string dialogName, string buttonId)
        {
            if (true == dialogName.Contains("MissionDialog") && true == buttonId.Contains("proceed"))
            {
                SetMissionMessage($"mission screen proceed detected. {dialogName} {buttonId}");
                GlobalServicesLocator.Instance.GetService<IPopupHandler>()?.CloseDialog();
            }

            return false;
        }
        #endregion
        
        #region Mission Events
        private void OnMissionCompleted(IMission m)
        {
            SetMissionMessage($"mission {m.Name}({m.Id}) completed.");
        }

        private void OnMissionStepCompleted(IMissionStep s)
        {
            SetMissionMessage($"mission {s.Name}({s.Id}/{s.MissionId}) completed.");
        }

        private void OnAllMissionStepsCompleted(IMission m)
        {
            SetMissionMessage($"All steps for mission {m.Name} completed.");
            IMissionStateAggregator aggregator = GlobalServicesLocator.Instance.GetService<IMissionStateAggregator>();
            aggregator?.SetCompleteState(engine?.ActiveMission, true);
            engine?.CompleteActiveMission();
        }

        private void OnMissionStepStarted(IMissionStep s)
        {
            SetMissionMessage($"mission {s.Name}({s.MissionId}.{s.Id}) started. Dialogs is {s.ShowUIOnActivate}.");
            if (true == s.ShowUIOnActivate)
                ShowMissionDialog(null, s);
            
            // don't like this but this is the mission controller so it would be
            // entry point for making decisions like this below.  
            // TODO: rethink who owns this
            if (s.Id == 1 && s.MissionId == 2)
            {
                ISpawnController controller = GlobalServicesLocator.Instance.GetService<ISpawnController>();
                foreach (GameObject pointData in controller.GetAllNpcSpawnPoints())
                {
                    ISpawnPoint point = pointData.GetComponent<ISpawnPoint>();
                    ISpawnData data = point.Data;
                    IMissionSpawnData missionData = data as IMissionSpawnData;
                    if (null == missionData)
                        continue;

                    if (missionData.MissionId != s.MissionId)
                        continue;
                    if (missionData.MissionStepId != s.Id)
                        continue;

                    ISpawnEngine spawnEngine = GlobalServicesLocator.Instance.GetService<ISpawnEngine>();
                    spawnEngine.Instantiate(pointData, point);
                    if (true == data.DestroyPointOnSpawn)
                        Destroy(pointData);
                    break;
                }
            }
        }

        private void OnMissionStarted(IMission m)
        {
            SetMissionMessage($"mission {m.Name}({m.Id}) started.");
            if (0 == m.Steps.Count)
                ShowMissionDialog(m, null);
        }

        private void OnMissionEngineStopped()
        {
            SetMissionMessage("mission engine stopped");
        }

        private void OnMissionEngineInitialized()
        {
            SetMissionMessage("mission engine initialized");
            IMissionPlayerData playerData =
                GlobalServicesLocator.Instance.GetService<IMissionPlayerData>();
            
            playerData.Initialize();
        }
        #endregion

        #region Private methods
        private void SetMissionMessage(string msg)
        {
            if (null == missionState)
                return;

            ILogger logger = GlobalServicesLocator.Instance.GetService<ILogger>();
            logger.Log(msg);
            missionState.text = msg;
        }

        public void ShowMissionDialog(IMission m, IMissionStep s)
        {
            if (null == s)
                SetMissionMessage("showing mission dialog");
            else
                SetMissionMessage("Showing mission dialog with step");
            
            popupHandler.ShowDialog(missionDialog);
        }
        #endregion

        public List<IMission> ReadAllMissions()
        {
            List<IMission> list = new List<IMission>();

            foreach (var step in missions)
            {
                list.Add(step);
            }
            return list;
        }

        public IMission ReadMission(int id)
        {
            return missions.Find(m => m.Id == id);
        }
    }
}