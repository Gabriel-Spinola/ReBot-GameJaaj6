using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maxeica : EnemyPatrol
{
    [SerializeField] private float jumpHeight = 8f;
    [SerializeField] private float disableFlipTimer = .5f;

    private Vector2 groundCheckerInitialPos = Vector2.zero;

    private bool canFlip;

    private void Start()
    {
        groundCheckerInitialPos = groundCheck.position;
    }

    protected override void FixedUpdate()
    {
        if (mustPatrol) {
            mustTurn = !Physics2D.OverlapCircle(new Vector2(groundCheck.position.x, groundCheckerInitialPos.y), .1f, whatIsBlock);
        }
    }

    protected override void Patrol()
    {
        if (mustTurn || col.isOnWall && canFlip) {
            Flip();

            StartCoroutine(DisableFlip(disableFlipTimer));
        }

        rb.velocity = Vector2.right * walkSpeed * Time.fixedDeltaTime + Vector2.up * rb.velocity.y;

        if (col.isGrounded) {
            rb.velocity = Vector2.right * rb.velocity.x + Vector2.up * jumpHeight * Time.fixedDeltaTime;
        }
    }

    private IEnumerator DisableFlip(float time)
    {
        canFlip = false;

        yield return new WaitForSeconds(time);

        canFlip = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            collision.gameObject.GetComponent<Player>().TakeDamage();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(new Vector2(groundCheck.position.x, groundCheckerInitialPos.y), .1f);
    }
}
