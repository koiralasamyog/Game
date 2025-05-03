using UnityEngine;
using UnityEngine.AI;

public class PatrolV : StateMachineBehaviour
{
    BossPatrol bossPatrol;
    NavMeshAgent agent;

    // Called when the state is entered
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Initialize variables
        agent = animator.GetComponent<NavMeshAgent>();
        bossPatrol = animator.GetComponent<BossPatrol>();
        bossPatrol.agent.destination = bossPatrol.patrolPoints[0].position; // Start patrolling
    }

    // Called every frame the state is active
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent.remainingDistance < 1f) // If we are near the patrol point, go to the next one
        {
            bossPatrol.Patrol();
        }

        // Check if the player is within the chase radius
        Transform player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player != null && Vector3.Distance(player.position, agent.transform.position) < bossPatrol.chaseRadius)
        {
            animator.SetBool("isChasing", true);
            animator.SetBool("isPatrolling", false);
        }
    }

    // Called when the state is exited
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Clean up if needed when leaving the patrol state
    }
}
