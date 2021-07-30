using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carlos : Enemy
{
    [Header("Carlos References")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform player;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private ParticleSystem shootParticle;

    [SerializeField] private LayerMask whatIsWall;

    [Header("Carlos Stats")]
    [SerializeField] private bool shouldAim = false;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float damage = 1f;
    [SerializeField] private float bulletSpeed = 2f;

    [SerializeField] private Animator anim = null;
    [SerializeField] private Animator scaleAnim = null;

    private float lookDir = 0f;

    private float nextTimeToFire = 0f;

    private void Update()
    {
        if (shouldAim && !Physics2D.Linecast(transform.position, player.position, whatIsWall)) {
            lookDir = StaticRes.LookDir(transform.position, player.position);
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, lookDir));

            if (Time.time >= nextTimeToFire) {
                nextTimeToFire = Time.time + 1f / fireRate;

                Shoot();
                shootParticle.Play();
            }
        }
        else {
            if (Time.time >= nextTimeToFire) {
                nextTimeToFire = Time.time + 1f / fireRate;

                Shoot();
                shootParticle.Play();
            }
        }
    }

    private void Shoot()
    {
        CommonBullet currentBullet = Instantiate(bullet, shootPoint.position, transform.rotation).GetComponent<CommonBullet>();

        currentBullet.damage = damage;
        currentBullet.speed = bulletSpeed;
        currentBullet.dir = new Vector2((int) transform.localScale.x, 0f);
        currentBullet.xScale = (int) transform.localScale.x;

        if (anim != null) {
            anim.SetTrigger("Shoot");
        }

        scaleAnim.SetTrigger("Squash");

        if (transform.parent.parent.name == "--- Present ---") {
            currentBullet.transform.parent = RoomManager.PresentTemporaryObjects;
        }
        else if (transform.parent.parent.name == "--- Past ---") {
            currentBullet.transform.parent = RoomManager.PastTemporaryObjects;
        }
    }
}