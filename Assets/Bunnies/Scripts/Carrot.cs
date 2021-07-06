using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AIBunnies
{
    public class Carrot : MonoBehaviour
    {
        [SerializeField] Text label = null;
        [SerializeField] Transform labelParent = null;
        [SerializeField] ParticleSystem effect = null;
        private Coroutine coroutine = null;
        private Vector3 startPos;

        private void Start()
        {
            startPos = transform.position;
            label.text = string.Empty;
            label.transform.position = Camera.main.WorldToScreenPoint(labelParent.position);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == BunniesHelper.Constants.PLAYER_TAG)
            {
                if (coroutine == null)
                {
                    Debug.Log("[CARROT] start");
                    coroutine = StartCoroutine(GetCarrot());
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == BunniesHelper.Constants.PLAYER_TAG)
            {
                if (coroutine != null)
                {
                    Debug.Log("[CARROT] stop");
                    effect.Stop();
                    StopCoroutine(coroutine);
                    GameManager.Instance.MakeNoise(false);
                    transform.position = startPos;
                    label.text = string.Empty;
                    coroutine = null;
                }
            }
        }

        IEnumerator GetCarrot()
        {
            effect.Play();
            GameManager.Instance.MakeNoise(true);
            Debug.Log("[CARROT] spin 1");
            label.text = "3";
            yield return new WaitForSeconds(1);
            Debug.Log("[CARROT] spin 2");
            label.text = "2";
            yield return new WaitForSeconds(1);
            Debug.Log("[CARROT] spin 3");
            label.text = "1";
            yield return new WaitForSeconds(1);
            Debug.Log("[CARROT] out");
            label.text = string.Empty;
            GameManager.Instance.GivePlayerPoint();
            GameManager.Instance.MakeNoise(false);
            effect.Stop();
            Destroy(gameObject);
        }
    }
}