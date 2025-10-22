using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionsManager : MonoBehaviour
{
    public KeyCode StartGame;

    void Update()
    {
        if (Input.GetKey(StartGame))
        {
            SceneManager.LoadScene("Level01");
        }

    }
}