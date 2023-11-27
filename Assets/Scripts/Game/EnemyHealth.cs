using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHealth;
    int currentHealth;
    [SerializeField] int scoreEnemy;
    Score score;

    public EnemyHealthBar enemyHealthBar;
    public DamagePopup damagePopup;
    public GameObject floatingPoints;
    public GameObject fxDestroy;
    private Renderer enemyRenderer;
    private Color flashColor = Color.red;
    private float flashDuration = 0.1f;
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
        damagePopup.UpdateText(0);
        enemyHealthBar.UpdateBar(currentHealth, maxHealth);
        score = FindObjectOfType<Score>();
        enemyRenderer = GetComponentInChildren<Renderer>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;

            //StartCoroutine(DieWithAnimation());
            Death();
        }
        damagePopup.UpdateText(damage);
        enemyHealthBar.UpdateBar(currentHealth, maxHealth);
        StartCoroutine(FlashCharacter());

        Instantiate(floatingPoints, transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0), Quaternion.identity);
    }

    IEnumerator FlashCharacter()
    {
        // Flash the character by changing its color temporarily
        enemyRenderer.material.color = flashColor;

        // Wait for the specified duration
        yield return new WaitForSeconds(flashDuration);

        // Reset the color back to the original color
        enemyRenderer.material.color = Color.white;
    }

    //IEnumerator DieWithAnimation()
    //{
    //    // animator.SetTrigger("Die");
    //    // Xu ly animation
    //    yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

    //    OnDeath.Invoke();
    //}

    public void Death()
    {
        score.UpdateScore(scoreEnemy);

        Destroy(gameObject);
        Instantiate(fxDestroy, transform.position, Quaternion.identity);
    }
}
