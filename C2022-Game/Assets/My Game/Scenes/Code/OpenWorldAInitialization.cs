using System.Runtime.Serialization;
using TatmanGames.Common;
using UnityEngine;

using TatmanGames.Common.Scene;
using TatmanGames.Common.ServiceLocator;
using TatmanGames.DebugUI;
using TatmanGames.DebugUI.Commands;
using TatmanGames.DebugUI.Interfaces;

public class OpenWorldAInitialization : MonoBehaviour
{
    private void Start()
    {
        try
        {
            TatmanGames.Common.Interfaces.ILogger logger = GlobalServicesLocator.Instance.GetService<TatmanGames.Common.Interfaces.ILogger>();
        }
        catch (ServiceLocatorException)
        {
            GlobalServicesLocator.Instance.AddService<TatmanGames.Common.Interfaces.ILogger>(new DebugLogging());
        }

        IDebugEngine engine = null;
        try
        {
            engine = GlobalServicesLocator.Instance.GetService<IDebugEngine>();
        }
        catch(ServiceLocatorException) 
        {
            engine = new CommandEngine();
            GlobalServicesLocator.Instance.AddService<IDebugEngine>(engine);
        }
        
        engine.OnStateChange += EngineOnStateChange;
            
        // initialize commands built into TatmanGames.DebugUI.Commands
        Registration.Initialize();
    }

    private void EngineOnStateChange(DebugCommandWindowState state)
    {
        TatmanGames.Common.Interfaces.ILogger logger = GlobalServicesLocator.Instance.GetService<TatmanGames.Common.Interfaces.ILogger>();
        logger?.Log($"Debug Console state is {state}");
        /*
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (state == DebugCommandWindowState.Opened)
            player.GetComponent<CharacterController>().enabled = false;
        else
            player.GetComponent<CharacterController>().enabled = true;
        */
    }
}
