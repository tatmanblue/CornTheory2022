using System;
using System.Collections.Generic;
using TatmanGames.Missions.Interfaces;

namespace TatmanGames.Missions
{ /// <summary>
    /// Intended to be an in-memory store of mission states.
    ///
    /// Allows IMission and IMissionSteps share their state without requiring those
    /// implementations to actually maintain state since state belongs to the player
    ///
    /// Allows separation of IMissionEngine from state information
    ///
    /// It is not meant to serialize state data
    ///
    /// It is not meant to understand the state of missions already completed once the in-memory
    /// data is refreshed (such as on game load)
    ///
    /// uses internal StateKey type, defined below
    /// </summary>
    public class MissionStateAggregator : IMissionStateAggregator
    {
        private Dictionary<StateKey, bool> stepStates = new Dictionary<StateKey, bool>();
        private Dictionary<IMission, bool> missionStates = new Dictionary<IMission, bool>();

        private void SetMissionStepState(IMission mission, IMissionStep step, bool state)
        {
            StateKey stateKey = new StateKey() { Mission = mission, Step = step };
            if (true == stepStates.ContainsKey(stateKey))
            {
                stepStates[stateKey] = state;
                return;
            }

            stepStates.Add(stateKey, state);
        }

        private void SetMissionState(IMission mission, bool state)
        {
            if (true == missionStates.ContainsKey(mission))
            {
                missionStates[mission] = state;
            }
            
            missionStates.Add(mission, state);
        }

        public void SetCompleteState(IMission mission, bool state)
        {
            SetMissionState(mission, state);
        }
        
        public void SetCompleteState(IMission mission, IMissionStep step, bool state)
        {
            if (null == mission)
                throw new MissionEngineError("mission arguments cannot be null");

            if (null != step)
            {
                SetMissionStepState(mission, step, state);
                return;
            }
            
            SetMissionState(mission, state);
        }

        public bool IsComplete(IMission mission, IMissionStep step)
        {
            if (null == mission)
                throw new MissionEngineError("mission arguments cannot be null");

            if (null != step)
            {
                StateKey stateKey = new StateKey() { Mission = mission, Step = step };
                if (false == stepStates.ContainsKey(stateKey))
                    return false;

                return stepStates[stateKey];
            }

            if (false == missionStates.ContainsKey(mission))
                return false;

            return missionStates[mission];
        }
    }
    
    
    /// <summary>
    /// used by MissionStateAggregator for creating a key for a Dictionary when
    /// information is pertaining to a IMissionStep
    /// </summary>
    internal class StateKey: IComparable
    {
        public IMission Mission { get; set; }
        public IMissionStep Step { get; set; }
        
        /// <summary>
        /// compares Mission.Id and Step.Id
        /// </summary>
        /// <param name="obj">StateKey or exception will be thrown</param>
        /// <returns>0 when both are equal</returns>
        public int CompareTo(object obj)
        {
            StateKey compare = obj as StateKey;
            if (compare.Mission.Id < Mission.Id)
                return -1;
            if (compare.Mission.Id > Mission.Id)
                return 1;
            if (null != Step)
            {
                if (compare.Step.Id < Step.Id)
                    return -1;
                if (compare.Step.Id > Step.Id)
                    return 1;
                
            }
            return 0;
        }

        public override int GetHashCode()
        {
            return Step.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            StateKey compare = obj as StateKey;
            if (0 == this.CompareTo(compare))
                return true;

            return false;
        }
    }

}