using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Level02SnakeBehavior : MonoBehaviour
{
    public static float Level02Speed = 2.0f;
	
	[SerializeField] private TMP_Text _gameOverText;
	[SerializeField] private TMP_Text _pauseText;

    public KeyCode RightDirection;
    public KeyCode LeftDirection;

    public KeyCode UpDirection;
    public KeyCode DownDirection;
	
	public KeyCode Restart;
	public KeyCode Pause;
	public KeyCode Unpause;

	public static bool AllowMovement = true;
	public static bool GameisPlaying = true;

	public Vector3 OldPosition;

    void Start()
    {
        _gameOverText.text = " ";
		_pauseText.text = " ";
		AllowMovement = true;
		GameisPlaying = true;
		Level02Speed = 2.0f;
	}

    void Update()
    {
        float HorizontalMovement = 0.0f;
        float VerticalMovement = 0.0f;

		if (AllowMovement == true)
		{
				if (Input.GetKey(RightDirection))
				{
					HorizontalMovement += Level02Speed;
					// Debug.Log("Current position is " + transform.position);
					OldPosition = transform.position;
				}

				else if (Input.GetKey(LeftDirection))
				{
					HorizontalMovement -= Level02Speed;
					// Debug.Log("Current position is " + transform.position);
					OldPosition = transform.position;
				}

				else if (Input.GetKey(UpDirection))
				{
					VerticalMovement += Level02Speed;
					// Debug.Log("Current position is " + transform.position);
					OldPosition = transform.position;
				}

				else if (Input.GetKey(DownDirection))
				{
					VerticalMovement -= Level02Speed;
					// Debug.Log("Current position is " + transform.position);
					OldPosition = transform.position;
				}

				transform.position += new Vector3(HorizontalMovement * Time.deltaTime, 0.0f, 0.0f);
				transform.position += new Vector3(0.0f, VerticalMovement * Time.deltaTime, 0.0f);

		}
		

        if (Input.GetKey(Restart))
        {
			Debug.Log("Resetting snake position and score");
            ResetSnake();
        }

		if (GameisPlaying == true)

        {
			if (Input.GetKey(Pause))
			{
				Debug.Log("Game Paused.");
				SceneManager.LoadScene("PauseMenu", LoadSceneMode.Additive);
				AllowMovement = false;
			}

			if (Input.GetKey(Unpause))
			{
				Debug.Log("Game Unpaused.");
				AllowMovement = true;
				_pauseText.text = " ";
			}
		}

	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
			Debug.Log("Wall collision - game over!");
			AllowMovement = false;
			_gameOverText.text = "Game Over! Press R to Restart";
        }

    }

    void ResetSnake()
        {
			Level02Manager.Instance.Score = 0;
			transform.position = Vector3.zero;
			_gameOverText.text = " ";
			AllowMovement = true;
			GameisPlaying = true;
			Level02Speed = 2.0f;
        }

}