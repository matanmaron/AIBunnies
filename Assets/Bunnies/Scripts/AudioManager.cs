using System;
using System.Collections.Generic;
using UnityEngine;

namespace AIBunnies
{
    public class AudioManager : MonoBehaviour
    {
        #region singleton
        public static AudioManager Instance { get; private set; }
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this;
            }
        }
        #endregion

        [SerializeField] List<AudioSource> SFXPlayer = null;
        [SerializeField] AudioSource SFXCarrot = null;
        int currentSfx = 0;

        [SerializeField] AudioClip Yell;
        [SerializeField] AudioClip Lost;
        [SerializeField] AudioClip Levelup;
        [SerializeField] AudioClip GameOver;
        private AudioSource PlayEffect(AudioClip clip)
        {
            Debug.Log($"[AudioManager] Using sfx player ({currentSfx})");
            SFXPlayer[currentSfx].clip = clip;
            SFXPlayer[currentSfx].Play();
            currentSfx++;
            if (currentSfx >= SFXPlayer.Count)
            {
                currentSfx = 0;
            }
            return SFXPlayer[currentSfx];
        }

        public void PlayCarrot(bool play)
        {
            if (play)
            {
                SFXCarrot.Play();
            }
            else
            {
                SFXCarrot.Stop();
            }
        }
        public void PlayYell()
        {
            PlayEffect(Yell);
        }
        public void PlayLost()
        {
            PlayEffect(Lost);
        }
        public void PlayLevelup()
        {
            PlayEffect(Levelup);
        }
        public void PlayGameOver()
        {
            PlayEffect(GameOver);
        }
    }
}