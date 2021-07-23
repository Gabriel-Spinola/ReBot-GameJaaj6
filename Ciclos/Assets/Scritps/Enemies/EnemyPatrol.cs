using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collision))]
public abstract class EnemyPatrol : Enemy
{
    [Header("Patrol References")]
    [SerializeField] protected Transform groundCheck;

    [SerializeField] protected LayerMask whatIsBlock;

    [Header("Patrol Stats")]
    [SerializeField] protected float walkSpeed = 10f;

    protected Rigidbody2D rb = null;
    protected Collision col = null;

    protected bool mustPatrol = true;
    protected bool mustTurn = false;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collision>();
    }

    protected virtual void Update()
    {
        if (mustPatrol) {
            Patrol();
        }
    }

    protected virtual void FixedUpdate()
    {
        if (mustPatrol) {
            mustTurn = !Physics2D.OverlapCircle(groundCheck.position, .1f, whatIsBlock);
        }
    }

    protected virtual void Patrol()
    {
        if (mustTurn || col.isOnWall)
            Flip();

        rb.velocity = new Vector2(walkSpeed, rb.velocity.y);
    }

    protected virtual void Flip()
    {
        mustPatrol = false;

        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        walkSpeed *= -1;

        mustPatrol = true;
    }
}
