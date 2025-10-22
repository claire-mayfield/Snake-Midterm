using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class HomeManager : MonoBehaviour
{
    public KeyCode StartGame;
    public KeyCode Instructions;
    public KeyCode QuitGame;

    [SerializeField] private AudioResource _uiSelect;

    private Rigidbody2D _rb;
    private AudioSource _source;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _source = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKey(StartGame))
        {
            _source.resource = _uiSelect;
            _source.Play();
            SceneManager.LoadScene("Level01");
        }

        if (Input.GetKey(Instructions))
        {
            _source.resource = _uiSelect;
            _source.Play();
            SceneManager.LoadScene("Instructions");
        }

        if (Input.GetKey(QuitGame))
        {
            Debug.Log("Quitting game");
            Manager.ExitGame();
        }
    }
}