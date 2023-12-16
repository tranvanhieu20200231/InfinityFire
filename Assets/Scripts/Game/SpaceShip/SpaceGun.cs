using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceGun : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePos;
    public GameObject muzzle;
    public float TimeBtwFire;
    public float bulletForce;
    private float timeBtwFire;
    [SerializeField] private AudioSource Fire;

    void Update()
    {
        RotateGun();
        timeBtwFire -= Time.deltaTime;

        Vector3 targetPosition = FindTargetPosition();

        if (targetPosition != Vector3.zero && timeBtwFire < 0)
        {
            Fire.Play();
            FireBullet(targetPosition);
        }
    }

    void RotateGun()
    {
        Vector3 targetPosition = FindTargetPosition();

        if (targetPosition != Vector3.zero)
        {
            Vector2 lookDir = targetPosition - transform.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg + 90f;

            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            transform.rotation = rotation;
        }
    }

    Vector3 FindTargetPosition()
    {
        NPC npc = FindObjectOfType<NPC>();
        Player player = FindObjectOfType<Player>();

        if (npc != null)
        {
            return npc.transform.position;
        }
        else if (player != null)
        {
            return player.transform.position;
        }

        return Vector3.zero;
    }

    void FireBullet(Vector3 targetPosition)
    {
        timeBtwFire = TimeBtwFire;

        FireSingleBullet(targetPosition);
        // Hieu ung
        Instantiate(muzzle, firePos.position, Quaternion.identity);
    }

    void FireSingleBullet(Vector3 targetPosition)
    {
        GameObject bulletTmp = Instantiate(bullet, firePos.position, Quaternion.identity);
        Rigidbody2D rb = bulletTmp.GetComponent<Rigidbody2D>();
        rb.AddForce((targetPosition - firePos.position).normalized * bulletForce, ForceMode2D.Impulse);
    }
}
