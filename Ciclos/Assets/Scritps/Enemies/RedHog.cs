using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedHog : EnemyPatrol
{
    [Header("RedHog References")]
    [SerializeField] private GameObject bullet;

    [Header("Redhog Stats")]
    [Range(1f, 5f)]
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float waitToAttack = 1.5f;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float damage = 1f;
    [SerializeField] private float bulletSpeed = 8f;
    [SerializeField] private int shootAngles = 5;

    private float nextTimeToFire = 0f;
    private int angles = 0;

    private void Start()
    {
        
    }

    private new void Update()
    {
        base.Update();

        if (Physics2D.OverlapCircle(transform.position, attackRange, whatIsPlayer)) {
            mustPatrol = false;
            walkSpeed = 0f;

            StartCoroutine(Attack(waitToAttack));
        }
    }

    private IEnumerator Attack(float time)
    {
        yield return new WaitForSeconds(time);

        if (Time.time >= nextTimeToFire) {
            nextTimeToFire = Time.time + 1f / fireRate;

            for (int i = 1; i <= shootAngles; i++) {
                angles += i * 25;
            }

            CommonBullet bullet_ = Instantiate(bullet, transform.position, transform.rotation).GetComponent<CommonBullet>();

            bullet_.damage = damage;
            bullet_.speed = bulletSpeed;
            bullet_.dir = bullet_.transform.right;
            bullet_.xScale = bullet_.transform.localScale.x;
            bullet_.transform.rotation = Quaternion.Euler(bullet_.transform.rotation.x, bullet_.transform.rotation.y, angles);
        }

        yield return 0;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
