using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
	[SerializeField] private SceneTheme[] sceneThemes;

	private string sceneName;

	private void Awake()
	{
		DontDestroyOnLoad(gameObject);

		// Shows a Unity warning, but doesn't cause any error.
		SceneManager.sceneLoaded += ((Scene scene, LoadSceneMode mode) => {
			string newSceneName = scene.name;

			if (newSceneName != sceneName) {
				sceneName = newSceneName;

				Invoke(nameof(PlayMusic), .2f);
			}
			else {
				Debug.LogWarning($"Theme: { newSceneName } not found!");
			}
		});
	}

	void PlayMusic()
	{
		AudioClip clipToPlay = null;
		float fadeDuration = 0f;
		float pitch = 0f;

		for (int i = 0; i < sceneThemes.Length; i++) {
			if (sceneName == sceneThemes[i].name) {
				clipToPlay = sceneThemes[i].theme;
				fadeDuration = sceneThemes[i].fadeDuration;
				pitch = sceneThemes[i].pitch;
			}
		}

		if (clipToPlay != null) {
			AudioManager._I.PlayMusic(clipToPlay, fadeDuration, pitch);

			Invoke(nameof(PlayMusic), clipToPlay.length);
		}
	}

	[System.Serializable]
	public class SceneTheme
	{
		public string name;
		public AudioClip theme;
		public float fadeDuration;
		public float pitch;
	}
}
