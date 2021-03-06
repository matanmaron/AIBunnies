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

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            player = GameManager.Instance.GetPlayerTransform();
            transform = animator.transform;
            navMeshAgent = animator.gameObject.GetComponent<NavMeshAgent>();
            navMeshAgent.speed = BunniesHelper.Constants.AI_PERSUE_SPEED;
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (GameManager.Instance.IsGameOver)
            {
                return;
            }
            Vector3 realGoal = new Vector3(player.position.x, transform.position.y, player.position.z);
            navMeshAgent.SetDestination(realGoal);
            Debug.DrawLine(transform.position, realGoal, Color.red);
            Debug.DrawRay(transform.position, transform.forward * GameManager.Instance.AIViewFieldDistance);
            if (!IsInSight())
            {
                AudioManager.Instance.PlayLost();
                animator.SetBool("InSight", false);
                Debug.Log($"[AI] Lost sight");
            }
        }

        public bool IsInSight()
        {
            //1. Distance
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance > GameManager.Instance.AIViewFieldDistance)
            {
                Debug.Log($"[AI] lost distance ({distance})");
                return false;
            }

            //2. Angle
            Vector3 distanceVector = player.transform.position - transform.position;
            distanceVector.y = 0;

            if (GameManager.Instance.AIViewFieldAngle == BunniesHelper.Constants.FIELD_ANGLE_PERSUE)
            {
                //3. Soud       
                RaycastHit[] hits = Physics.RaycastAll(transform.position, distanceVector.normalized, distance);
                foreach (var hit in hits)
                {
                    if (hit.collider.tag == BunniesHelper.Constants.PLAYER_TAG)
                    {
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
                        return true;
                    }
                }
            }
            Debug.Log($"[AI] lost. not in any range or raycast");
            return false;
        }
    }
}