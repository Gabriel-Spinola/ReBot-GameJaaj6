using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RicarP2 : EnemyPatrol
{
    [Header("Ricardão References")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform shootPos;

    [Header("Ricardão Stats")]
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float damage = 1f;
    [SerializeField] private float bulletSpeed = 2f;
    [SerializeField] private float angle = 90f;

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
            position: shootPos.position,
            rotation: Quaternion.identity
        ).GetComponent<CommonBullet>();

        currentBullet.damage = damage;
        currentBullet.speed = bulletSpeed;
        currentBullet.dir = new Vector2(0f, 1f);
        currentBullet.xScale = 1f;

        currentBullet.transform.GetChild(0).GetComponent<Transform>().rotation = Quaternion.Euler(
            x: currentBullet.transform.rotation.x,
            y: currentBullet.transform.rotation.y,
            z: angle
        );

        if (transform.parent.parent.name == "--- Present ---") {
            currentBullet.transform.parent = RoomManager.PresentTemporaryObjects;
        }
        else if (transform.parent.parent.name == "--- Past ---") {
            currentBullet.transform.parent = RoomManager.PastTemporaryObjects;
        }
    }
}