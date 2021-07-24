using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeGauntlet : MonoBehaviour
{
    [SerializeField] private GameObject[] present;
    [SerializeField] private GameObject[] past;

    [SerializeField] private InputManager inputManager;

    public static bool isOnPast = false;

    private void Update()
    {
        if (isOnPast && inputManager.keyGauntlet) {
            foreach (GameObject @object in present) {
                @object.SetActive(true);
            }

            foreach (GameObject @object in past) {
                @object.SetActive(false);
            }

            isOnPast = false;
        }
        else if (inputManager.keyGauntlet) {
            foreach (GameObject @object in present) {
                @object.SetActive(false);
            }

            foreach (GameObject @object in past) {
                @object.SetActive(true);
            }

            isOnPast = true;
        }
    }
}
