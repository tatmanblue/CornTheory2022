using System.Collections.Generic;
using TatmanGames.Missions.Interfaces;

namespace TatmanGames.Missions.Demo
{
   
    /**
     * Hardcoded missions for demo purposes
     */
    public class Mission1 : IMission
    {
        public int Id { get; private set; } = 1;
        public int ParentId { get; private set; } = -1;
        public string Uuid { get; private set; } = string.Empty;
        public string Name { get; private set; } = "Mission One";
        public string Description { get; private set; } = "This is the first mission. It does not have any steps.";
        public List<IMissionStep> Steps { get; } = new List<IMissionStep>();

        public bool IsCompleted()
        {
            return true;
        }

        public int CompareTo(object obj)
        {
            IMission compareTo = obj as IMission;
            if (Id > compareTo?.Id) return 1;
            if (Id < compareTo?.Id) return -1;
            return 0;
        }
    }
    
    public class Mission2 : IMission
    {
        public int Id { get; private set; } = 2;
        public int ParentId { get; private set; } = 1;
        public string Uuid { get; private set; } = string.Empty;
        public string Name { get; private set; } = "Mission Two";
        public string Description { get; private set; } = "This is the next mission, after the first one";
        public List<IMissionStep> Steps { get; } = new List<IMissionStep>();

        public Mission2()
        {
            // normally this would be a bad practice but this is a demo and I dont want to
            // create a lot of extra work so we will create the steps here
            Steps.Add(new Mission2Step1());
        }

        public bool IsCompleted()
        {
            return true;
        }
        
        public int CompareTo(object obj)
        {
            IMission compareTo = obj as IMission;
            if (Id > compareTo?.Id) return 1;
            if (Id < compareTo?.Id) return -1;
            return 0;
        }

    }
    
    public class Mission3 : IMission
    {
        public int Id { get; private set; } = 3;
        public int ParentId { get; private set; } = 2;
        public string Uuid { get; private set; } = string.Empty;
        public string Name { get; private set; } = "Mission 3";
        public string Description { get; private set; } = "This is the third mission in the series. And for now its the last but if another one is added this text could be wrong";
        public List<IMissionStep> Steps { get; } = new List<IMissionStep>();
        public bool IsCompleted()
        {
            return true;
        }
        
        public int CompareTo(object obj)
        {
            IMission compareTo = obj as IMission;
            if (Id > compareTo?.Id) return 1;
            if (Id < compareTo?.Id) return -1;
            return 0;
        }

    }

    public class Mission2Step1 : IMissionStep
    {
        public int Id { get; private set; } = 1;
        public int ParentId { get; private set; } = 0;
        public int MissionId { get; } = 2;
        public string Uuid { get; private set; } = string.Empty;
        public string Name { get; private set; } = "Step 1";
        
        public bool ShowUIOnActivate { get; private set; } = true;
        public string Description { get; private set; } = @"Mission has this step to complete before completing the mission.  
               Completing this step will cause the mission to complete and the next mission becomes active.";
        
        public bool IsCompleted()
        {
            return true;
        }
        
        public int CompareTo(object obj)
        {
            IMission compareTo = obj as IMission;
            if (Id > compareTo?.Id) return 1;
            if (Id < compareTo?.Id) return -1;
            return 0;
        }
    }
    
}