using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicsManager : MonoBehaviour
{
    public static GraphicsManager I;
    public static int currentLevel = 1;
    public static int qualityLevel = 1;

    public GameObject[] volumeQualities;

    void Awake()
    {
        if (I == null) {
            I = this;
        }
        else {
            Destroy(gameObject);

            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        VolumeQualityLevel();
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
}
