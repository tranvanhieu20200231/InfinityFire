using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxHealth;
    int currentHealth;
    [SerializeField] int maxArmor;
    int currentArmor;
    public int armoring;
    public float CDArmoring;
    private float timeArmoring;

    public ArmorBar armorBar;
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
        currentArmor = maxArmor;

        healthBar.UpdateBar(currentHealth, maxHealth);
        armorBar.UpdateBar(currentArmor, maxArmor);
    }

    private void Update()
    {
        Armoring();
    }

    public void TakeDamage(int damage)
    {
        if (!player.rollOnce)
        {
            currentArmor -= damage;

            if (currentArmor <= 0)
            {
                currentArmor = 0;
                currentHealth -= damage;

                if (currentHealth <= 0)
                {
                    currentHealth = 0;
                    OnDeath.Invoke();
                }

                healthBar.UpdateBar(currentHealth, maxHealth);
            }

            armorBar.UpdateBar(currentArmor, maxArmor);
        }
    }

    void Armoring()
    {
        timeArmoring -= Time.deltaTime;
        if (currentArmor < maxArmor && timeArmoring <= 0)
        {
            timeArmoring = CDArmoring;
            currentArmor += 20;
        }
        if (currentArmor >= maxArmor)
        {
            currentArmor = maxArmor;
        }

        armorBar.UpdateBar(currentArmor, maxArmor);
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
