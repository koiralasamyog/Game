using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChase : StateMachineBehaviour
{
    NavMeshAgent agent;
    Transform player;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (agent != null)
        {
            agent.speed = 2f;
            agent.acceleration = 4f; // Lower acceleration
            agent.angularSpeed = 120f; // Moderate turn speed
            agent.stoppingDistance = 2.5f; // Slight stop before reaching player
            agent.updateRotation = true; // Let agent rotate itself
            agent.isStopped = false;
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player == null || agent == null) return;

        agent.SetDestination(player.position);

        float distance = Vector3.Distance(player.position, animator.transform.position);

        if (distance > 10f)
        {
            animator.SetBool("isChasing", false);
        }
        else if (distance < 3f)
        {
            animator.SetBool("isAttacking", true);
        }

        // Optional: draw path line for debugging
        Debug.DrawLine(animator.transform.position, player.position, Color.red);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent != null)
        {
            agent.ResetPath();
            agent.isStopped = true;
        }
    }
}
