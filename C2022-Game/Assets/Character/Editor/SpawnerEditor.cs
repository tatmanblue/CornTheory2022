using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace TatmanGames.Character.Editor
{
    /// <summary>
    /// WIP: goal is to be able to graphically setup spawning data and spawning points
    /// </summary>
    public class SpawnerEditor : EditorWindow
    {
        [MenuItem ("Tools/Tatman Games/Spawn Editor")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(SpawnerEditor));
        }
        
        private string savePath = String.Empty;
        
        private void OnGUI()
        {
            GUILayout.Label("Save all spawn points", EditorStyles.boldLabel);
            EditorGUILayout.BeginHorizontal();
            savePath = EditorGUILayout.TextField("Path", savePath);

            if (GUILayout.Button("...", EditorStyles.miniButtonRight, GUILayout.Width(25)))
            {
                savePath = EditorUtility.OpenFolderPanel("Where to save data", "", "");
            }
            EditorGUILayout.EndHorizontal();
            
            if (GUI.Button (new Rect (25, 75, 100, 30), "Save")) 
            {
                Debug.Log("Save called");
            }
        }
    }
}
