using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TatmanGames.Character.Character.Demo
{
    public class ApplicationControl : MonoBehaviour
    {
        private bool handlingKey = false;
        
        // Update is called once per frame
        private void Update()
        {
            if (true == Input.GetKey(KeyCode.Backspace))
            {
                if (SceneManager.GetActiveScene().name.Contains("Menu"))
                    Application.Quit(0);
                else
                    SceneManager.LoadScene("Menu");
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

        public void ShowScene(int id)
        {
            switch (id)
            {
                case 1:
                    SceneManager.LoadScene("CustomController");
                    break;
                case 2:
                    SceneManager.LoadScene("UMAController");
                    break;
                case 3:
                    SceneManager.LoadScene("UnityController");
                    break;
                case 4:
                    SceneManager.LoadScene("OpsiveUma");
                    break;
                default:
                    break;
            }
        }
    }
}