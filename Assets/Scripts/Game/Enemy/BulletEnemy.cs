using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    public int minDamage;
    public int maxDamage;

    public GameObject bulletDestroy;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Xu ly va cham cua bullet voi player
        if (other.CompareTag("Player"))
        {
            int damage = Random.Range(minDamage, maxDamage);
            other.GetComponent<PlayerHealth>().TakeDamage(damage);
            Destroy(gameObject);
            Instantiate(bulletDestroy, transform.position, Quaternion.identity);
        }
        // Xu ly va cham cua bullet voi npc
        if (other.CompareTag("NPC"))
        {
            int damage = Random.Range(minDamage, maxDamage);
            other.GetComponent<NPCHealth>().TakeDamage(damage);
            Destroy(gameObject);
            Instantiate(bulletDestroy, transform.position, Quaternion.identity);
        }
        // Xu ly va cham khien
        if (other.CompareTag("Shield"))
        {
            Destroy(gameObject);
            Instantiate(bulletDestroy, transform.position, Quaternion.identity);
        }
        // Xu ly va cham cua bullet voi obstacle
        if (other.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
            Instantiate(bulletDestroy, transform.position, Quaternion.identity);
        }
    }
}
