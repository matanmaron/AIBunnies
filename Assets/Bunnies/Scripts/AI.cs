using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

namespace AIBunnies
{
    public class AI : MonoBehaviour
    {
        NavMeshAgent navMeshAgent;
        private Transform goal;
        Animator anim;
        bool isWalking;

        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            goal = GameManager.Instance.GetPlayerTransform();
            anim = GetComponent<Animator>();
        }

        void Update()
        {
            Vector3 realGoal = new Vector3(goal.position.x, transform.position.y, goal.position.z);
            navMeshAgent.SetDestination(realGoal);
            Debug.DrawLine(transform.position, realGoal, Color.red);
            if (navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance)
            {
                if (!isWalking)
                {
                    anim.SetBool("Run", true);
                    isWalking = true;
                }
            }
            else if (isWalking)
            {
                anim.SetBool("Run", false);
                isWalking = false;
            }
        }
    }
}