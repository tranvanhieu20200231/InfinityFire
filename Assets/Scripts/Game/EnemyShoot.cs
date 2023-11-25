using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [Header("Attack Parameter")]
    public GameObject bullet;
    public float bulletSpeed;
    public float timeBtwFire;
    public bool isRadiantBullets;  // Thêm biến để xác định loại đạn

    private float fireCD;

    private void Update()
    {
        fireCD -= Time.deltaTime;
        if (fireCD <= 0)
        {
            fireCD = timeBtwFire;
            EnemyFireBullet();
        }
    }

    void EnemyFireBullet()
    {
        if (isRadiantBullets)
        {
            FireRadiantBullets();
        }
        else
        {
            FireSingleBullet();
        }
    }

    void FireRadiantBullets()
    {
        int numberOfBullets = 36;
        float angleStep = 360f / numberOfBullets;

        for (int i = 0; i < numberOfBullets; i++)
        {
            float angle = i * angleStep;
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
            Vector3 direction = rotation * Vector3.right;

            Vector3 enemyPos = transform.position + new Vector3(0, -1f, 0);
            var bulletTmp = Instantiate(bullet, enemyPos, rotation);
            Rigidbody2D rb = bulletTmp.GetComponent<Rigidbody2D>();
            rb.AddForce(direction.normalized * bulletSpeed, ForceMode2D.Impulse);
        }
    }

    void FireSingleBullet()
    {
        var bulletTmp = Instantiate(bullet, transform.position, Quaternion.identity);

        Rigidbody2D rb = bulletTmp.GetComponent<Rigidbody2D>();
        Vector3 playerPos = FindObjectOfType<Player>().transform.position;
        Vector3 direction = playerPos - transform.position;
        rb.AddForce(direction.normalized * bulletSpeed, ForceMode2D.Impulse);
    }
}
