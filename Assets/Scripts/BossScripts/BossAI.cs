using UnityEngine;
using UnityEngine.AI;

public class BossAI : MonoBehaviour
{
    public enum BossState { Idle, Chasing, Attacking, Dead }
    public BossState currentState = BossState.Idle;

    public Transform player;
    public float chaseRange = 15f;
    public float attackRange = 3f;
    public float timeBetweenAttacks = 2f;
    public int bossHealth = 100;

    private float lastAttackTime = 0;
    private Animator animator;
    private NavMeshAgent agent;
    private bool isDead = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (isDead) return;

        float distance = Vector3.Distance(transform.position, player.position);

        switch (currentState)
        {
            case BossState.Idle:
                if (distance < chaseRange)
                    currentState = BossState.Chasing;
                break;

            case BossState.Chasing:
                ChasePlayer();
                if (distance <= attackRange)
                    currentState = BossState.Attacking;
                break;

            case BossState.Attacking:
                AttackPlayer();
                if (distance > attackRange)
                    currentState = BossState.Chasing;
                break;
        }
    }

    void ChasePlayer()
    {
        animator.SetBool("isWalking", true);
        agent.isStopped = false;
        agent.SetDestination(player.position);
    }

    void AttackPlayer()
    {
        agent.isStopped = true;
        animator.SetBool("isWalking", false);
        transform.LookAt(player);

        if (Time.time - lastAttackTime >= timeBetweenAttacks)
        {
            animator.SetTrigger("Attack");
            lastAttackTime = Time.time;
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        bossHealth -= damage;
        if (bossHealth <= 0)
            Die();
    }

    void Die()
    {
        isDead = true;
        animator.SetTrigger("Die");
        agent.isStopped = true;
        currentState = BossState.Dead;
    }

    // This will be called via animation event
    //public void DealDamageToPlayer()
    //{
    //    float distance = Vector3.Distance(transform.position, player.position);
    //    if (distance < attackRange + 1f)
    //    {
    //        player.GetComponent<HealthBar>().TakeDamage(20);
    //    }
    //}
}
