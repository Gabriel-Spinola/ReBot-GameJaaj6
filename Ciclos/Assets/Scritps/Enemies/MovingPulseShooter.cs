using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collision))]
public class MovingPulseShooter : PulseShooter
{
    [Header("Config")]
    [SerializeField] private bool isVerticalMovement = false;

    [Header("Patrol References")]
    [SerializeField] private Transform groundCheck;

    [SerializeField] private LayerMask whatIsBlock;

    [Header("Patrol Stats")]
    [SerializeField] private float walkSpeed = 10f;

    private Rigidbody2D rb = null;
    private Collision col = null;

    private Vector2 initialPos = Vector2.zero;

    private bool mustPatrol = true;
    private bool mustTurn = false;
    private bool canFlip = true;

    protected override void Awake()
    {
        base.Awake();

        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collision>();
    }

    protected override void Start()
    {
        base.Start();

        initialPos = transform.position;
    }

    protected override void Update()
    {
        base.Update();

        if (TimeGauntlet.usedGauntlet) {
            ResetValues();
        }

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

        if (isVerticalMovement) {
            rb.velocity = new Vector2(rb.velocity.x, walkSpeed * Time.fixedDeltaTime);
        }
        else {
            rb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, rb.velocity.y);
        }
    }

    private void Flip()
    {
        if (!canFlip)
            return;

        mustPatrol = false;

        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        walkSpeed *= -1;

        mustPatrol = true;

        StartCoroutine(DisableFlip(.2f));
    }

    protected override void ResetValues()
    {
        StopAllCoroutines();
        mustPatrol = true;
        mustTurn = false;
        canFlip = true;
        rb.velocity = Vector2.zero;
        transform.position = initialPos;
    }

    private IEnumerator DisableFlip(float time)
    {
        canFlip = false;

        yield return new WaitForSeconds(time);

        canFlip = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(groundCheck.position, .1f);
    }
}
