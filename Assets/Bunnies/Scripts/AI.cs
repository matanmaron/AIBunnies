using System.Collections;
using TMPro;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

namespace AIBunnies
{
    public class AI : MonoBehaviour
    {
        private Transform goal;
        [SerializeField] float speed = 0.01f;
        [SerializeField] float distance = 1.5f;
        [SerializeField]ThirdPersonCharacter thirdPersonCharacter;

        Animator anim;
        bool isWalking;

        private void Start()
        {
            goal = GameManager.Instance.GetPlayerTransform();
            anim = GetComponent<Animator>();
        }

        void Update()
        {
            Vector3 realGoal = new Vector3(goal.position.x, transform.position.y, goal.position.z);
            Vector3 direction = realGoal - transform.position;
            if (direction.magnitude >= distance)
            {
                if (!isWalking)
                {
                    anim.SetBool("Run", true);
                    isWalking = true;
                }
                Vector3 pushVector = direction.normalized * speed;
                transform.Translate(pushVector, Space.World);
                thirdPersonCharacter.Move(pushVector, false, false);
            }
            else if (isWalking)
            {
                anim.SetBool("Run", false);
                isWalking = false;
            }
            Debug.DrawLine(transform.position, realGoal, Color.red);
        }
    }
}