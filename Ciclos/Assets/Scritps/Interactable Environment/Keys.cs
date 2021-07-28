using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keys : MonoBehaviour
{
    [SerializeField] private GameObject holdDoor;

    public void OpenDoor()
    {
        Destroy(holdDoor);
        Destroy(gameObject);
    }
}
