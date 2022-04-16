using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TatmanGames.Missions.Demo
{
    /// <summary>
    /// 
    /// </summary>
    public class UIBehaviors : MonoBehaviour
    {
        private bool handlingKey = false;
        [SerializeField] private TMP_Text Text;
        
        // Update is called once per frame
        private void Update()
        {
            ShowKeyPressMessage();
            
            if (true == Input.GetKey(KeyCode.Escape))
            {
                if (SceneManager.GetActiveScene().name.Contains("Menu"))
                    Application.Quit(0);
                else
                    SceneManager.LoadScene("Menu");
            }

            if (true == Input.GetKey(KeyCode.Alpha1) && 
                true == SceneManager.GetActiveScene().name.Contains("Menu"))
            {
                ShowScene(1);
            }

            
            if (true == Input.GetKey(KeyCode.Alpha2) && 
                true == SceneManager.GetActiveScene().name.Contains("Menu"))
            {
                ShowScene(2);
            }
            
            if (true == Input.GetKey(KeyCode.Space) && 
                true == Input.GetKey(KeyCode.Return) &&
                false == handlingKey)
            {
                handlingKey = true;
                Debug.Log("got space + enter");
                StartCoroutine("ChangeScreen");
            }
        }

        private IEnumerator ChangeScreen()
        {
            Screen.fullScreen = !Screen.fullScreen;
            yield return null;
            handlingKey = false;
            yield return null;
        }

        private void ShowKeyPressMessage()
        {
            if (false == Input.anyKey)
            {
                if (Text != null)
                    Text.text = string.Empty;

                return;
            }    
            
            if (true == Input.GetKey(KeyCode.W))
            {
                if (null == Text)
                    return;
                
                SetMovingMessage("moving forward");
            }
        }
        
        private void SetMovingMessage(string msg)
        {
            if (null == Text)
                return;

            Text.text = msg;
        }
        
        public void Quit()
        {
            Application.Quit(0);
        }
        
        public void ShowScene(int id)
        {
            switch (id)
            {
                case 1:
                    SceneManager.LoadScene("MissionDemo");
                    break;
                case 2:
                    SceneManager.LoadScene("UMAMissionInteractions");
                    break;
                default:
                    break;
            }
        }        
    }
}