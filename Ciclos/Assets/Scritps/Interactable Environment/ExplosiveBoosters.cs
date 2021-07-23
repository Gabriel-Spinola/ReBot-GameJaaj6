using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBoosters : MonoBehaviour
{
    [SerializeField] protected ParticleSystem movingEffect = null;
    [SerializeField] protected GameObject explodeEffect = null;

    [SerializeField] private float kockbackForce = 10f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            Player player_ = collision.gameObject.GetComponent<Player>();

            player_.SetUseBetterJump(false);
            player_.Jump(kockbackForce);

            Destroy(gameObject);
        }
    }
}
