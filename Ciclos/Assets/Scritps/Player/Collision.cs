using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask whatIsBlocks;

    [Header("Horizontal Collision")]
    [SerializeField] private Vector2 rightColOffset;
    [SerializeField] private Vector2 leftColOffset;

    [Range(0f, 1f)]
    [SerializeField] private float horizontalColRadius = .5f;

    [Header("Vertical Collision")]
    [SerializeField] private Vector2 bottomColSize;
    [SerializeField] private Vector2 bottomColOffset;

    [Space]

    public bool isOnRightWall = false;
    public bool isOnLeftWall = false;
    public bool isOnWall = false;
    public bool isGroundedEarly = false;

    public float bottomColYSize;

    public int wallSide;

    [Space]

    public bool isGrounded = false;

    private void Update()
    {
        isOnRightWall = Physics2D.OverlapCircle((Vector2) transform.position + rightColOffset, horizontalColRadius, whatIsBlocks);
        isOnLeftWall = Physics2D.OverlapCircle((Vector2) transform.position + leftColOffset, horizontalColRadius, whatIsBlocks);

        isOnWall = isOnLeftWall || isOnRightWall;
        wallSide = isOnRightWall ? -1 : 1;

        isGrounded = Physics2D.OverlapBox((Vector2) transform.position + bottomColOffset, bottomColSize, 0f, whatIsBlocks);
        isGroundedEarly = Physics2D.OverlapBox((Vector2) transform.position + bottomColOffset, new Vector2(bottomColSize.x - .12f, bottomColSize.y + .2f), 0f, whatIsBlocks);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2) transform.position + rightColOffset, horizontalColRadius);
        Gizmos.DrawWireSphere((Vector2) transform.position + leftColOffset, horizontalColRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube((Vector2) transform.position + bottomColOffset, bottomColSize);
    }
}