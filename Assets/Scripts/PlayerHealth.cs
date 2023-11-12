using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxHealth;
    int currentHealth;

    public HealthBar healthBar;
    public UnityEvent OnDeath;
    public Player player;

    private void OnEnable()
    {
        OnDeath.AddListener(Death);
    }

    private void OnDisable()
    {
        OnDeath.RemoveListener(Death);
    }

    private void Start()
    {
        currentHealth = maxHealth;

        healthBar.UpdateBar(currentHealth, maxHealth);
    }

    public void TakeDamage(int damage)
    {
        if (!player.rollOnce)
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                OnDeath.Invoke();
            }

            healthBar.UpdateBar(currentHealth, maxHealth);
        }
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
