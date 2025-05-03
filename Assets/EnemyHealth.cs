using UnityEngine;
using UnityEngine.UI;
public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    [SerializeField] private EnemyHealthBar _healthBar;
    ChangeGoldValue gold;

    private Animator animator;
    public Image image;
    public bool die = false;
    private void Start()
    {
        currentHealth = maxHealth;
        _healthBar.UpdateHealthBar(maxHealth, currentHealth);
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (animator != null)
        {
            animator.SetTrigger("damage");
            _healthBar.UpdateHealthBar(maxHealth, currentHealth);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        die = true;

        // Play death animation or destroy enemy
        Debug.Log(gameObject.name + " died!");
        
        if (animator != null)
        {
            animator.SetTrigger("die");
            image.enabled = true;
            Invoke("DisableEffect", 1.5f);
            

        }

        // Optional: disable enemy behavior or destroy the object
        Destroy(gameObject, 2f);
        gold.AddGold(50);
    }

    private void DisableEffect()
    {
        image.enabled = false;
    }
}
