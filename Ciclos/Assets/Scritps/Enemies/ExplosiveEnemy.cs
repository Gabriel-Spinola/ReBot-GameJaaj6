using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveEnemy : EnemyPatrol
{
    [Header("Explosive Enemy Stats")]
    [SerializeField] private float waitToExplode = 1.5f;
    [Range(1f, 5f)]
    [SerializeField] private float attackRange = 1f;
    [Range(1f, 5f)]
    [SerializeField] private float explosionRadius = 1.2f;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (Physics2D.OverlapCircle(transform.position, attackRange, whatIsPlayer)) {
            mustPatrol = false;
            walkSpeed = 0f;

            StartCoroutine(Explode(waitToExplode));
        }
    }

    private IEnumerator Explode(float time)
    {
        yield return new WaitForSeconds(time);

        Collider2D hitted = Physics2D.OverlapCircle(transform.position, explosionRadius, whatIsPlayer);

        if (hitted) {
            hitted.GetComponent<Player>().TakeDamage();
        }

        Die();

        yield return 0;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
