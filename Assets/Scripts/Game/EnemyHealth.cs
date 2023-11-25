using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHealth;
    int currentHealth;
    [SerializeField] int scoreEnemy;
    Score score;

    public EnemyHealthBar enemyHealthBar;
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
        enemyHealthBar.UpdateBar(currentHealth, maxHealth);
        score = FindObjectOfType<Score>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;

            StartCoroutine(DieWithAnimation());
        }

        enemyHealthBar.UpdateBar(currentHealth, maxHealth);
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
        score.UpdateScore(scoreEnemy);

        Destroy(gameObject);
    }
}
