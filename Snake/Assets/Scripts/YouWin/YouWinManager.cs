using UnityEngine;
using UnityEngine.SceneManagement;

public class YouWinManager : MonoBehaviour
{
    public KeyCode RestartGame;
    public KeyCode Credits;

    void Update()
    {
        if (Input.GetKey(RestartGame))
        {
            Debug.Log("Enter input recieved");

            SceneManager.LoadScene("SnakeGame");
            Debug.Log("Snake Game reloaded");


            SceneManager.UnloadScene("YouWin");
        }

        if (Input.GetKey(Credits))
        {
            SceneManager.LoadScene("Credits");
            SceneManager.UnloadScene("YouWin");
        }
    }
}