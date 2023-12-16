using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarGun : MonoBehaviour
{
    [Header("Attack Parameter")]
    public GameObject bullet;
    public float bulletSpeed;
    public float timeBtwFire;
    public int numberOfBullets = 36;
    [SerializeField] private AudioSource Fire;

    private float fireCD;

    private void Update()
    {
        fireCD -= Time.deltaTime;
        if (fireCD <= 0)
        {
            fireCD = timeBtwFire;
            Fire.Play();
            FireRadiantBullets();
        }
    }

    void FireRadiantBullets()
    {
        float angleStep = 360f / numberOfBullets;

        for (int i = 0; i < numberOfBullets; i++)
        {
            float angle = i * angleStep;
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
            Vector3 direction = rotation * Vector3.right;

            var bulletTmp = Instantiate(bullet, transform.position, rotation);
            Rigidbody2D rb = bulletTmp.GetComponent<Rigidbody2D>();
            rb.AddForce(direction.normalized * bulletSpeed, ForceMode2D.Impulse);
        }
    }
}
