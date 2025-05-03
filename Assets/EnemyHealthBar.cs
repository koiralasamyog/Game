using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthbarSprite;
    public Transform cam;

    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        _healthbarSprite.fillAmount = currentHealth / (float)maxHealth;

    }

    private void LateUpdate()
    {
        transform.LookAt(cam);
    }
}

