using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isGamePaused = false;

    [SerializeField] private GameObject pauseMenuUI = null;
    [SerializeField] private LevelManager levelManager = null;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) {
            if (isGamePaused) {
                Resume();
            }
            else {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        isGamePaused = false;

        Time.timeScale = 1f;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        isGamePaused = true;

        Time.timeScale = 0f;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;

        levelManager.GoToLevel("MainMenu");
    }
    
    public void Quit()
    {
        Debug.Log("Quit");

        Application.Quit();
    }
}
