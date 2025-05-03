using UnityEngine;
using UnityEngine.UI;

public class DragonBoss : MonoBehaviour
{
    public ChangeGoldValue goldValue;
    public Image image;
    public Image anotherImage;
    float delayTime = 1.5f;
    public int maxHealth = 500;
    private int currentHealth;
    public bool die = false;
    [SerializeField] private EnemyHealthBar _healthBar;

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
        anotherImage.enabled = true;
        
        // Play death animation or destroy enemy
        Debug.Log(gameObject.name + " died!");
        if (animator != null)
        {
            animator.SetTrigger("die");
        }
        image.enabled = true;
        
        Invoke("DisableObject", delayTime);
        // Optional: disable enemy behavior or destroy the object
        Destroy(gameObject, 2f);
        goldValue.AddGold(500);
    }

    private void DisableObject()
    {
        image.enabled = false;
        anotherImage.enabled = false;   
    }
}
