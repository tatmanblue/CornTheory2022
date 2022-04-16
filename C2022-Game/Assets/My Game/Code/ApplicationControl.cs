﻿using UnityEngine;

namespace CornTheory
{
    /// <summary>
    /// Static methods, probably will go away as other types take shape
    /// </summary>
    public class ApplicationControl : MonoBehaviour
    {
        public void Quit(int exitCode = 0)
        {
            // This method is duplicated in code\ui\SceneHandler2
            Application.Quit(exitCode);
        }

        public void Set640x480(bool fullScreen = true)
        {
            Screen.SetResolution(640, 480, fullScreen);
        }
        
        public void Set1024x768(bool fullScreen = true)
        {
            Screen.SetResolution(1024, 768, fullScreen);
        }

        public void Set3840x2160(bool fullScreen = true)
        {
            Screen.SetResolution(3840, 2160, fullScreen);
        }
    }
}