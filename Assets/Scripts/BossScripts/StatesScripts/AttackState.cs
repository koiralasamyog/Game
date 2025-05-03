using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackState : StateMachineBehaviour
{
    Transform player;
    PlayerHealth playerHealth;
    NavMeshAgent agent;

    float attackCooldown = 1.5f;
    float nextAttackTime;

    // Called when the attack state starts
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
            agent.isStopped = true; // Stop movement during attack
            agent.ResetPath(); // Reset any existing path
        }

        nextAttackTime = Time.time + attackCooldown;
    }

    // Called on each frame during the attack state
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player == null) return;

        // Rotate the enemy to face the player
        Vector3 direction = player.position - animator.transform.position;
        direction.y = 0; // Keep the enemy upright (no vertical rotation)
        Quaternion rotation = Quaternion.LookRotation(direction);

        // Slow down the rotation speed by reducing the interpolation value
        float rotationSpeed = 3f; // Lower value for slower rotation
        animator.transform.rotation = Quaternion.Slerp(animator.transform.rotation, rotation, Time.deltaTime * rotationSpeed);

        float distance = Vector3.Distance(player.position, animator.transform.position);

        // Check if the player is out of range
        if (distance > 10f)
        {
            animator.SetBool("isAttacking", false);
        }
        else if (Time.time >= nextAttackTime)
        {
            nextAttackTime = Time.time + attackCooldown;

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(25); // Apply damage to player
                Debug.Log("Player hit by enemy attack!");
            }
        }
    }

    // Called when the attack state finishes
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent != null)
        {
            agent.isStopped = false; // Resume movement after attack
        }
    }
}
