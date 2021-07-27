using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collision))]
public class MovingPulseShooter : PulseShooter
{
    [Header("Patrol References")]
    [SerializeField] private Transform groundCheck;

    [SerializeField] private LayerMask whatIsBlock;

    [Header("Patrol Stats")]
    [SerializeField] private float walkSpeed = 10f;

    private Rigidbody2D rb = null;
    private Collision col = null;

    private bool mustPatrol = true;
    private bool mustTurn = false;

    protected override void Awake()
    {
        base.Awake();

        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collision>();
    }

    protected override void Update()
    {
        base.Update();

        if (mustPatrol) {
            Patrol();
        }
    }

    private void FixedUpdate()
    {
        if (mustPatrol) {
            mustTurn = !Physics2D.OverlapCircle(groundCheck.position, .1f, whatIsBlock);
        }
    }

    private void Patrol()
    {
        if (mustTurn || col.isOnWall)
            Flip();

        rb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }

    private void Flip()
    {
        mustPatrol = false;

        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        walkSpeed *= -1;

        mustPatrol = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(groundCheck.position, .1f);
    }
}
