using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeGauntlet : MonoBehaviour
{
    [SerializeField] private GameObject[] present;
    [SerializeField] private GameObject[] past;

    [SerializeField] private InputManager inputManager;

    [SerializeField] private bool isOnPast = false;
    public static bool IsOnPast = false;

    public static bool usedGauntlet = false;

    private void Update()
    {
        IsOnPast = isOnPast;

        usedGauntlet = inputManager.keyGauntlet;
    }

    public void UpdateGauntlet()
    {
        if (isOnPast) {
            foreach (GameObject @object in present) {
                if (@object is null)
                    break;

                @object.SetActive(true);
            }

            foreach (GameObject @object in past) {
                if (@object is null)
                    break;

                @object.SetActive(false);
            }

            isOnPast = false;
        }
        else {
            foreach (GameObject @object in present) {
                if (@object is null)
                    break;

                @object.SetActive(false);
            }

            foreach (GameObject @object in past) {
                if (@object is null)
                    break;

                @object.SetActive(true);
            }

            isOnPast = true;
        }
    }
}
