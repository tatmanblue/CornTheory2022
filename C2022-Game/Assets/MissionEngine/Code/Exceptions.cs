using System;

namespace TatmanGames.Missions
{
    public class MissionEngineError : Exception
    {
        public MissionEngineError(string msg) : base(msg)
        {
        }
    }
}