using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class GraphicsManager : MonoBehaviour
{
    public static GraphicsManager I;
    public static int QualityLevel = 1;
    public static bool IsLightningEnabled = true;

    public GameObject[] volumeQualities;

    private bool isLightReseted = false;

    private void Awake()
    {
        if (I == null) {
            I = this;
        }
        else {
            Destroy(gameObject);

            return;
        }
    }

    private void Update()
    {
        VolumeQualityLevel();

        if (IsLightningEnabled) {
            if (!isLightReseted) {
                foreach (Light2D light in FindObjectsOfType<Light2D>()) {
                    if (!light.isActiveAndEnabled) {
                        ResetLightSettings();

                        isLightReseted = true;
                    }

                    break;
                }
            }
        }
        else {
            DisableLights();

            isLightReseted = false;
        }
    }

    private void VolumeQualityLevel()
    {
        switch (QualitySettings.GetQualityLevel()) {
            case 0:
                ResetVolumeSettings();
                volumeQualities[0].SetActive(true);
            break;

            case 1:
                ResetVolumeSettings();
                volumeQualities[1].SetActive(true);
            break;

            case 2:
                ResetVolumeSettings();
                volumeQualities[2].SetActive(true);
            break;
        }
    }

    private void ResetVolumeSettings()
    {
        for (int i = 0; i < volumeQualities.Length; i++) {
            volumeQualities[i].SetActive(false);
        }
    }

    private void ResetLightSettings()
    {
        foreach (Light2D light in FindObjectsOfType<Light2D>()) {
            if (light.isActiveAndEnabled)
                continue;

            if (!light.CompareTag("Global Light")) {
                light.enabled = true;
            }
            else {
                light.intensity = .75f;
            }
        }

        foreach (ShadowCaster2D shadow in FindObjectsOfType<ShadowCaster2D>()) {
            if (shadow.isActiveAndEnabled)
                continue;

            shadow.enabled = true;
        }
    }

    private void DisableLights()
    {
        foreach (Light2D light in FindObjectsOfType<Light2D>()) {
            if (!light.CompareTag("Global Light")) {
                light.enabled = false;
            }
            else {
                light.intensity = 1f;
            }
        }

        DisableShadowCasters();
    }

    private void DisableShadowCasters()
    {
        foreach (ShadowCaster2D shadow in FindObjectsOfType<ShadowCaster2D>()) {
            shadow.enabled = false;
        }
    }
}
