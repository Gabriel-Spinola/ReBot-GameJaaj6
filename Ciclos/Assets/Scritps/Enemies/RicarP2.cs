using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RicarP2 : EnemyPatrol
{
    [SerializeField] protected Vector2 soundAreaOffset;
    [SerializeField] protected Vector2 soundAreaSize;

    [Header("Ricardão References")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform shootPos;
    [SerializeField] private Transform player;

    [SerializeField] private LayerMask whatIsWall;

    [Header("Ricardão Stats")]
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float damage = 1f;
    [SerializeField] private float bulletSpeed = 2f;
    [SerializeField] private float angle = 90f;

    private float nextTimeToFire = 0f;

    protected override void Update()
    {
        base.Update();

        if (!Physics2D.Linecast(transform.position, player.position, whatIsWall)) {
            if (Time.time >= nextTimeToFire) {
                nextTimeToFire = Time.time + 1f / fireRate;

                Shoot();

                AudioManager._I.PlaySound2D("Shoot", .8f);
            }
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
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
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube((Vector2) transform.position + soundAreaOffset, soundAreaSize);
    }
}