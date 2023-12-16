using System.Collections;
using UnityEngine;

public class RainGun : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePos;
    public GameObject muzzle;
    public GameObject spawnWarning;
    public GameObject spawnBullets;
    public GameObject spawnFXExplosion;
    public GameObject spawnExplosion;
    public float TimeBtwFire;
    public float bulletForce;
    public float spawnRadius = 30f;
    public int maxRainBullet = 5;
    private float timeBtwFire = 0;
    [SerializeField] private AudioSource Fire;
    [SerializeField] private AudioSource Explosion;

    void Update()
    {
        timeBtwFire -= Time.deltaTime;

        if (timeBtwFire < 0)
        {
            timeBtwFire = TimeBtwFire;
            Fire.Play();
            FireBullet();
        }
    }

    void FireBullet()
    {
        GameObject bulletTmp = Instantiate(bullet, firePos.position, Quaternion.identity);
        Rigidbody2D rb = bulletTmp.GetComponent<Rigidbody2D>();

        rb.AddForce(Vector2.up * bulletForce, ForceMode2D.Impulse);
        Instantiate(muzzle, firePos.position, Quaternion.identity, transform);
        for (int i = 0; i < maxRainBullet; i++)
        {
            SpawnObjectWithinRadius();
        }
    }

    void SpawnObjectWithinRadius()
    {
        Vector2 randomPosition = Random.insideUnitCircle * spawnRadius;
        Vector3 spawnWarningPosition = firePos.position + new Vector3(randomPosition.x, randomPosition.y, 0f);
        Instantiate(spawnWarning, spawnWarningPosition, Quaternion.identity);

        Vector3 spawnBulletPosition = spawnWarningPosition + new Vector3(0f, 60f, 0f);
        GameObject bulletDown = Instantiate(spawnBullets, spawnBulletPosition, Quaternion.identity);
        Rigidbody2D rb = bulletDown.GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.down * bulletForce, ForceMode2D.Impulse);

        StartCoroutine(SpawnExplosion(spawnWarningPosition));
    }

    IEnumerator SpawnExplosion(Vector3 spawnPosition)
    {
        yield return new WaitForSeconds(2f);

        Explosion.Play();
        Instantiate(spawnFXExplosion, spawnPosition, Quaternion.identity);
        Instantiate(spawnExplosion, spawnPosition, Quaternion.identity);
    }
}
