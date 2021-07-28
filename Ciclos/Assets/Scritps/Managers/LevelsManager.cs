using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsManager
{
    public static int CurrentLevel = 1;

    public static void GoToNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public static void GoToLevel(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public static void GoToLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
