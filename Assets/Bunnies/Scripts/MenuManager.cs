using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AIBunnies
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] GameObject Exit;

        private void Start()
        {
#if UNITY_ANDROID
            Exit.SetActive(false);
#endif
        }

        public void OnStart()
        {
            SceneManager.LoadScene(1);
        }

        public void OnExit()
        {
            Application.Quit();
        }
    }
}