using System;
using System.Collections;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

namespace AIBunnies
{
    public class Player : MonoBehaviour
    {
        [SerializeField] float speed = 0.02f;
        [SerializeField] float distance = 1.5f;
        [SerializeField] ThirdPersonCharacter thirdPersonCharacter;

        Vector3 goal;
        Animator anim;
        bool isWalking;

        private void Start()
        {
            anim = GetComponent<Animator>();
            goal = transform.position;
        }

        void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log($"new target set - {hit.point}");
                    goal = hit.point;
                }
            }
            Vector3 realGoal = new Vector3(goal.x,
                transform.position.y, goal.z);
            Vector3 direction = realGoal - transform.position;

            if (direction.magnitude >= distance)
            {
                if (!isWalking)
                {
                    anim.SetBool("Run",true);
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
            Debug.DrawLine(transform.position, realGoal, Color.green);
        }
    }
}