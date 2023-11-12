using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyShoot : MonoBehaviour
{
    [Header("Attack Parameter")]
    public GameObject bullet;
    public float bulletSpeed;
    public float timeBtwFire;
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
        var bulletTmp = Instantiate(bullet, transform.position, Quaternion.identity);

        Rigidbody2D rb = bulletTmp.GetComponent<Rigidbody2D>();
        Vector3 playerPos = FindObjectOfType<Player>().transform.position;
        Vector3 diretion = playerPos - transform.position;
        rb.AddForce(diretion.normalized * bulletSpeed, ForceMode2D.Impulse);
    }
}
