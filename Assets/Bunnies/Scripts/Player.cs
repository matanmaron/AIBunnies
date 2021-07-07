using UnityEngine;
using UnityEngine.AI;

namespace AIBunnies
{
    public class Player : MonoBehaviour
    {
        NavMeshAgent navMeshAgent;
        Vector3 goal;
        Animator anim;
        bool isWalking;

        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            goal = transform.position;
            anim = GetComponent<Animator>();
        }

        void Update()
        {
            Vector3 realGoal = new Vector3(goal.x, transform.position.y, goal.z);
            navMeshAgent.SetDestination(realGoal);
            Debug.DrawLine(transform.position, realGoal, Color.green);
            if (Input.GetMouseButtonUp(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log($"[PLAYER] new target set - {hit.point}");
                    goal = hit.point;
                }
            }
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

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log($"[PLAYER] OnTriggerEnter {other.gameObject.name}");
            if (other.gameObject.tag == BunniesHelper.Constants.CARROT_TAG)
            {
                goal = transform.position;
            }
            if (other.gameObject.tag == BunniesHelper.Constants.FARMER_TAG)
            {
                GameManager.Instance.LoseGame();
            }
        }
    }
}