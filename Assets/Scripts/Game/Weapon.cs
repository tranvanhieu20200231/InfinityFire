using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
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
        if (Input.GetMouseButton(0) && timeBtwFire < 0)
        {
            Fire.Play();
            FireBullet();
        }
    }

    void RotateGun()
    {
        // Xac dinh vi tri tam con tro chuot
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0.5f, -0.5f, 0).normalized;
        Vector2 lookDir = mousePos - transform.position;
        // Xac dinh goc xoay sung
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = rotation;
        // Doi chieu sung khi xoay
        if(transform.eulerAngles.z > 90 && transform.eulerAngles.z < 270)
        {
            transform.localScale = new Vector3(1, -1, 0);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 0);
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

    void SetOrderInLayer(int order)
    {
        // Order in layer cho SpriteRenderer
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sortingOrder = order;
        }
    }

    void FireBullet()
    {
        timeBtwFire = TimeBtwFire;

        if (shotGun)
        {
            for (int i = 0; i < bulletsPerShot; i++)
            {
                FireShotGunBullet();
            }
        }
        else
        {
            FireSingleBullet();
        }

        // Hieu ung
        Instantiate(muzzle, firePos.position, transform.rotation, transform);
    }

    void FireSingleBullet()
    {
        GameObject bulletTmp = Instantiate(bullet, firePos.position, Quaternion.identity);
        Rigidbody2D rb = bulletTmp.GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * bulletForce, ForceMode2D.Impulse);
    }

    void FireShotGunBullet()
    {
        float spreadAngle = Random.Range(-15f, 15f);
        GameObject bulletTmp = Instantiate(bullet, firePos.position, Quaternion.identity);
        // Goc lech ngau nhien
        Vector2 bulletDirection = Quaternion.Euler(0, 0, spreadAngle) * transform.right;

        Rigidbody2D rb = bulletTmp.GetComponent<Rigidbody2D>();
        rb.AddForce(bulletDirection * bulletForce, ForceMode2D.Impulse);
    }
}
