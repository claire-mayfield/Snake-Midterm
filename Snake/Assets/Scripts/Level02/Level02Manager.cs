using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Level02Manager : MonoBehaviour
{
    public static Level02Manager Instance;
	
			
    [SerializeField] private TMP_Text _level02WinText;

    [SerializeField] private int _level02ScoreToVictory = 10;

    [SerializeField] private float _level02SpeedIncreaseIntensity = 0.25f;
	
	public KeyCode Restart;



    [SerializeField] private GameObject _goldenApplePrefab;
    [SerializeField] private GameObject _parent;



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
        _level02WinText.text = " ";
		Score = 0;
        SpawnGoldenApple();
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
            _scoreUI.text = "Score: " + Score.ToString();
        }
    }

    [SerializeField] private TMP_Text _scoreUI;

    public void ScorePoint()
    {
        Score++;

        // Increase snake speed with each apple intake
        Level02SnakeBehavior.Level02Speed = Level02SnakeBehavior.Level02Speed + _level02SpeedIncreaseIntensity;
        Debug.Log("Snake Speed Increased! Speed: " + Level02SnakeBehavior.Level02Speed);
    }

    public void Score5Points()
    {
        Score = Score + 5;

        // Increase snake speed with each apple intake
        Level02SnakeBehavior.Level02Speed = Level02SnakeBehavior.Level02Speed + _level02SpeedIncreaseIntensity;
        Debug.Log("Snake Speed Increased! Speed: " + Level02SnakeBehavior.Level02Speed);
    }

    void SpawnGoldenApple()
    {
        Debug.Log("Spawning Golden Apple...");
        GameObject newGoldenApple = Instantiate(
        _goldenApplePrefab,
        Vector3.zero,
        Quaternion.identity,
        _parent.transform
        );
    }

    void Update()
    {
        if (Score >= _level02ScoreToVictory)
        {
			Debug.Log("Game won!");
            SceneManager.LoadScene("YouWin");
            Level02SnakeBehavior.AllowMovement = false;
            Level02SnakeBehavior.GameisPlaying = false;

        }

        if (Input.GetKey(Restart))
        {
			Debug.Log("Removing victory text");
            _level02WinText.text = " ";
        }
		
    }

    

    

}