using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private HealthBar _healthBar;
    
    public int maxHealth = 100;
    private int currentHealth;
    public Image image;
    public GameObject hello;
    private Animator animator;

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
            animator.SetTrigger("hit");
            _healthBar.UpdateHealthBar(maxHealth, currentHealth);
        }

        Debug.Log("Player HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player died!");
        hello.SetActive(true);
        if (animator != null)
        {
            animator.SetTrigger("die");
            image.enabled = true;
        }

        // Disable player movement/attacks or reload scene
        Destroy(gameObject); // if needed
    }
}
