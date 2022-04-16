using System;
using System.Collections.Generic;
using TatmanGames.Missions.Interfaces;
using UnityEngine;

namespace TatmanGames.Missions.Scriptables
{
    [CreateAssetMenu(fileName = "Mission", menuName = "Tatman Games/Missions/Mission")]
    public class MissionData : ScriptableObject, IMission
    {
        #region IMission properties
        public int Id => id;

        public int ParentId => (null == parent ? -1: parent.Id);

        public string Uuid => uuid;

        public string Name => missionName;

        public string Description => description;

        public List<IMissionStep> Steps
        {
            get
            {
                return GetSteps();
            }
        }
        
        public bool IsCompleted()
        {
            return this.CheckIsComplete();
        }
        
        public int CompareTo(object obj)
        {
            IMission compare = obj as IMission;
            if (this.Id < compare.Id)
                return -1;
            if (this.Id > compare.Id)
                return 1;

            return 0;
        }
        #endregion
        
        #region Scriptable data

        [SerializeField] private int id = 1;
        [SerializeField] private MissionData parent;
        [SerializeField] private string uuid = Guid.NewGuid().ToString();
        [SerializeField] private string missionName;
        [TextArea(3, 10)][SerializeField] private string description;
        [SerializeField] private List<MissionStepData> steps = new List<MissionStepData>();

        #endregion

        private List<IMissionStep> GetSteps()
        {
            List<IMissionStep> list = new List<IMissionStep>();

            foreach (var step in steps)
            {
                list.Add(step);
            }
            return list;
        }
    }
}