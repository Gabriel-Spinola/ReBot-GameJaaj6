using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private bool lockAfterExit;
    [SerializeField] private bool verticalTransition;
    [SerializeField] private double roomID;
    
    private GameObject virtualCamera = null;
    private RoomManager roomManager = null;

    private void Awake()
    {
        virtualCamera = transform.GetChild(0).gameObject;
        virtualCamera.SetActive(false);

        transform.GetChild(1).name = $"{ transform.GetChild(1).name }{ roomID }";

        roomManager = FindObjectOfType<RoomManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger) {
            virtualCamera.SetActive(true);

            RoomManager.CurrentRoom = roomID;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger) {
            if (lockAfterExit)
                GetComponent<Collider2D>().isTrigger = false;
            if (verticalTransition)
                roomManager.player.Jump(25f);

            virtualCamera.SetActive(false);

            if (roomManager.player != null && gameObject.activeInHierarchy) {
                StartCoroutine(roomManager.player.DisablePlayer(.4f));
                StartCoroutine(roomManager.player.playerGraphics.DisableAnimation(.4f));
            }
        }
    }
}