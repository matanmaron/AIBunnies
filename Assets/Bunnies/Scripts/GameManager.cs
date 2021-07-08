using System.Collections.Generic;
using UnityEngine;

namespace AIBunnies
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] Transform Player;
        [SerializeField] Transform Ai;
        [SerializeField] UIManager uIManager;
        [SerializeField] internal List<Transform> AIGoals;
        internal float AIViewFieldDistance = BunniesHelper.Constants.FIELD_DISTANCE;
        internal float AIViewFieldAngle = BunniesHelper.Constants.FIELD_ANGLE_PATROL;
        int playerPoints = 0;
        public bool IsGameOver = false;

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
            if (playerPoints == BunniesHelper.Constants.NEEDED_POINTS)
            {
                WinGame();
            }
        }

        internal void WinGame()
        {
            if (IsGameOver)
            {
                return;
            }
            IsGameOver = true;
            uIManager.GameOver(true);
        }

        internal void LoseGame()
        {
            if (IsGameOver)
            {
                return;
            }
            IsGameOver = true;
            uIManager.GameOver(false);
        }

        internal void MakeNoise(bool IsNoise)
        {
            if (IsNoise)
            {
                AudioManager.Instance.PlayCarrot(true);
                AIViewFieldAngle = BunniesHelper.Constants.FIELD_ANGLE_PERSUE;
            }
            else
            {
                AudioManager.Instance.PlayCarrot(false);
                AIViewFieldAngle = BunniesHelper.Constants.FIELD_ANGLE_PATROL;
            }
        }
    }
}