using UnityEngine;

public class AttackV : StateMachineBehaviour
{
    Transform player;
    float attackDuration;  // Duration of the attack animation

    public float timeBetweenAttacks = 2f; // Time between attacks to ensure the boss doesn't attack immediately

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        attackDuration = stateInfo.length; // Get the duration of the attack animation
        animator.SetBool("isAttacking", true); // Ensure the attack animation starts
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // If the attack animation is finished, transition to idle or patrol state
        if (stateInfo.normalizedTime >= 1)
        {
            animator.SetBool("isAttacking", false);  // End the attack state
            animator.SetBool("isPatrolling", true);  // Transition to patrol or idle
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Reset any necessary parameters when the attack state ends
        animator.SetBool("isAttacking", false); // Reset attack flag
    }
}
