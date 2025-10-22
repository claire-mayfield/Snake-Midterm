using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class SnakeBehavior : MonoBehaviour
{
    public static float Speed = 2.0f;
	
	[SerializeField] private TMP_Text _gameOverText;
	[SerializeField] private TMP_Text _pauseText;

    public KeyCode RightDirection;
    public KeyCode LeftDirection;

    public KeyCode UpDirection;
    public KeyCode DownDirection;
	
	public KeyCode Restart;
	public KeyCode Pause;
	public KeyCode Unpause;


	public static int Lives = 3;

	public static bool AllowMovement = true;
	public static bool GameisPlaying = true;

	[SerializeField] private AudioResource _eatApple;
	[SerializeField] private AudioResource _eatEvilApple;
	[SerializeField] private AudioResource _hitWood;
	[SerializeField] private AudioResource _gameOverSound;

	public Vector3 OldPosition;

	private Rigidbody2D _rb;
	private AudioSource _source;

	void Start()
    {
		_rb = GetComponent<Rigidbody2D>();
		_source = GetComponent<AudioSource>();
		_gameOverText.text = " ";
		_pauseText.text = " ";
		AllowMovement = true;
		GameisPlaying = true;
		Speed = 2.0f;
		Lives = 3;
	}

    void Update()
    {
        float HorizontalMovement = 0.0f;
        float VerticalMovement = 0.0f;

		if (AllowMovement == true)
		{
				if (Input.GetKey(RightDirection))
				{
					HorizontalMovement += Speed;
					// Debug.Log("Current position is " + transform.position);
					OldPosition = transform.position;
				}

				else if (Input.GetKey(LeftDirection))
				{
					HorizontalMovement -= Speed;
					// Debug.Log("Current position is " + transform.position);
					OldPosition = transform.position;
				}

				else if (Input.GetKey(UpDirection))
				{
					VerticalMovement += Speed;
					// Debug.Log("Current position is " + transform.position);
					OldPosition = transform.position;
				}

				else if (Input.GetKey(DownDirection))
				{
					VerticalMovement -= Speed;
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
			if (Lives == 0)
			{
				_gameOverText.text = "Game Over! Press R to Restart.";
				SnakeBehavior.AllowMovement = false;
				SnakeBehavior.GameisPlaying = false;
				_source.resource = _gameOverSound;
				_source.Play();
			}

			if (Lives == 1)
			{
				Debug.Log("Down to 0 lives!");
				Lives = 0;
				_source.resource = _hitWood;
				_source.Play();
			}

			if (Lives == 2)
			{
				Debug.Log("Down to 1 life");
				Lives = 1;
				_source.resource = _hitWood;
				_source.Play();
			}

			if (Lives == 3)
			{
				Debug.Log("Down to 2 lives");
				Lives = 2;
				_source.resource = _hitWood;
				_source.Play();
			}

		}

		if (other.gameObject.CompareTag("Apple"))
		{
			_source.resource = _eatApple;
			_source.Play();

		}

		if (other.gameObject.CompareTag("EvilApple"))
		{
			_source.resource = _eatEvilApple;
			_source.Play();

		}

	}

	

    void ResetSnake()
        {
			Manager.Instance.Score = 0;
			transform.position = Vector3.zero;
			_gameOverText.text = " ";
			AllowMovement = true;
			GameisPlaying = true;
			Speed = 2.0f;
			Lives = 3;
        }

}