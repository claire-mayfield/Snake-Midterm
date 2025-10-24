using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.Audio;


public class Manager : MonoBehaviour
{
    public static Manager Instance;
	
			
    [SerializeField] private TMP_Text _winText;
    [SerializeField] private TMP_Text _gameOver;

    [SerializeField] private int _scoreToVictory = 10;


    [SerializeField] private float _speedIncreaseIntensity = 0.25f;
	
	public KeyCode Restart;
    public KeyCode QuitGame;



    [SerializeField] private GameObject _goldenApplePrefab;
    [SerializeField] private GameObject _parent;


    [SerializeField] private AudioResource _gameOverSound;
    [SerializeField] private AudioResource _winGame;
    private Rigidbody2D _rb;
    private AudioSource _source;


    public bool Level01Completed;

    public KeyCode LoadCredits;



    private void Awake()
    {
        // Instance is null when no Manager has been initialized
        if (Instance == null)
        {
            Instance = this;
            Debug.Log("New instance initialized...");

            DontDestroyOnLoad(gameObject);
        }

        // We evaluate this portion when trying to initialize a new instance
        // when one already exists
        else if (Instance != this)
        {
            Destroy(gameObject);
            Debug.Log("Duplicate instance found and deleted...");
        }
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _source = GetComponent<AudioSource>();

        _winText.text = " ";
        _gameOver.text = " ";
        Score = 0;

        Level01Completed = false;
    }
    
    private int _score = 0;
    
    public int Score
    {
        get
        {
            return _score;
        }

        set
        {
            _score = value;
            _scoreUI.text = "Apples: " + Score.ToString() + "/" + _scoreToVictory;
        }
    }

    [SerializeField] private TMP_Text _scoreUI;

    public void ScorePoint()
    {
        Score++;

        // INCREASE snake speed with each apple intake
        SnakeBehavior.Speed = SnakeBehavior.Speed + _speedIncreaseIntensity;
        Debug.Log("Snake Speed Increased! Speed: " + SnakeBehavior.Speed);
    }



    public void SubtractPoint()
    {
        Score = Score - 1;

        // DECREASE snake speed with each apple intake
        SnakeBehavior.Speed = SnakeBehavior.Speed - _speedIncreaseIntensity;
        Debug.Log("Snake Speed Increased! Speed: " + SnakeBehavior.Speed);
    }





    void SpawnGoldenApple()
    {
        GameObject newGoldenApple = Instantiate(
        _goldenApplePrefab,
        Vector3.zero,
        Quaternion.identity,
        _parent.transform
        );
    }

    void Update()
    {


        //if (Score == 5)
        //{
        //SpawnGoldenApple();
        //}

        // Restart Game
        if (Input.GetKey(Restart))
        {
			Debug.Log("Removing victory text");
            _winText.text = " ";
            _gameOver.text = " ";
        }

        // Lose the game with a negative score
        if (Score <= -1)
        {
            _gameOver.text = "Game Over! Press R to Restart.";
            SnakeBehavior.AllowMovement = false;
            SnakeBehavior.GameisPlaying = false;

            _source.resource = _gameOverSound;
            _source.Play();
        }

        // Allow credits to be loaded when victory score is met 
        if (Score >= _scoreToVictory)
        {
            _source.resource = _winGame;
            _source.Play();
            Debug.Log("Game won!");
            Level01Completed = true;
            SnakeBehavior.AllowMovement = false;
            SnakeBehavior.GameisPlaying = false;

            //if (SnakeBehavior.Lives == 3)
            //{
                //_winText.text = "You Win! Rank: A. Press C for credits. Or, press R to play again.";
            //}

            //if (SnakeBehavior.Lives == 2)
            //{
               // _winText.text = "You Win! Rank: B. Press C for credits. Or, press R to play again.";
            //}

            //if (SnakeBehavior.Lives == 1)
            //{
            ///    _winText.text = "You Win! Rank: C. Press C for credits. Or, press R to play again.";
            //}

           // if (SnakeBehavior.Lives == 0)
           // {
             //   _winText.text = "You Win! Rank: D. Press C for credits. Or, press R to play again.";
            //}

        }

        // Quit the game by pressing the Q key
        if (Input.GetKey(QuitGame))
        {
            Debug.Log("Quitting Game");
            ExitGame();
        }

        if (Level01Completed == true)
        {
            if (Input.GetKey(LoadCredits))
            {
                SceneManager.LoadScene("Credits");
            }
        }

    }

    public static void ExitGame()
    {
        Application.Quit();
    }
    

    

    

}