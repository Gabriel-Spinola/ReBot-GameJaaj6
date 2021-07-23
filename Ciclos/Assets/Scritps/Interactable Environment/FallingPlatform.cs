using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FallingPlatform : MonoBehaviour
{
    [SerializeField] private float waitToFall = 1f;
    [SerializeField] private float respawnTime = 2f;

    private Rigidbody2D rb = null;

    private Vector2 initialPos = Vector2.zero;

    private bool falled = false;

    private void Start()
    {
        initialPos = transform.position;

        rb = GetComponent<Rigidbody2D>();
    }

    private IEnumerator Fall(float time)
    {
        yield return new WaitForSeconds(time);

        rb.isKinematic = false;
        GetComponent<Collider2D>().isTrigger = true;

        Debug.Log("Falling");

        falled = true;

        StartCoroutine(Respawn(respawnTime));
    }

    private IEnumerator Respawn(float time)
    {
        yield return new WaitForSeconds(time);

        rb.isKinematic = true;
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        transform.position = initialPos;
        falled = false;

        Debug.Log("Respawned");

        GetComponent<Collider2D>().isTrigger = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !falled) {
            StartCoroutine(Fall(waitToFall));

            Debug.Log("Enter Collision");
        }
    }
}
