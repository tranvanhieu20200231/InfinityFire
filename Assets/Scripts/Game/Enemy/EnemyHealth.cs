using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHealth;
    int currentHealth;
    [SerializeField] int minScore;
    [SerializeField] int maxScore;
    int scoreEnemy;
    Score score;

    [Header("Camera Shake Parameters")]
    private CameraShake cameraShake;
    private float shakeIntensity = 3;
    private float shakeTime = 0.1f;
    [SerializeField] private AudioSource enemyHurt;

    private bool isDead = false;
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

        scoreEnemy = Random.Range(minScore, maxScore);
        currentHealth = maxHealth;
        damagePopup.UpdateText(0);
        enemyHealthBar.UpdateBar(currentHealth, maxHealth);
        score = FindObjectOfType<Score>();

        enemyRenderer = GetComponentInChildren<Renderer>();

        cameraShake = GameObject.Find("CameraFollowPlayer").GetComponent<CameraShake>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;

            Death();
        }
        damagePopup.UpdateText(damage);
        enemyHealthBar.UpdateBar(currentHealth, maxHealth);
        enemyHurt.Play();
        cameraShake.ShakeCamera(shakeIntensity, shakeTime);
        StartCoroutine(FlashCharacter());

        Instantiate(floatingPoints, transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0), Quaternion.identity);
    }

    IEnumerator FlashCharacter()
    {
        enemyRenderer.material.color = flashColor;

        yield return new WaitForSeconds(flashDuration);

        enemyRenderer.material.color = Color.white;
    }

    public void Death()
    {
        if (!isDead)
        {
            isDead = true;

            Player player = FindObjectOfType<Player>();
            if (player != null)
            {
                player.ReduceRollCDT(1f);
            }

            PlayerSkill playerSkill = FindObjectOfType<PlayerSkill>();
            if (playerSkill != null)
            {
                playerSkill.ReduceShieldCDT(1f);
            }

            PlayerSummon playerSummon = FindObjectOfType<PlayerSummon>();
            if (playerSummon != null)
            {
                playerSummon.ReduceSummonCDT(1f);
            }

            //NPC npc = FindObjectOfType<NPC>();
            //if (npc != null)
            //{
            //    npc.Reward();
            //}

            score.UpdateScore(scoreEnemy);
            Destroy(gameObject);
            Instantiate(fxDestroy, transform.position, Quaternion.identity);
        }
    }
}
