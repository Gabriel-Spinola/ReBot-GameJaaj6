using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveEnemy : EnemyPatrol
{
    [Header("Explosive Enemy References")]
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private Transform player;
    [SerializeField] private LineRenderer line;

    [SerializeField] private LayerMask whatIsWall;

    [Header("Explosive Enemy Stats")]
    [SerializeField] private float waitToExplode = 1.5f;
    [Range(1f, 5f)]
    [SerializeField] private float attackRange = 1f;
    [Range(1f, 10f)]
    [SerializeField] private float explosionRadius = 1.2f;

    [Header("Dangerous Circel")]
    [Range(0, 50)]
    [SerializeField] private int segments = 50;

    private Animator anim = null;

    private void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();

        line.positionCount = segments + 1;
        line.useWorldSpace = false;

        CreateCircle();
    }

    private void CreateCircle()
    {
        float x;
        float y;

        float angle = 20f;

        for (int i = 0; i < ( segments + 1 ); i++) {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * 3.55f;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * 3.55f;

            line.SetPosition(i, new Vector3(x, y, 0));

            angle += (360f / segments);
        }
    }

    protected override void Awake()
    {
        base.Awake();

        anim = GetComponent<Animator>();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (Physics2D.OverlapCircle(transform.position, attackRange, whatIsPlayer) && !Physics2D.Linecast(transform.position, player.position, whatIsWall)) {
            mustPatrol = false;
            walkSpeed = 0f;

            StartCoroutine(Explode(waitToExplode));
        }
    }

    private IEnumerator Explode(float time)
    {
        anim.SetBool("IsExploding", true);

        yield return new WaitForSeconds(time);

        Explosion explosion_ = Instantiate(explosionEffect, transform.position, Quaternion.identity).GetComponent<Explosion>();
        explosion_.radius = explosionRadius;

        Collider2D hitted = Physics2D.OverlapCircle(transform.position, explosionRadius, whatIsPlayer);

        if (hitted) {
            hitted.GetComponent<Player>().TakeDamage();
        }

        anim.SetBool("IsExploding", false);

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
