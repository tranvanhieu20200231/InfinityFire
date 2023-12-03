using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int minDamage;
    public int maxDamage;
    public bool playerBullet;   // Player fire
    public bool pierce;         // Xuyen giap

    public GameObject bulletDestroy;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Xu ly va cham cua bullet voi player, enemy
        if (other.CompareTag("Player") && !playerBullet)
        {
            int damage = Random.Range(minDamage, maxDamage);
            other.GetComponent<PlayerHealth>().TakeDamage(damage);
            Destroy(gameObject);
            Instantiate(bulletDestroy, transform.position, Quaternion.identity);
        }
        // Xu ly va cham khien
        if (other.CompareTag("Shield") && !playerBullet)
        {
            Destroy(gameObject);
            Instantiate(bulletDestroy, transform.position, Quaternion.identity);
        }

        if (other.CompareTag("Enemy") && playerBullet && !pierce)
        {
            int damage = Random.Range(minDamage, maxDamage);
            other.GetComponent<EnemyHealth>().TakeDamage(damage);
            Destroy(gameObject);
            Instantiate(bulletDestroy, transform.position, Quaternion.identity);
        }

        if (other.CompareTag("Enemy") && playerBullet && pierce)
        {
            int damage = Random.Range(minDamage, maxDamage);
            other.GetComponent<EnemyHealth>().TakeDamage(damage);
        }
        // Xu ly va cham cua bullet voi obstacle
        if (other.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
            Instantiate(bulletDestroy, transform.position, Quaternion.identity);
        }
    }
}
