using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace AIBunnies
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] Text score;
        [SerializeField] GameObject GameOverPanel;
        [SerializeField] GameObject WinImg;
        [SerializeField] GameObject LoseImg;
        internal void RefreshPoints(int playerPoints)
        {
            score.text = playerPoints.ToString();
            Debug.Log($"[UI] playerPoints: {playerPoints}");
        }

        internal void GameOver(bool isWin)
        {
            WinImg.SetActive(isWin);
            LoseImg.SetActive(!isWin);
            GameOverPanel.SetActive(true);
        }

        public void OnRestart()
        {
            SceneManager.LoadScene(1);
        }

        public void OnMenu()
        {
            SceneManager.LoadScene(0);
        }
    }
}