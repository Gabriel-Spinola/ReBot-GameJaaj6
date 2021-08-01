using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform pos1;
    [SerializeField] private Transform pos2;
    [SerializeField] private Transform startPos;

    [SerializeField] private float speed;
    [SerializeField] private float delayToMove;

    private Vector3 nextPos;

    private bool canMove = true;

    private void Start()
    {
        nextPos = startPos.position;
    }

    private void Update()
    {
        if (!canMove)
            return;

        if (transform.position == pos1.position) {
            nextPos = pos2.position;

            StartCoroutine(WaitToMove(delayToMove));
        }

        if (transform.position == pos2.position) {
            nextPos = pos1.position;

            StartCoroutine(WaitToMove(delayToMove));
        }

        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.fixedDeltaTime);
    }

    private IEnumerator WaitToMove(float time)
    {
        canMove = false;

        yield return new WaitForSeconds(time);

        canMove = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(pos1.position, pos2.position);
    }
}
