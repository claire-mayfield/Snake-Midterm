using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour
{
    public KeyCode RestartGame;
    public KeyCode QuitGame;

    void Update()
    { 

        if (Input.GetKey(QuitGame))
        {
            Debug.Log("Quitting Game");
            ExitGame();
        }
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}