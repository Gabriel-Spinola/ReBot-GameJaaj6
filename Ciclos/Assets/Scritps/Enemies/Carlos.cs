using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carlos : Enemy
{
    [Header("Carlos References")]
    [SerializeField] private GameObject bullet;

    [Header("Carlos Stats")]
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float damage = 1f;
    [SerializeField] private float bulletSpeed = 2f;

    private float nextTimeToFire = 0f;

    private void Update()
    {
        if (Time.time >= nextTimeToFire) {
            nextTimeToFire = Time.time + 1f / fireRate;

            Shoot();   
        }
    }

    private void Shoot()
    {
        CommonBullet currentBullet = Instantiate(bullet, transform.position, transform.rotation).GetComponent<CommonBullet>();

        currentBullet.damage = damage;
        currentBullet.speed = bulletSpeed;
        currentBullet.dir = new Vector2((int) transform.localScale.x, 0f);
        currentBullet.xScale = (int) transform.localScale.x;
    }
}