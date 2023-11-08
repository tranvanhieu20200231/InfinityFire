﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePos;
    public GameObject muzzle;
    public float TimeBtwFire;
    public float bulletForce;
    private float timeBtwFire;

    void Update()
    {
        RotateGun();
        timeBtwFire -= Time.deltaTime;
        if (Input.GetMouseButton(0) && timeBtwFire < 0)
        {
            FireBullet();
        }
    }

    void RotateGun()
    {
        // Xac dinh vi tri con tro chuot
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
    }

    void FireBullet()
    {
        timeBtwFire = TimeBtwFire;

        GameObject bullerTmp = Instantiate(bullet, firePos.position, Quaternion.identity);
        // Hieu ung
        Instantiate(muzzle, firePos.position, transform.rotation, transform);
        Rigidbody2D rb = bullerTmp.GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * bulletForce, ForceMode2D.Impulse);
    }
}