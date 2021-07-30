using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        LevelsManager.GoToLevel(LevelsManager.CurrentLevel);
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
