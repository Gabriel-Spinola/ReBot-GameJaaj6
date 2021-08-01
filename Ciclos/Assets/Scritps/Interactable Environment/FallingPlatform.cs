using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class FallingPlatform : MonoBehaviour
{
    private enum Time {
        Present,
        Past
    }

    [SerializeField] private Time time;

    [SerializeField] private float waitToFall = 1f;
    [SerializeField] private float respawnTime = 2f;

    private Rigidbody2D rb = null;
    private Collider2D collider = null;
    private SpriteRenderer spriteRenderer = null;

    private Vector2 initialPos = Vector2.zero;

    private bool falled = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        initialPos = transform.position;    
    }

    private void Update()
    {
        switch (time) {
            case Time.Present:
                if (TimeGauntlet.IsOnPast) {
                    collider.enabled = false;
                    spriteRenderer.enabled = false;
                }
                else {
                    collider.enabled = true;
                    spriteRenderer.enabled = true;
                }
            break;

            case Time.Past:
                if (TimeGauntlet.IsOnPast) {
                    collider.enabled = true;
                    spriteRenderer.enabled = true;
                }
                else {
                    collider.enabled = false;
                    spriteRenderer.enabled = false;
                }
            break;
        }
    }

    private IEnumerator Fall(float time)
    {
        yield return new WaitForSeconds(time);

        GetComponent<TargetJoint2D>().enabled = false;
        falled = true;

        StartCoroutine(Respawn(respawnTime));

        yield return new WaitForSeconds(.5f);

        GetComponent<Collider2D>().isTrigger = true;
    }

    private IEnumerator Respawn(float time)
    {
        yield return new WaitForSeconds(time);

        GetComponent<TargetJoint2D>().enabled = true;
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        transform.position = initialPos;
        falled = false;

        GetComponent<Collider2D>().isTrigger = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !falled) {
            StartCoroutine(Fall(waitToFall));
        }
    }
}
