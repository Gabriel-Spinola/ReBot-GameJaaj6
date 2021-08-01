using System.Collections;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [SerializeField] protected LayerMask whatIsBlocks;

    [SerializeField] private Vector2 colSize;
    [SerializeField] private Vector2 colOffset;

    [SerializeField] protected float lifeTime;

    [HideInInspector] public float damage;
    [HideInInspector] public float speed;

    [HideInInspector] public bool isJhon = false;

    protected virtual void Update()
    {
        StartCoroutine(DestroyBulletOnTimer(lifeTime));
    }

    protected void FixedUpdate()
    {
        if (Physics2D.OverlapBox((Vector2) transform.position + colOffset, colSize, 0f, whatIsBlocks)) {
            Destroy(gameObject);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isJhon) {
            other.gameObject.GetComponent<Player>().TakeDamage();
            Destroy(gameObject);
        }
    }

    protected IEnumerator DestroyBulletOnTimer(float timer)
    {
        yield return new WaitForSeconds(timer);

        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube((Vector2) transform.position + colOffset, colSize);
    }
}
