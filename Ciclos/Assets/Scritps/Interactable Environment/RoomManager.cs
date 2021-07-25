using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    private GameObject virtualCamera;

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
            virtualCamera.SetActive(false);
        }
    }
}
