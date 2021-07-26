using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private bool canComeBack;
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
            virtualCamera.SetActive(false);

            StartCoroutine(roomManager.player.DisablePlayer(.7f));
            StartCoroutine(roomManager.player.playerGraphics.DisableAnimation(.7f));
        }
    }
}