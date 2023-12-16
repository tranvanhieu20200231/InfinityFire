using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainBulletDamage : MonoBehaviour
{
    PlayerHealth playerHealth;
    NPCHealth npcHealth;
    public int minDamage;
    public int maxDamage;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider != null && collider.CompareTag("Player"))
        {
            playerHealth = collider.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                DamagePlayer();
            }
        }

        if (collider != null && collider.CompareTag("NPC"))
        {
            npcHealth = collider.GetComponent<NPCHealth>();
            if (npcHealth != null)
            {
                DamageNPC();
            }
        }
    }

    void DamagePlayer()
    {
        if (playerHealth != null)
        {
            int damage = Random.Range(minDamage, maxDamage);
            playerHealth.TakeDamage(damage);
        }
    }

    void DamageNPC()
    {
        if (npcHealth != null)
        {
            int damage = Random.Range(minDamage, maxDamage);
            npcHealth.TakeDamage(damage);
        }
    }

}
