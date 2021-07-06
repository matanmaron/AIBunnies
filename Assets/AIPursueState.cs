using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;
using Debug = UnityEngine.Debug;

namespace AIBunnies
{
    public class AIPursueState : StateMachineBehaviour
    {
        NavMeshAgent navMeshAgent;
        private Transform transform;
        private Transform player;
        Stopwatch stopWatch = new Stopwatch();
        bool hasSight = true;
        float speed = 4f;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            player = GameManager.Instance.GetPlayerTransform();
            transform = animator.transform;
            navMeshAgent = animator.gameObject.GetComponent<NavMeshAgent>();
            navMeshAgent.speed = speed;
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Vector3 realGoal = new Vector3(player.position.x, transform.position.y, player.position.z);
            navMeshAgent.SetDestination(realGoal);
            Debug.DrawLine(transform.position, realGoal, Color.red);
            for (int i = 0; i < 10; i++)
            {
                Debug.DrawRay(transform.position, Vector3.forward * GameManager.Instance.AIViewFieldDistance);
            }
            if (IsInSight() && !hasSight)
            {
                hasSight = true;
                stopWatch.Reset();
                stopWatch.Stop();
            }
            else if (!hasSight)
            {
                stopWatch.Reset();
                stopWatch.Start();
                GameManager.Instance.StartTimer(delegate { CheckWatch(animator); });
            }
        }

        private void CheckWatch(Animator anim)
        {
            if (hasSight)
            {
                return;
            }
            TimeSpan ts = stopWatch.Elapsed;
            if (ts.TotalSeconds > 1)
            {
                AudioManager.Instance.PlayLost();
                anim.SetBool("InSight", false);
                Debug.Log($"[AI] Lost sight for ({ts.TotalSeconds}) seconds");
            }
        }

        public bool IsInSight()
        {
            //1. Distance
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance > GameManager.Instance.AIViewFieldDistance)
            {
                return false;
            }

            //2. Angle
            Vector3 distanceVector = player.transform.position - transform.position;
            distanceVector.y = 0;
            float angle = Vector3.Angle(transform.forward, distanceVector);
            if (angle > GameManager.Instance.AIViewFieldAngle / 2)
            {
                return false;
            }

            if (GameManager.Instance.AIViewFieldAngle == 360)
            {
                //3. Soud       
                RaycastHit[] hits = Physics.RaycastAll(transform.position, distanceVector.normalized, distance);
                foreach (var hit in hits)
                {
                    if (hit.collider.tag == BunniesHelper.Constants.PLAYER_TAG)
                    {
                        if (!hasSight)
                        {
                            Debug.Log("[AI] Detect Sound!");
                        }
                        return true;
                    }
                }
            }
            else
            {
                //3. Obstacles       
                RaycastHit hit;
                if (Physics.Raycast(transform.position, distanceVector.normalized, out hit, distance))
                {
                    if (hit.collider.tag == BunniesHelper.Constants.PLAYER_TAG)
                    {
                        if (!hasSight)
                        {
                            Debug.Log("[AI] Detect Sight!");
                        }
                        return true;
                    }
                }
            }
            return false;
        }
    }
}