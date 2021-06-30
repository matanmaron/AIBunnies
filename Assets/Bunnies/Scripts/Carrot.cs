using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AIBunnies
{
    public class Carrot : MonoBehaviour
    {
        private Coroutine coroutine = null;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == BunniesHelper.Constants.PLAYER_TAG)
            {
                if (coroutine == null)
                {
                    Debug.Log("[CARROT] start");
                    coroutine = StartCoroutine(GetCarrot());
                }
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.tag == BunniesHelper.Constants.PLAYER_TAG)
            {
                if (coroutine != null)
                {
                    Debug.Log("[CARROT] stop");
                    StopCoroutine(coroutine);
                    coroutine = null;
                }
            }
        }

        IEnumerator GetCarrot()
        {
            Debug.Log("[CARROT] spin 1");
            yield return new WaitForSeconds(1);
            Debug.Log("[CARROT] spin 2");
            yield return new WaitForSeconds(1);
            Debug.Log("[CARROT] spin 3");
            yield return new WaitForSeconds(1);
            Debug.Log("[CARROT] out");
        }
    }
}