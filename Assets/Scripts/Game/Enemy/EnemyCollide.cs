using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollide : MonoBehaviour
{
    PlayerHealth playerHealth;
    NPCHealth npcHealth;
    public int minDamage;
    public int maxDamage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            playerHealth = collision.collider.GetComponent<PlayerHealth>();
            InvokeRepeating("DamagePlayer", 0, 2f);
        }

        if (collision.collider.CompareTag("NPC"))
        {
            npcHealth = collision.collider.GetComponent<NPCHealth>();
            InvokeRepeating("DamageNPC", 0, 2f);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            playerHealth = null;
            CancelInvoke("DamagePlayer");
        }

        if (collision.collider.CompareTag("NPC"))
        {
            playerHealth = null;
            CancelInvoke("DamageNPC");
        }
    }

    void DamagePlayer()
    {
        int damage = Random.Range(minDamage, maxDamage);
        playerHealth.TakeDamage(damage);
    }

    void DamageNPC()
    {
        int damage = Random.Range(minDamage, maxDamage);
        npcHealth.TakeDamage(damage);
    }
}
