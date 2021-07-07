using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AIBunnies
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] Transform Player;
        [SerializeField] Transform Ai;
        [SerializeField] UIManager uIManager;
        [SerializeField] internal List<Transform> AIGoals;
        [SerializeField] internal float AIViewFieldDistance = 20;
        [SerializeField] internal float AIViewFieldAngle = 110;
        int playerPoints = 0;

        #region singleton
        private static GameManager _instance;
        public static GameManager Instance { get { return _instance; } }

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
        }
        #endregion

        internal Transform GetPlayerTransform()
        {
            return Player;
        }

        internal void GivePlayerPoint()
        {
            playerPoints++;
            uIManager.RefreshPoints(playerPoints);
        }

        internal void MakeNoise(bool IsNoise)
        {
            if (IsNoise)
            {
                AudioManager.Instance.PlayCarrot(true);
                AIViewFieldAngle = 360;
            }
            else
            {
                AudioManager.Instance.PlayCarrot(false);
                AIViewFieldAngle = 60;
            }
        }
    }
}