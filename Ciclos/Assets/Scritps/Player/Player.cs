using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collision))]
public class Player : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InputManager InputManager = null;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float friction = .2f;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2;

    private Rigidbody2D rb = null;
    private Collision col = null;

    private bool canMove = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collision>();
    }

    private void Update()
    {
        BetterJump();
        Movement();

        if (InputManager.keyJump && col.isGrounded) {
            Jump();
        }
    }

    private void Movement()
    {
        if (InputManager.xAxis != 0) {
            rb.velocity = Vector2.right * InputManager.xAxis * moveSpeed + Vector2.up * rb.velocity.y;
        }
        else {
            rb.velocity = Vector2.right * Mathf.Lerp(rb.velocity.x, 0f, friction) + Vector2.up * rb.velocity.y;
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void Jump(Vector2 jumpDir)
    {
        rb.velocity = jumpDir;
    }

    /// <summary>
    /// if falling, add fallMultiplier
    /// if jumping and not holding spacebar, increase gravity to peform a small jump
    /// if jumping and holding spacebar, perform a full jump
    /// </summary>
    private void BetterJump()
    {
        if (rb.velocity.y < 0) {
            rb.velocity += Vector2.up * Physics.gravity.y * ( fallMultiplier - 1 ) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !InputManager.keyJumpHold) {
            rb.velocity += Vector2.up * Physics.gravity.y * ( lowJumpMultiplier - 1 ) * Time.deltaTime;
        }
    }

}
