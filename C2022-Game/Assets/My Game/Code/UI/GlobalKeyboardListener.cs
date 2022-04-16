using System;
using CornTheory.Interfaces;
using TatmanGames.Common.ServiceLocator;
using UnityEngine;

namespace CornTheory.UI
{
    /// <summary>
    /// A global handler for global key strokes like exiting the game
    ///
    /// TODO:  ScreenUI asset contains a global handler (which may or not apply but...)
    /// not sure this will be need with the new changes
    /// </summary>
    [Obsolete("not sure what replaces this", true)]
    public class GlobalKeyboardListener : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ISceneHandler sceneHandler = GlobalServicesLocator.Instance.GetService<ISceneHandler>();
                sceneHandler?.GotoMainMenu();
            }
        }
    }
}