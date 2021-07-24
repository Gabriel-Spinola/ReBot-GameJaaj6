using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform pos1;
    [SerializeField] private Transform pos2;
    [SerializeField] private Transform startPos;

    [SerializeField] private float speed;

    private Rigidbody2D rb;

    private Vector3 nextPos;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        nextPos = startPos.position;
    }

    private void FixedUpdate()
    {
        if (transform.position == pos1.position) {
            nextPos = pos2.position;
        }

        if (transform.position == pos2.position) {
            nextPos = pos1.position;
        }

        rb.
        transform.position = Vector2.MoveTowards(transform.position, nextPos, speed * Time.fixedDeltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(pos1.position, pos2.position);
    }
}
