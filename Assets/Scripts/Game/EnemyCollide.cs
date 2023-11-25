using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollide : MonoBehaviour
{
    PlayerHealth playerHealth;
    public int minDamage;
    public int maxDamage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            playerHealth = collision.collider.GetComponent<PlayerHealth>();
            InvokeRepeating("DamagePlayer", 0, 2f);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            playerHealth = null;
            CancelInvoke("DamagePlayer");
        }
    }

    void DamagePlayer()
    {
        int damage = Random.Range(minDamage, maxDamage);
        playerHealth.TakeDamage(damage);
    }
}
