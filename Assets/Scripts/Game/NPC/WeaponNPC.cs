using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponNPC : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePos;
    public GameObject muzzle;
    public float TimeBtwFire;
    public float bulletForce;
    public bool shotGun = false;
    public int bulletsPerShot = 20;
    private float timeBtwFire;
    [SerializeField] private AudioSource Fire;

    void Update()
    {
        RotateGun();
        timeBtwFire -= Time.deltaTime;

        // Find the nearest enemy with the "Enemy" tag
        GameObject nearestEnemy = FindNearestEnemyWithTag("Enemy");

        if (nearestEnemy != null && timeBtwFire < 0)
        {
            Fire.Play();
            FireBullet(nearestEnemy.transform.position);
        }
    }

    void RotateGun()
    {
        // Find the nearest enemy with the "Enemy" tag
        GameObject nearestEnemy = FindNearestEnemyWithTag("Enemy");

        if (nearestEnemy != null)
        {
            Vector2 lookDir = nearestEnemy.transform.position - transform.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            transform.rotation = rotation;

            // Doi chieu sung khi xoay
            if (transform.eulerAngles.z > 90 && transform.eulerAngles.z < 270)
            {
                transform.localScale = new Vector3(1, -1, 0) * 25 / 3;
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 0) * 25 / 3;
            }

            // Doi layer sung khi xoay
            if (transform.eulerAngles.z > 0 && transform.eulerAngles.z < 180)
            {
                SetOrderInLayer(0);
            }
            else
            {
                SetOrderInLayer(2);
            }
        }
    }

    void SetOrderInLayer(int order)
    {
        // Order in layer cho SpriteRenderer
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sortingOrder = order;
        }
    }

    GameObject FindNearestEnemyWithTag(string tag)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(tag);
        GameObject nearestEnemy = null;
        float nearestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);

            if (distance < nearestDistance)
            {
                nearestEnemy = enemy;
                nearestDistance = distance;
            }
        }

        return nearestEnemy;
    }

    void FireBullet(Vector3 targetPosition)
    {
        timeBtwFire = TimeBtwFire;

        if (shotGun)
        {
            for (int i = 0; i < bulletsPerShot; i++)
            {
                FireShotGunBullet(targetPosition);
            }
        }
        else
        {
            FireSingleBullet(targetPosition);
        }

        // Hieu ung
        Instantiate(muzzle, firePos.position, transform.rotation, transform);
    }

    void FireSingleBullet(Vector3 targetPosition)
    {
        GameObject bulletTmp = Instantiate(bullet, firePos.position, Quaternion.identity);
        Rigidbody2D rb = bulletTmp.GetComponent<Rigidbody2D>();
        rb.AddForce((targetPosition - firePos.position).normalized * bulletForce, ForceMode2D.Impulse);
    }

    void FireShotGunBullet(Vector3 targetPosition)
    {
        float spreadAngle = Random.Range(-15f, 15f);
        GameObject bulletTmp = Instantiate(bullet, firePos.position, Quaternion.identity);
        // Goc lech ngau nhien
        Vector2 bulletDirection = Quaternion.Euler(0, 0, spreadAngle) * (targetPosition - firePos.position).normalized;

        Rigidbody2D rb = bulletTmp.GetComponent<Rigidbody2D>();
        rb.AddForce(bulletDirection * bulletForce, ForceMode2D.Impulse);
    }
}