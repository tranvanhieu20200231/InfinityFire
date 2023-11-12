using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHealth;
    int currentHealth;

    private Animator animator;
    public UnityEvent OnDeath;

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
        animator = GetComponentInChildren<Animator>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            StartCoroutine(DieWithAnimation());
        }
    }

    IEnumerator DieWithAnimation()
    {
        animator.SetTrigger("Die");
        // Xu ly animation
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        OnDeath.Invoke();
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
