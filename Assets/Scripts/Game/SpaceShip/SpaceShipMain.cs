using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipMain : MonoBehaviour
{
    void Start()
    {
        gameObject.tag = "Untagged";
    }

    void Update()
    {
        GameObject bigGunRightObject = GameObject.Find("BigGunRight");
        GameObject bigGunLeftObject = GameObject.Find("BigGunLeft");
        GameObject centerGunObject = GameObject.Find("CenterGun");
        GameObject smallGunRightObject = GameObject.Find("SmallGunRight");
        GameObject smallGunLeftObject = GameObject.Find("SmallGunLeft");
        GameObject radarObject = GameObject.Find("Radar");
        GameObject rainBulletObject = GameObject.Find("RainBullet");

        if (bigGunRightObject == null &&
            bigGunLeftObject == null &&
            centerGunObject == null &&
            smallGunRightObject == null &&
            smallGunLeftObject == null &&
            radarObject == null &&
            rainBulletObject == null)
        {
            gameObject.tag = "Enemy";
        }
    }
}
