using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static int CurrentLevel = 1;

    [SerializeField] private Animator anim = null;

    [SerializeField] private float transitionTime = 1f;

    private Player player = null;

    private void Awake()
    {
        if (GameObject.Find("[Player]")) {
            player = GameObject.Find("[Player]").GetComponent<Player>();
        }
    }

    public void GoToNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void GoToLevel(int sceneIndex)
    {
        StartCoroutine(LoadLevel(sceneIndex));
    }

    public void GoToLevel(string sceneName)
    {
        StartCoroutine(LoadLevel(sceneName));
    }

    public IEnumerator LoadLevel(int levelIndex)
    {
        if (player != null)
            StartCoroutine(player.DisablePlayer(transitionTime));

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }

    public IEnumerator LoadLevel(string levelName)
    {
        if (player != null)
            StartCoroutine(player.DisablePlayer(transitionTime));

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelName);
    }
}
