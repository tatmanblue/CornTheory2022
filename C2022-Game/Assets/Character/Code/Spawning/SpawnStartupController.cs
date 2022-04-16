using System.Collections;
using System.Collections.Generic;
using TatmanGames.Character.Interfaces;
using TatmanGames.Common;
using TatmanGames.Common.ServiceLocator;
using UnityEngine;
using ILogger = TatmanGames.Common.Interfaces.ILogger;

namespace TatmanGames.Character.Spawning
{
    /// <summary>
    /// applied to a prefab in a scene, an awake it will
    /// find all prefabs with INPCEngine.SpawnTag set up the NPCs
    /// </summary>
    public class SpawnStartupController : MonoBehaviour, ISpawnController
    {
        private ISpawnEngine engine = null;
        private bool hasLoaded = false;

        private void Update()
        {
            if (hasLoaded == true) return;
            hasLoaded = true;
            StartCoroutine("SpawnNpcs");
        }

        private void SpawnNpcs()
        {
            InitializeDefaults();

            engine = GlobalServicesLocator.Instance.GetService<ISpawnEngine>();
            
            // this can seem confusing that we get INpcSpawnController from service locator
            // when this type implements INpcSpawnController.  The reason is 
            // we want to allow consumers to replace the implementation of INpcSpawnController here
            // with a customized solution.  
            // TODO but this still seems hokey
            ISpawnController controller = GlobalServicesLocator.Instance.GetService<ISpawnController>();
            
            GameObject[] respawns = controller.GetAllNpcSpawnPoints();
            if (null == respawns)
                return;
            
            Log($"found {respawns?.Length} spawn points");
            foreach (GameObject pointData in respawns)
            {
                ISpawnPoint data = pointData.GetComponent<ISpawnPoint>();
                if (null == data)
                {
                    Log("failed to find INPCSpawnData");
                    continue;
                }
                
                if (false == controller?.CanSpawnAtStartup(data))
                    continue;
                
                Log($"Spawning {data.Data.Id}");
                engine?.Instantiate(pointData, data);
                
                // TODO is this behavior belonging in the engine?
                if (true == data.Data.DestroyPointOnSpawn)
                    Destroy(pointData);
            }
        }

        /// <summary>
        /// consumers should use another means for initializing which implementations they need.
        /// this is here to ensure something works.
        /// </summary>
        private void InitializeDefaults()
        {
            try
            {
                // because we can't ask if the service exists
                GlobalServicesLocator.Instance.GetService<ISpawnEngine>();
            }
            catch (ServiceLocatorException)
            {
                GlobalServicesLocator.Instance.AddService<ISpawnEngine>(new SpawnEngine());
            }

            try
            {
                GlobalServicesLocator.Instance.GetService<ISpawnController>();
            }
            catch (ServiceLocatorException)
            {
                GlobalServicesLocator.Instance.AddService<ISpawnController>( this);
            }
        }

        private void Log(string msg)
        {
            TatmanGames.Common.Interfaces.ILogger logger = GlobalServicesLocator.Instance.GetService<ILogger>();
            logger.Log(msg);
        }

        public bool CanSpawnAtStartup(ISpawnPoint point)
        {
            return point.Data.SpawnOnStart;
        }

        public GameObject[] GetAllNpcSpawnPoints()
        {
            return GameObject.FindGameObjectsWithTag(engine?.SpawnTag);
        }
    }
}
