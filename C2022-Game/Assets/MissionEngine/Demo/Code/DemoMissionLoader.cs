using System;
using System.Collections.Generic;
using TatmanGames.Missions.Interfaces;

namespace TatmanGames.Missions.Demo
{
    /**
     * for demo only, we have hardcoded missions so we just new them here.
     * more than likely an production loader would get data from a serialized
     * source such as DB or json files etc....
     */
    public class DemoMissionLoader : IMissionLoader
    {

        public List<IMission> ReadAllMissions()
        {
            List<IMission> results = new List<IMission>();

            results.Add(new Mission1());
            results.Add(new Mission2());
            results.Add(new Mission3());
            return results;
        }

        public IMission ReadMission(int id)
        {
            switch (id)
            {
                case 1:
                    return new Mission1();
                case 2:
                    return new Mission2();
                case 3:
                    return new Mission3();
                default:
                    break;
            }

            throw new ApplicationException($"mission {id} isn't defined in demo app");
        }
    }
}