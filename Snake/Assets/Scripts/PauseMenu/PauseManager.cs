using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public KeyCode Unpause;
    public KeyCode MainMenu;
    public KeyCode Instructions;
    
    void Start()
    {

  
    }

    
    void Update()
    {

        if (Input.GetKey(Unpause))
        {
            Debug.Log("Game Unpaused.");
            SceneManager.UnloadScene("PauseMenu");
            SnakeBehavior.AllowMovement = true;
        }

        if (Input.GetKey(MainMenu))
        {
            Debug.Log("To the main menu.");
            SceneManager.LoadScene("MainMenu");
        }

        if (Input.GetKey(Instructions))
        {
            Debug.Log("To the instructions.");
            SceneManager.LoadScene("Instructions");
        }

    }
}
