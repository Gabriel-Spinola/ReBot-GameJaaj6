using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static double CurrentRoom = 1;
    public static bool RespawnMenu = false;
    public static Transform PastTemporaryObjects;
    public static Transform PresentTemporaryObjects;

    [HideInInspector] public Player player = null;

    private Vector3 spawnPosition = Vector3.zero;

    private void Awake()
    {
        if (GameObject.Find("[PastTemporaryObjects]"))
            PastTemporaryObjects = GameObject.Find("[PastTemporaryObjects]").transform;

        if (GameObject.Find("[PresentTemporaryObjects]"))
            PresentTemporaryObjects = GameObject.Find("[PresentTemporaryObjects]").transform;
    }

    private void Update()
    {
        if (GameObject.Find($"Spawner{ CurrentRoom }") != null)
            spawnPosition = GameObject.Find($"Spawner{ CurrentRoom }").transform.position;

        player = FindObjectOfType<Player>();

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.RightAlt)) {
            Respawn();
        }
#endif

        if (RespawnMenu) {
            Respawn();

            RespawnMenu = false;
        }
    }

    public void Respawn()
    {
        player.transform.position = spawnPosition;
        StartCoroutine(player.DisableMovement(.35f));
    }
}