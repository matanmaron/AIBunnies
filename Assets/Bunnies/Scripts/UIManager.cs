using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AIBunnies
{
    public class UIManager : MonoBehaviour
    {
        internal void RefreshPoints(int playerPoints)
        {
            Debug.Log($"[UI] playerPoints: {playerPoints}");
        }
    }
}