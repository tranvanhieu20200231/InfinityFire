using Cinemachine;
using FirstGearGames.SmoothCameraShaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] int maxHealth;
    int currentHealth;
    public DamagePopup healthPopup;
    public GameObject healthPopupPoints;

    [Header("Armor")]
    [SerializeField] int maxArmor;
    int currentArmor;
    public int armoring;
    public float CDArmoring;
    private float timeArmoring;
    public DamagePopup armorPopup;
    public GameObject armorPopupPoints;

    [Header("Camera Shake Parameters")]
    [SerializeField] private CameraShake cameraShake;
    [SerializeField] private float shakeIntensity = 3;
    [SerializeField] private float shakeTime = 0.2f;
    [SerializeField] private AudioSource Hurt;

    private Color flashColor = Color.red;
    private float flashDuration = 0.2f;
    private Renderer characterSR;
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
        healthPopup.UpdateText(0);
        armorBar.UpdateBar(currentArmor, maxArmor);
        armorPopup.UpdateText(0);

        characterSR = GetComponentInChildren<Renderer>();
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
            Hurt.Play();
            cameraShake.ShakeCamera(shakeIntensity, shakeTime);
            StartCoroutine(FlashCharacter());

            if (currentArmor <= 0)
            {
                currentArmor = 0;
                currentHealth -= damage;

                if (currentHealth <= 0)
                {
                    currentHealth = 0;
                    OnDeath.Invoke();
                }

                //healthPopup.UpdateText(-damage);
                //Instantiate(healthPopupPoints, transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0), Quaternion.identity);
                healthBar.UpdateBar(currentHealth, maxHealth);
            }
            else
            {

                //armorPopup.UpdateText(-damage);
                //Instantiate(armorPopupPoints, transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0), Quaternion.identity);
                armorBar.UpdateBar(currentArmor, maxArmor);
            }
        }
    }

    void Armoring()
    {
        timeArmoring -= Time.deltaTime;
        if (currentArmor < maxArmor && timeArmoring <= 0)
        {
            timeArmoring = CDArmoring;
            currentArmor += armoring;
            //armorPopup.UpdateText(armoring);
            //Instantiate(armorPopupPoints, transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0), Quaternion.identity);
        }
        if (currentArmor >= maxArmor)
        {
            currentArmor = maxArmor;
        }

        armorBar.UpdateBar(currentArmor, maxArmor);
    }

    IEnumerator FlashCharacter()
    {
        // Flash the character by changing its color temporarily
        characterSR.material.color = flashColor;

        // Wait for the specified duration
        yield return new WaitForSeconds(flashDuration);

        // Reset the color back to the original color
        characterSR.material.color = Color.white;
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
