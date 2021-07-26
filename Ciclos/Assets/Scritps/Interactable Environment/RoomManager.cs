using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static int CurrentRoom = 1;

    private Player player = null;

    private Vector3 spawnPosition = Vector3.zero;

    private void Update()
    {
        spawnPosition = GameObject.Find($"Spawner{ CurrentRoom }").transform.position;

        player = FindObjectOfType<Player>();

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.RightAlt)) {
            Respawn();
        }
#endif
    }

    public void Respawn()
    {
        player.transform.position = spawnPosition;
    }
}