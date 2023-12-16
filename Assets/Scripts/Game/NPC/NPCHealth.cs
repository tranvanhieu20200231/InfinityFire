using Cinemachine;
using FirstGearGames.SmoothCameraShaker;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class NPCHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] int maxHealth;
    int currentHealth;
    public float timeDestroy = 60f;

    [Header("Camera Shake Parameters")]
    private CameraShake cameraShake;
    [SerializeField] private float shakeIntensity = 3;
    [SerializeField] private float shakeTime = 0.2f;
    [SerializeField] private AudioSource Hurt;

    public GameObject npcDestroy;
    private HealthBar npcHealthBar;
    private GameObject npcHealthBarUI;
    private Color flashColor = Color.red;
    private float flashDuration = 0.2f;
    private Renderer characterSR;
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
        currentHealth = maxHealth;

        npcHealthBar = GameObject.Find("NPCHealthBar").GetComponent<HealthBar>();
        npcHealthBar.UpdateBar(currentHealth, maxHealth);
        npcHealthBarUI = GameObject.Find("NPCHealthBar");
        cameraShake = GameObject.Find("CameraFollowPlayer").GetComponent<CameraShake>();
        characterSR = GetComponentInChildren<Renderer>();

        Invoke("Death", timeDestroy);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Hurt.Play();
        cameraShake.ShakeCamera(shakeIntensity, shakeTime);
        StartCoroutine(FlashCharacter());

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            OnDeath.Invoke();
        }

        npcHealthBar.UpdateBar(currentHealth, maxHealth);
    }

    IEnumerator FlashCharacter()
    {
        characterSR.material.color = flashColor;

        yield return new WaitForSeconds(flashDuration);

        characterSR.material.color = Color.white;
    }

    public void Death()
    {
        Destroy(gameObject);
        npcHealthBarUI.SetActive(false);
        Instantiate(npcDestroy, transform.position, Quaternion.identity);
    }
}
