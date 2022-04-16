using System;
using TatmanGames.Common.ServiceLocator;
using TatmanGames.Missions.Interfaces;
using UnityEngine;

namespace TatmanGames.Missions.Scriptables
{
    /// <summary>
    /// TODO: there is a circular reference here that I do not like
    /// </summary>
    [CreateAssetMenu(fileName = "MissionStep", menuName = "Tatman Games/Missions/Mission Step")]
    public class MissionStepData : ScriptableObject, IMissionStep
    {
        #region IMission properties
        public int Id => id;

        public int MissionId => (null == mission ? -1 : mission.Id);

        public int ParentId => (null == parent ? -1: parent.Id);

        public string Uuid => uuid;

        public string Name => missionStepName;

        public string Description => description;

        public bool ShowUIOnActivate => showUIOnActivate;

        public bool IsCompleted()
        {
            return this.CheckIsComplete();
        }
        
        public int CompareTo(object obj)
        {
            IMissionStep compare = obj as IMissionStep;
            if (this.Id < compare.Id)
                return -1;
            if (this.Id > compare.Id)
                return 1;

            return 0;
        }
        #endregion
        
        #region Scriptable data

        [SerializeField] private int id = 1;
        [SerializeField] private MissionStepData parent;
        [SerializeField] private MissionData mission;
        [SerializeField] private string uuid = Guid.NewGuid().ToString();
        [SerializeField] private string missionStepName;
        [SerializeField] private bool showUIOnActivate = true;
        [TextArea(3, 10)][SerializeField] private string description;

        #endregion
    }
}