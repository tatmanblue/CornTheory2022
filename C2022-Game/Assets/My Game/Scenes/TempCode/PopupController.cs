using System.Collections;
using System.Collections.Generic;
using TatmanGames.Common.ServiceLocator;
using TatmanGames.ScreenUI.Interfaces;
using TatmanGames.ScreenUI.UI;
using UnityEngine;

public class PopupController : MonoBehaviour
{
    [SerializeField] private GameObject settingsDialog;
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

        dialogEvents.OnButtonPressed += DialogEventsOnButtonPressed;
    }
        
    private bool DialogEventsOnButtonPressed(string dialogName, string buttonId)
    {
        IPopupHandler popupHandler = GlobalServicesLocator.Instance.GetService<IPopupHandler>();
            
        if ("settings" == buttonId)
        {
            Debug.Log("opening settings");
            popupHandler.ShowDialog(settingsDialog);
            return true;
        }
            
        if ("quit" == buttonId)
        {
            Debug.Log("closing settings");
            popupHandler.CloseDialog();
            return true;
        }
        
        Debug.Log($"dialog {dialogName} clicked {buttonId}");
        return false;
    }


}
