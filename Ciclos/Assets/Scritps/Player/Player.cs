using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collision))]
public class Player : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InputManager InputManager = null;
    [SerializeField] private LayerMask whatIsSpikes = 0;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float friction = .2f;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2;

    [Header("Walljump")]
    [SerializeField] private float wallJumpForce = 10f;

    private Rigidbody2D rb = null;
    private Collision col = null;

    private bool canMove = true;
    private bool wallJumped = false;
    private bool useBetterJump = true;
    private bool isInterpolationDisabled = false;
    private bool canChangeInterpolation = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collision>();
    }

    private void Update()
    {
        BetterJump();
        Movement();

        if (col.isGrounded) {
            if (isInterpolationDisabled && canChangeInterpolation) {
                rb.interpolation = RigidbodyInterpolation2D.Interpolate;
                rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            }

            wallJumped = false;
            useBetterJump = true;
        }

        if (InputManager.keyJump) {
            if (col.isGrounded) {
                Jump(jumpForce);
            }
            else if (col.isOnWall) {
                WallJump();
            }
        }
    }

    private void Movement()
    {
        if (!canMove)
            return;

        if (InputManager.xAxis != 0) {
            rb.velocity = Vector2.right * InputManager.xAxis * moveSpeed + Vector2.up * rb.velocity.y;
        }
        else {
            if (!wallJumped) {
                rb.velocity = Vector2.right * Mathf.Lerp(rb.velocity.x, 0f, friction) + Vector2.up * rb.velocity.y;
            }
            else {
                rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0f, friction - .17f), rb.velocity.y);
            }
        }
    }

    public void Jump(float jumpForce)
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    public void Jump(Vector2 jumpDir)
    {
        rb.velocity = jumpDir;
    }

    private void WallJump()
    {
        StartCoroutine(DisableMovement(.2f));

        int wallJumpDir = col.isOnRightWall ? -1 : col.isOnLeftWall ? 1 : 0;
        wallJumped = true;

        Jump(Vector2.right * wallJumpForce / 1.5f * wallJumpDir + Vector2.up * wallJumpForce / 1.2f);
    }

    /// <summary>
    /// if falling, add fallMultiplier
    /// if jumping and not holding spacebar or walljumping, increase gravity to peform a small jump
    /// if jumping and holding spacebar, perform a full jump
    /// </summary>
    private void BetterJump()
    {
        if (rb.velocity.y < 0) {
            rb.velocity += Vector2.up * Physics.gravity.y * ( fallMultiplier - 1 ) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !InputManager.keyJumpHold || wallJumped) {
            rb.velocity += Vector2.up * Physics.gravity.y * ( lowJumpMultiplier - 1 ) * Time.deltaTime;
        }
    }

    private IEnumerator DisableMovement(float time)
    {
        canMove = false;

        yield return new WaitForSeconds(time);

        canMove = true;
    }

    private void Die()
    {
        Debug.Log("DIED");
    }

    public void TakeDamage() => Die();

    public void SetUseBetterJump(bool val) => useBetterJump = val;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == whatIsSpikes) {
            TakeDamage();
        }

        if (collision.gameObject.CompareTag("Platform")) {
            transform.parent = collision.gameObject.transform;

            rb.interpolation = RigidbodyInterpolation2D.None;
            rb.collisionDetectionMode = CollisionDetectionMode2D.Discrete;

            isInterpolationDisabled = true;
            canChangeInterpolation = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform")) {
            transform.parent = null;

            canChangeInterpolation = true;
        }
    }
}
