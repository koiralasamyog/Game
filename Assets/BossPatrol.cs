using UnityEngine;
using UnityEngine.AI;

public class BossPatrol : MonoBehaviour
{
    public Transform[] patrolPoints;    // Array of patrol points
    private int currentPoint = 0;       // Current patrol point index
    public NavMeshAgent agent;         // Reference to the NavMeshAgent component

    public float patrolSpeed = 2f;      // Speed when patrolling
    public float chaseSpeed = 5f;       // Speed when chasing player
    public float chaseRadius = 10f;     // Radius at which the boss starts chasing the player
    public float attackRange = 2f;      // Range at which the boss starts attacking the player

    private Transform player;           // Reference to the player's transform

    public Animator animator;           // Reference to the Animator to control animations

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;   // Assuming player has the "Player" tag
        agent.speed = patrolSpeed;     // Set initial patrol speed
    }

    private void Update()
    {
        PatrolAndChase();
    }

    private void PatrolAndChase()
    {
        // Check distance to player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // If player is within chase range, switch to chasing
        if (distanceToPlayer < chaseRadius)
        {
            ChasePlayer();

            // If the boss is close enough to the player, stop and attack
            if (distanceToPlayer < attackRange)
            {
                StopAndAttack();
            }
        }
        else
        {
            Patrol();
        }
    }

    public void Patrol()
    {
        // If we are already at the patrol point, move to the next one
        if (Vector3.Distance(transform.position, patrolPoints[currentPoint].position) < 1f)
        {
            currentPoint = (currentPoint + 1) % patrolPoints.Length;  // Loop back to the first point
        }

        // Move to the current patrol point
        agent.destination = patrolPoints[currentPoint].position;
        agent.speed = patrolSpeed;  // Ensure we are patrolling at the correct speed
    }

    private void ChasePlayer()
    {
        // Chase player
        agent.destination = player.position;
        agent.speed = chaseSpeed;  // Set the chase speed
        animator.SetBool("isChasing", true);
    }

    private void StopAndAttack()
    {
        // Stop moving and trigger attack behavior
        agent.isStopped = true;   // Stop the agent from moving
        animator.SetBool("isAttacking", true); // Trigger attack animation
    }

    public void ResumeMovement()
    {
        // This method can be called after the attack to resume chasing or patrolling
        agent.isStopped = false;
        animator.SetBool("isAttacking", false);
    }
}
