using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrol : MonoBehaviour
{
    [SerializeField] private Transform groundCheck;

    [SerializeField] private LayerMask whatIsBlock;

    [HideInInspector] public float walkSpeed = 10f;

    private bool mustPatrol = true;
    private bool mustTurn = false;

    private Rigidbody2D rb = null;
    private Collision col = null;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collision>();
    }

    private void Update()
    {
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

        rb.velocity = new Vector2(walkSpeed, rb.velocity.y);
    }

    private void Flip()
    {
        mustPatrol = false;

        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        walkSpeed *= -1;

        mustPatrol = true;
    }
}
