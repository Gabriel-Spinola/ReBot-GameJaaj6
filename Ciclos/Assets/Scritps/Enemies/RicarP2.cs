using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RicarP2 : EnemyPatrol
{
    [Header("Ricardão References")]
    [SerializeField] private GameObject bullet;

    [Header("Ricardão Stats")]
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float damage = 1f;
    [SerializeField] private float bulletSpeed = 2f;

    private float nextTimeToFire = 0f;

    protected override void Update()
    {
        base.Update();

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
            rotation: Quaternion.identity
        ).GetComponent<CommonBullet>();

        currentBullet.damage = damage;
        currentBullet.speed = bulletSpeed;
        currentBullet.dir = new Vector2(0f, 1f);
        currentBullet.xScale = 1f;

        currentBullet.transform.GetChild(0).GetComponent<Transform>().rotation = Quaternion.Euler(
            x: currentBullet.transform.rotation.x,
            y: currentBullet.transform.rotation.y,
            z: 90f
        );
    }
}