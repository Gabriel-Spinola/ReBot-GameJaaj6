using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeGauntlet : MonoBehaviour
{
    [SerializeField] private GameObject present;
    [SerializeField] private GameObject past;

    [SerializeField] private InputManager inputManager;

    private bool isOnPast = false;

    private void Update()
    {
        if (isOnPast && inputManager.keyGauntlet) {
            present.SetActive(true);
            past.SetActive(false);
        }
        else if (inputManager.keyGauntlet) {
            present.SetActive(false);
            past.SetActive(true);
        }
    }
}
