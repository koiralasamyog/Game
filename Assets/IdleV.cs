using UnityEngine;

public class IdleV : StateMachineBehaviour
{
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Transition to patrolling after a delay or condition
        if (!animator.GetBool("isPatrolling"))
        {
            animator.SetBool("isPatrolling", true);
        }
    }
}
