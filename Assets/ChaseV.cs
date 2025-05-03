using UnityEngine;
using UnityEngine.AI;

public class ChaseV : StateMachineBehaviour
{
    NavMeshAgent agent;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Transform player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (player != null)
        {
            // Set the destination to the player's position
            agent.destination = player.position;

            // If player is out of chase range, go back to patrolling
            if (Vector3.Distance(player.position, agent.transform.position) > 15f)
            {
                animator.SetBool("isChasing", false);
                animator.SetBool("isPatrolling", true);
            }
        }
    }
}
