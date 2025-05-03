using UnityEngine;

public class AttackHit : MonoBehaviour
{
    public float pushForce = 10f; // Force to push the enemy away
    public float attackRange = 5f; // Range of the attack
    public LayerMask enemyLayer; // To specify that we want to hit enemies

    private void OnTriggerEnter(Collider other)
    {
        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(50); // Adjust damage as needed
        }
        
        DragonBoss bossHealth = other.GetComponent<DragonBoss>();
        if (bossHealth != null)
        {
            bossHealth.TakeDamage(50); // Adjust damage as needed
        }

        // Check if the object hit is an enemy (based on the enemy's tag or layer)
        if ((enemyLayer.value & (1 << other.gameObject.layer)) > 0)
        {
            // Get the enemy's Rigidbody
            Rigidbody enemyRigidbody = other.GetComponent<Rigidbody>();

            if (enemyRigidbody != null)
            {
                // Calculate direction and apply force
                Vector3 direction = other.transform.position - transform.position;
                direction.y = 0; // Optional: remove vertical component to prevent lifting
                enemyRigidbody.AddForce(direction.normalized * pushForce, ForceMode.Impulse);
            }

            // Trigger the hit animation on the enemy's Animator
            Animator enemyAnimator = other.GetComponent<Animator>();
            if (enemyAnimator != null)
            {
                // Trigger the hit animation using the "Hit" trigger parameter
                enemyAnimator.SetTrigger("damage");
                Debug.Log("Hitting");
            }
        }
    }
}
