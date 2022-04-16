using System.Collections;
using System.Collections.Generic;
using TatmanGames.Common.Scene;
using TatmanGames.Common.ServiceLocator;
using TatmanGames.ScreenUI.Interfaces;
using TatmanGames.ScreenUI.UI;
using UnityEngine;

namespace CornTheory.UI
{
    /// <summary>
    /// follows the pattern like ScreenUI\Demo\Code\ScreenInitializer
    /// </summary>
    public class MainMenuScreenController : MonoBehaviour
    {
        [SerializeField] private GameObject settingsDialog;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip openSound;
        [SerializeField] private AudioClip closeSound;

        private PopupEventsManager dialogEvents;
        
        private void Start()
        {
            var services = GlobalServicesLocator.Instance;
            dialogEvents = new PopupEventsManager();
            services.AddService<IPopupEventsManager>(dialogEvents);
            services.AddService<IDialogEvents>(dialogEvents);

            IPopupHandler popupHandler = new TatmanGames.ScreenUI.UI.PopupHandler(dialogEvents);
            services.AddService<IPopupHandler>(popupHandler);

            popupHandler.Canvas = GetComponent<Canvas>();
            popupHandler.AudioSource = audioSource;
            popupHandler.OpenSound = openSound;
            popupHandler.CloseSound = closeSound;

            services.AddService<TatmanGames.Common.Interfaces.ILogger>(new DebugLogging());
            
            dialogEvents.OnButtonPressed += DialogEventsOnButtonPressed;
        }
        
        private bool DialogEventsOnButtonPressed(string dialogName, string buttonId)
        {
            TatmanGames.Common.Interfaces.ILogger logger =
                GlobalServicesLocator.Instance.GetService<TatmanGames.Common.Interfaces.ILogger>();
            IPopupHandler popupHandler = GlobalServicesLocator.Instance.GetService<IPopupHandler>();
            
            if ("Canvas" == dialogName && "settings" == buttonId)
            {
                popupHandler.ShowDialog(settingsDialog);
                return true;
            }
            
            if ("quit" == buttonId)
            {
                popupHandler.CloseDialog();
                return true;
            }
            
            logger.Log($"dialog {dialogName} clicked {buttonId}");
            return false;
        }
    }
}
