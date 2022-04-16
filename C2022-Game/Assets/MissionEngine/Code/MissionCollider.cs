using System;
using TatmanGames.Common.ServiceLocator;
using TatmanGames.Missions.Interfaces;
using TatmanGames.Missions.Scriptables;
using UnityEngine;

namespace TatmanGames.Missions
{
    /// <summary>
    /// When this collider triggers, it means the player has entered an area that is part of a mission.
    ///
    /// The assumption is this trigger ends a mission step (or a mission).
    /// </summary>
    public class MissionCollider : MonoBehaviour
    {
        [SerializeField] private MissionData mission;
        [SerializeField] private MissionStepData step;
        
        private string colliderName = string.Empty;

        private void Awake()
        {
            colliderName = this.gameObject.name;
        }

        private void OnTriggerEnter(Collider collider)
        {
            HandleTrigger();
        }

        private void OnCollisionEnter(Collision collision)
        {
            HandleTrigger();
        }

        private void HandleTrigger()
        {
            IMissionEngine engine = GlobalServicesLocator.Instance.GetService<IMissionEngine>();
            IMissionStateAggregator aggregator = GlobalServicesLocator.Instance.GetService<IMissionStateAggregator>();
            
            Debug.Log($"{mission.Name} and {step.Name} triggered");
            aggregator.SetCompleteState(mission as IMission, step as IMissionStep, true);
            engine.CompleteActiveMissionStep();
            
            Destroy(this.gameObject);
        }
    }
}