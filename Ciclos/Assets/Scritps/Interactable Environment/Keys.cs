using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keys : MonoBehaviour
{
    [SerializeField] private Door door;

    public void OpenDoor()
    {
        AudioManager._I.PlaySound2D("Pickup");
        Destroy(gameObject);

        door.Open();
    }
}
