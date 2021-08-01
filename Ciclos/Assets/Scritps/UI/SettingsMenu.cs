using UnityEngine;
using UnityEngine.UI;
using TMPro;

// https://youtu.be/EA-tBcTxE8M
public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Slider[] volumeSliders;
    [SerializeField] private Toggle[] resolutionToggles;
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private Toggle lightConfigToggle;
    [SerializeField] private TMP_Dropdown qualityDropdown;

    [SerializeField] private int[] screenWidths;

    private int activeScreenResIndex;

    private void Start()
    {
        activeScreenResIndex = PlayerPrefs.GetInt("screen res index", activeScreenResIndex);

        bool isFullscren = PlayerPrefs.GetInt("fullscreen") == 1;
        bool isLightsDisabled = PlayerPrefs.GetInt("lights disabled") == 1;

        volumeSliders[0].value = AudioManager._I.MasterVolumePercent;
        volumeSliders[1].value = AudioManager._I.SFXVolumePercent;
        volumeSliders[2].value = AudioManager._I.MusicVolumePercent;

        for (int i = 0; i < resolutionToggles.Length; i++) {
            resolutionToggles[i].isOn = i == activeScreenResIndex;
        }

        SetFullscreen(isFullscren);
        SetLightning(isLightsDisabled);

        if (!GraphicsManager.IsLightningEnabled) {
            SetLightning(true);
            lightConfigToggle.isOn = true;
        }

        if (Screen.fullScreen) {
            SetFullscreen(true);
            fullscreenToggle.isOn = true;
        }
    }

    private void Update()
    {
        qualityDropdown.value = QualitySettings.GetQualityLevel();
    }

    public void SetScreenResolution(int index)
    {
        if (resolutionToggles[index].isOn) {
            float aspectRatio = 16f / 9f;

            activeScreenResIndex = index;

            Screen.SetResolution(screenWidths[index], (int) ( screenWidths[index] / aspectRatio ), false);

            PlayerPrefs.SetInt("screen res index", activeScreenResIndex);
            PlayerPrefs.Save();
        }
    }

    public void SetFullscreen(bool isFullscreen)
    {
        for (int i = 0; i < resolutionToggles.Length; i++) {
            resolutionToggles[i].interactable = !isFullscreen;
        }

        if (isFullscreen) {
            Resolution[] allResolutions = Screen.resolutions;
            Resolution maxResolution = allResolutions[allResolutions.Length - 1];

            Screen.SetResolution(maxResolution.width, maxResolution.height, true);
        }
        else {
            SetScreenResolution(activeScreenResIndex);
        }

        PlayerPrefs.SetInt("fullscreen", isFullscreen ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void SetLightning(bool isDisabled)
    {
        GraphicsManager.IsLightningEnabled = !isDisabled;

        PlayerPrefs.SetInt("lights disabled", isDisabled ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void SetQuality(int qualityIndex) => QualitySettings.SetQualityLevel(qualityIndex);

    public void SetMasterVolume(float volume) => AudioManager._I.SetVolume(volume, AudioManager.AudioChannel.Master);

    public void SetSFXVolume(float volume) => AudioManager._I.SetVolume(volume, AudioManager.AudioChannel.SFX);

    public void SetMusicVolume(float volume) => AudioManager._I.SetVolume(volume, AudioManager.AudioChannel.Music);
}
