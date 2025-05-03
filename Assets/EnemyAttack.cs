using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : StateMachineBehaviour
{

    Transform player;
    PlayerHealth playerHealth;
    NavMeshAgent agent;


    float attackCooldown = 1.5f;
    float nextAttackTime;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        agent = animator.GetComponent<NavMeshAgent>();

        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
        }

        if (agent != null)
        {
            agent.isStopped = true; // ❌ Stop movement during attack
            agent.ResetPath();
        }

        nextAttackTime = Time.time + attackCooldown;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player == null) return;

        float distance = Vector3.Distance(player.position, animator.transform.position);

        if (distance > 3f)
        {
            animator.SetBool("isAttacking", false);
        }
        else if (Time.time >= nextAttackTime)
        {
            nextAttackTime = Time.time + attackCooldown;

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(5);
                Debug.Log("Player hit by enemy attack!");
            }
        }

        // Optional: Keep enemy looking at the player (only if you're not using NavMeshAgent's rotation)
        animator.transform.LookAt(new Vector3(player.position.x, animator.transform.position.y, player.position.z));
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent != null)
        {
            agent.isStopped = false; // ✅ Allow movement again after attack
        }
    }
}
