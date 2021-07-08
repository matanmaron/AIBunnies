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
        private Vector3 endPos;
        bool raise;

        private void Start()
        {
            raise = false;
            startPos = transform.position;
            endPos = new Vector3(startPos.x, startPos.y + 10, startPos.z);
            label.text = string.Empty;
            label.transform.position = Camera.main.WorldToScreenPoint(labelParent.position);
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log($"[CARROT] OnTriggerEnter {other.gameObject.name}");
            if (other.gameObject.tag == BunniesHelper.Constants.PLAYER_TAG)
            {
                if (coroutine == null)
                {
                    Debug.Log("[CARROT] start");
                    coroutine = StartCoroutine(GetCarrot());
                }
            }
        }

        private void Update()
        {
            if (raise || transform.position == endPos)
            {
                transform.position = Vector3.Lerp(transform.position, endPos, Time.deltaTime * 0.1f);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == BunniesHelper.Constants.PLAYER_TAG)
            {
                if (coroutine != null)
                {
                    Debug.Log("[CARROT] stop");
                    raise = false;
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
            raise = true;
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
            raise = false;
            Debug.Log("[CARROT] out");
            label.text = string.Empty;
            GameManager.Instance.GivePlayerPoint();
            GameManager.Instance.MakeNoise(false);
            effect.Stop();
            Destroy(gameObject);
        }
    }
}