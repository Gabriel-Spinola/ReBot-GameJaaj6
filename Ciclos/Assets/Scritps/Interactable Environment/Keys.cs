using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keys : MonoBehaviour
{
    [SerializeField] private Door door;

    public void OpenDoor()
    {
        Destroy(gameObject);

        door.Open();
    }
}
