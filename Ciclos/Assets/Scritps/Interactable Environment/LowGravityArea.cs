using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowGravityArea : MonoBehaviour
{
    [SerializeField] private bool disableBetterJump = false;
    [SerializeField] private float newGravityScale = 1f;

    private float cGravity = 0f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            Player player = other.gameObject.GetComponent<Player>();

            if (disableBetterJump)
                player.SetUseBetterJump(false);

            cGravity = player.GetRigidbody().gravityScale;
            player.GetRigidbody().gravityScale = newGravityScale;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            Player player = other.gameObject.GetComponent<Player>();

            if (disableBetterJump)
                player.SetUseBetterJump(true);
            if (cGravity != player.GetRigidbody().gravityScale)
                player.GetRigidbody().gravityScale = cGravity;
        }
    }
}
