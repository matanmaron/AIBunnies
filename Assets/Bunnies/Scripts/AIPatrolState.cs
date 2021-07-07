using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AIBunnies
{
    public class AIPatrolState : StateMachineBehaviour
    {
        NavMeshAgent navMeshAgent;
        private Transform goal;
        private Transform transform;
        private Transform player;
        List<Transform> AIGoals;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            player = GameManager.Instance.GetPlayerTransform();
            transform = animator.transform;
            AIGoals = GameManager.Instance.AIGoals;
            navMeshAgent = animator.gameObject.GetComponent<NavMeshAgent>();
            goal = transform;
            navMeshAgent.speed = BunniesHelper.Constants.AI_PATROL_SPEED;
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (GameManager.Instance.IsGameOver)
            {
                return;
            }
            Vector3 realGoal = new Vector3(goal.position.x, transform.position.y, goal.position.z);
            navMeshAgent.SetDestination(realGoal);
            Debug.DrawLine(transform.position, realGoal, Color.red);
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                goal = AIGoals[Random.Range(0, AIGoals.Count)];
            }
            Debug.DrawRay(transform.position, transform.forward * GameManager.Instance.AIViewFieldDistance);
            if (IsInSight())
            {
                AudioManager.Instance.PlayYell();
                Debug.Log("[AI] IsInSight");
                animator.SetBool("InSight", true);
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

            if (GameManager.Instance.AIViewFieldAngle == BunniesHelper.Constants.FIELD_ANGLE_PERSUE)
            {
                //3. Soud       
                RaycastHit[] hits = Physics.RaycastAll(transform.position, distanceVector.normalized, distance);
                foreach (var hit in hits)
                {
                    if (hit.collider.tag == BunniesHelper.Constants.PLAYER_TAG)
                    {
                        Debug.Log("[AI] Detect Sound!");
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
                        Debug.Log("[AI] Detect Sight!");
                        return true;
                    }
                }
            }
            return false;
        }
    }
}