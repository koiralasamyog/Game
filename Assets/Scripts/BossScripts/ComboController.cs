using UnityEngine;

public class ComboController : MonoBehaviour
{
    private Animator animator;
    private int comboStep = 0;
    private float comboTimer;
    private float comboCooldown = 1.5f;
    private bool isAttacking = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (ShouldStartAttack() && !isAttacking)
        {
            StartCombo();
        }

        if (isAttacking)
        {
            comboTimer += Time.deltaTime;
            if (comboTimer >= comboCooldown)
            {
                ResetCombo();
            }
        }
    }

    void StartCombo()
    {
        comboStep = 1;
        animator.SetInteger("comboStep", comboStep);
        animator.SetTrigger("nextAttack");
        isAttacking = true;
        comboTimer = 0;
    }

    // This will be called from an animation event at the end of each attack animation
    public void ContinueCombo()
    {
        if (comboStep < 3)
        {
            comboStep++;
            animator.SetInteger("comboStep", comboStep);
            animator.SetTrigger("nextAttack");
            comboTimer = 0;
        }
        else
        {
            ResetCombo();
        }
    }

    void ResetCombo()
    {
        comboStep = 0;
        animator.SetInteger("comboStep", 0);
        isAttacking = false;
        comboTimer = 0;
    }

    bool ShouldStartAttack()
    {
        // Replace this with your condition, e.g., player in range
        return Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) < 4f;
    }
}
