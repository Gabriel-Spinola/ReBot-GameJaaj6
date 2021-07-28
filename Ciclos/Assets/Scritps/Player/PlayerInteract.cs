using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Key")) {
            other.gameObject.GetComponent<Keys>().OpenDoor();
        }
    }
}
