using System;
using TatmanGames.Common.ServiceLocator;
using TatmanGames.Missions.Interfaces;
using TMPro;
using UnityEngine;

namespace TatmanGames.Missions.Demo
{
    public class MissionDialogController : MonoBehaviour
    {
        private void Start()
        {
            IMissionEngine engine = GlobalServicesLocator.Instance.GetService<IMissionEngine>();
            IMission mission = engine.ActiveMission;
           
            SetUIText("MissionName", mission.Name);
            SetUIText("MissionText", mission.Description);

            if (0 == mission.Steps.Count)
            {
                SetUIText("MissionStepText", string.Empty);
                SetUIText("StepText", string.Empty);
                return;
            }

            IMissionStep step = engine.ActiveStep;
            SetUIText("MissionStepText", step.Description);
            SetUIText("StepText", $"{step.Id} of {mission.Steps.Count}");
        }

        private void SetUIText(string componentName, string text)
        {
            var textGameObject = GameObject.Find(componentName);
            TMP_Text textField = textGameObject.GetComponent<TMP_Text>();
            textField.text = text;
        }
    }
}