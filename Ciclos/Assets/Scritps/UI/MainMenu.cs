using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager = null;

    public void Play()
    {
        levelManager.GoToLevel(LevelManager.CurrentLevel);
        RoomManager.RespawnMenu = true;

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
