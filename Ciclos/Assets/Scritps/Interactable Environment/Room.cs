using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    private GameObject virtualCamera = null;

    private void Awake()
    {
        virtualCamera = transform.GetChild(0).gameObject;
        virtualCamera.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger) {
            virtualCamera.SetActive(true);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger) {
            GetComponent<Collider2D>().isTrigger = false;
            virtualCamera.SetActive(false);

            StartCoroutine(player.DisablePlayer(.7f));
            StartCoroutine(player.playerGraphics.DisableAnimation(.7f));

            RoomManager.CurrentRoom++;
        }
    }
}