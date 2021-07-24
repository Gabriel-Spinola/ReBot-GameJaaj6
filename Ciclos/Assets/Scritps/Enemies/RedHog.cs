using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedHog : EnemyPatrol
{
    [Header("RedHog References")]
    [SerializeField] private GameObject bullet;

    [Header("RefHog Stats")]
    [SerializeField] private float friction = .2f;
    [Range(1f, 5f)]
    [SerializeField] private float attackRange = 1f;
    [Tooltip("In Seconds")]
    [SerializeField] private float waitToAttack = 1.5f;

    [Header("Redhog Bullet Config")]
    [SerializeField] private float bulletSpeed = 8f;
    [SerializeField] private float damage = 1f;

    [Header("RedHog Shooting")]
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private int amountOfBulletsPerShoot = 5;
    [SerializeField] private float initialAngle = 25f;
    [SerializeField] private float angle = 25f;
    [SerializeField] private float maxRotation = 70f;
    
    private float nextTimeToFire = 0f;
    private float currentShootAngle = 0;

    private bool isAttacking = false;

    private void Start()
    {
        currentShootAngle = initialAngle;
    }

    protected override void Update()
    {
        if (!isAttacking)
            base.Update();

        if (isAttacking) {
            rb.velocity = Vector2.right * Mathf.Lerp(rb.velocity.x, 0f, friction) + Vector2.up * rb.velocity.y;

            StartCoroutine(Attack(waitToAttack));
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        isAttacking = Physics2D.OverlapCircle(transform.position, attackRange, whatIsPlayer);
    }

    private IEnumerator Attack(float time)
    {
        yield return new WaitForSeconds(time);

        if (Time.time >= nextTimeToFire) {
            nextTimeToFire = Time.time + 1f / fireRate;

            for (int i = 1; i <= amountOfBulletsPerShoot; i++) {
                if (currentShootAngle >= amountOfBulletsPerShoot * angle || currentShootAngle >= maxRotation) {
                    currentShootAngle = initialAngle;
                }

                currentShootAngle += i * angle;

                CommonBullet bullet_ = Instantiate(bullet, transform.position, transform.rotation).GetComponent<CommonBullet>();

                bullet_.damage = damage;
                bullet_.speed = bulletSpeed;
                bullet_.dir = bullet_.transform.right;
                bullet_.xScale = bullet_.transform.localScale.x;
                bullet_.transform.rotation = Quaternion.Euler(bullet_.transform.rotation.x, bullet_.transform.rotation.y, currentShootAngle);
            }
        }

        yield return 0;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
