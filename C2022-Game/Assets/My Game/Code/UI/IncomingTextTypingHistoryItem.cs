﻿using TMPro;
using UnityEngine;


namespace CornTheory.UI
{
    /// <summary>
    /// For a message displayed in a ScrollView.  "Bound" to
    /// prefab "TwoWayTextHistoryItem.prefab"
    /// </summary>
    public class IncomingTextTypingHistoryItem : MonoBehaviour
    {
        public GameObject WaitImage;
        public TextMeshProUGUI Who;
        public TextMeshProUGUI Said;
        public int WaitMS = 2000;

    }
}