using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        if (PauseMenu.isGamePaused) {
            PauseMenu.isGamePaused = false;
        }
    }

    public void Quit()
    {
        Debug.Log("Quit");

        Application.Quit();
    }
}
