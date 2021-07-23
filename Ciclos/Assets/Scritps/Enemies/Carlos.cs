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
        CommonBullet currentBullet = Instantiate(
            original: bullet,
            position: transform.position,
            rotation: transform.rotation
        ).GetComponent<CommonBullet>();

        currentBullet.damage = damage;
        currentBullet.speed = bulletSpeed;
        currentBullet.dir = (int) transform.localScale.x;
    }
}