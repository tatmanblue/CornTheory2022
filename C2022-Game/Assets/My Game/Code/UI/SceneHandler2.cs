using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using CornTheory.Interfaces;
using TatmanGames.Common.ServiceLocator;

namespace CornTheory.UI
{
    /// <summary>
    /// Replaces UI\SceneSwitcher
    /// </summary>
    public class SceneHandler2 : MonoBehaviour, ISceneHandler
    {
        private void Awake()
        {
            GlobalServicesLocator.Instance.AddService<ISceneHandler>(this);
        }

        #region ISceneHandler
        public void GotoMainMenu()
        {
            SceneManager.LoadScene(Constants.OpenMenuScene);
        }

        public void GotoMainScene()
        {
            throw new System.NotImplementedException();
        }

        public void Quit()
        {
            // this method is duplicated in Code\ApplicationControl.cs
            Application.Quit();
        }
        #endregion
    }
}