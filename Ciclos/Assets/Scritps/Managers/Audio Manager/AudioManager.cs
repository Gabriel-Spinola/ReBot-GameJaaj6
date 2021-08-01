using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://youtu.be/C65ExBy6WPA
public class AudioManager : MonoBehaviour
{
    public static AudioManager _I = null;

    public enum AudioChannel
    {
        Master,
        SFX,
        Music
    }

    [HideInInspector] public float MasterVolumePercent { get; private set; }
    [HideInInspector] public float MusicVolumePercent { get; private set; }
    [HideInInspector] public float SFXVolumePercent { get; private set; }

    private Dictionary<string, bool> isFadedDictionary = new Dictionary<string, bool>();

    private AudioSource[] musicSources = null;
    private AudioSource SFX2DSource = null;
    private SoundLibrary library = null;

    private int activeMusicSource;

    private void Awake()
    {
        if (_I != null) {
            Destroy(gameObject);
        }
        else {
            _I = this;

            DontDestroyOnLoad(gameObject);

            library = GetComponent<SoundLibrary>();
            musicSources = new AudioSource[2];

            for (int i = 0; i < musicSources.Length; i++) {
                GameObject newMusicSource = new GameObject($"Music Source { i + 1 }");

                musicSources[i] = newMusicSource.AddComponent<AudioSource>();
                newMusicSource.transform.parent = transform;
            }

            foreach (AudioSource music in musicSources) {
                isFadedDictionary.Add(music.name, false);
            }

            GameObject newSFX2DSource = new GameObject("2D Sound Effect Source");

            SFX2DSource = newSFX2DSource.AddComponent<AudioSource>();
            newSFX2DSource.transform.parent = transform;

            MasterVolumePercent = PlayerPrefs.GetFloat("master vol", 1f);
            SFXVolumePercent = PlayerPrefs.GetFloat("sfx vol", 1f);
            MusicVolumePercent = PlayerPrefs.GetFloat("music vol", 1f);
        }
    }

    public void SetVolume(float volumePercent, AudioChannel channel)
    {
        switch (channel) {
            case AudioChannel.Master:
                MasterVolumePercent = volumePercent;
            break;

            case AudioChannel.SFX:
                SFXVolumePercent = volumePercent;
            break;

            case AudioChannel.Music:
                MusicVolumePercent = volumePercent;
            break;
        }

        for (int i = 0; i < musicSources.Length; i++) {
            if (!isFadedDictionary[musicSources[i].name]) {
                musicSources[i].volume = MusicVolumePercent * MasterVolumePercent;
            }
        }

        PlayerPrefs.SetFloat("master vol", MasterVolumePercent);
        PlayerPrefs.SetFloat("sfx vol", SFXVolumePercent);
        PlayerPrefs.SetFloat("music vol", MusicVolumePercent);
        PlayerPrefs.Save();
    }

    public void PlayMusic(AudioClip clip, float fadeDuration = 1f, float pitch = 1f)
    {
        activeMusicSource = 1 - activeMusicSource;

        musicSources[activeMusicSource].clip = clip;
        musicSources[activeMusicSource].pitch = pitch;
        musicSources[activeMusicSource].Play();

        StartCoroutine(MusicCrossfade(fadeDuration));
    }

    public void PlaySound(AudioClip clip, Vector3 pos)
    {
        if (clip != null) {
            AudioSource.PlayClipAtPoint(clip, pos, SFXVolumePercent * MasterVolumePercent);
        }
        else {
            Debug.LogWarning($"Can't Play \"{ clip.name }\" Audio Clip");
        }
    }

    public void PlaySound(string clip, Vector3 pos)
    {
        PlaySound(library.GetClipFromName(clip), pos);
    }

    public void PlaySound2D(string soundName, float pitch = 1f, int priority = 128)
    {
        SFX2DSource.pitch = pitch;
        SFX2DSource.priority = priority;
        SFX2DSource.PlayOneShot(library.GetClipFromName(soundName), SFXVolumePercent * MasterVolumePercent);
    }

    private IEnumerator MusicCrossfade(float duration)
    {
        float percent = 0f;

        while (percent < 1) {
            percent += Time.deltaTime * 1 / duration;

            musicSources[activeMusicSource].volume = Mathf.Lerp(0f, MusicVolumePercent * MasterVolumePercent, percent);
            musicSources[1 - activeMusicSource].volume = Mathf.Lerp(MusicVolumePercent * MasterVolumePercent, 0f, percent);

            isFadedDictionary[musicSources[activeMusicSource].name] = false;
            isFadedDictionary[musicSources[1 - activeMusicSource].name] = true;

            yield return null;
        }
    }
}
