using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class SnakeBehavior : MonoBehaviour
{
	// Prefab that will be the body segments of the snake 
	[SerializeField] private GameObject _snakeBodyPrefab;

	public static float Speed = 2.0f;

	[SerializeField] private TMP_Text _gameOverText;
	[SerializeField] private TMP_Text _pauseText;
	[SerializeField] private TMP_Text _livesText;

	public KeyCode RightDirection;
	public KeyCode LeftDirection;

	public KeyCode UpDirection;
	public KeyCode DownDirection;

	public KeyCode Restart;
	public KeyCode Pause;
	public KeyCode Unpause;

	public static bool AllowMovement = true;
	public static bool GameisPlaying = true;

	[SerializeField] private AudioResource _eatApple;
	[SerializeField] private AudioResource _eatEvilApple;
	[SerializeField] private AudioResource _hitWood;
	[SerializeField] private AudioResource _gameOverSound;

	public Vector3[] OldPosition;
	public int OldPositionCount;

	private Rigidbody2D _rb;
	private AudioSource _source;



	// Snake Body Array
	public Transform[] SnakeBody;

	// Keep track of the size of the snake body
	public int SnakeBodySize;


	private int _lives = 3;
	public int Lives
	{
		get
		{
			return _lives;
		}

		set
		{
			_lives = value;
			_livesText.text = "Lives: " + Lives.ToString();
		}
	}




	void Start()
	{
		_rb = GetComponent<Rigidbody2D>();
		_source = GetComponent<AudioSource>();


		// Make text invisible
		_gameOverText.text = " ";
		_pauseText.text = " ";


		AllowMovement = true;
		GameisPlaying = true;

		// Set speed and number of lives
		Speed = 2.0f;
		Lives = 3;


		SnakeBodySize = 0;

		SnakeBody = new Transform[100];

		for (int i = 0; i < 100; i++)
		{
			Debug.Log("Spawning Snake Body segment...");
			GameObject newSnakeBodySegment = Instantiate(
												_snakeBodyPrefab,
												Vector3.zero,
												Quaternion.identity
														);

			newSnakeBodySegment.SetActive(false);

			SnakeBody[i] = newSnakeBodySegment.transform;
		}

		OldPositionCount = 0;
		OldPosition = new Vector3[1000];

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

				OldPosition[OldPositionCount] = transform.position;
				OldPositionCount++;

			}

			else if (Input.GetKey(LeftDirection))
			{
				HorizontalMovement -= Speed;
				// Debug.Log("Current position is " + transform.position);

				OldPosition[OldPositionCount] = transform.position;
				OldPositionCount++;

			}

			else if (Input.GetKey(UpDirection))
			{
				VerticalMovement += Speed;
				// Debug.Log("Current position is " + transform.position);

				OldPosition[OldPositionCount] = transform.position;
				OldPositionCount++;

			}

			else if (Input.GetKey(DownDirection))
			{
				VerticalMovement -= Speed;
				// Debug.Log("Current position is " + transform.position);

				OldPosition[OldPositionCount] = transform.position;
				OldPositionCount++;

			}

			transform.position += new Vector3(HorizontalMovement * Time.deltaTime, 0.0f, 0.0f);
			transform.position += new Vector3(0.0f, VerticalMovement * Time.deltaTime, 0.0f);


			if (OldPositionCount >= 20)

			{
				Debug.Log("The head has moved 100 times. We're gonna move the body.");

				OldPositionCount = 0;
				Vector3 CurrentPosition = OldPosition[0];


				if (SnakeBodySize >= 1)
				{
					for (int i = 0; i < 100; i++)
					{
						Vector3 temporary = SnakeBody[i].position;
						SnakeBody[i].position = CurrentPosition;
						CurrentPosition = temporary;

						// GameObject segment = SnakeBody[0].gameObject;
						// segment.SetActive(false);
						// segment.transform.position = OldPosition[0];


						// Debug.Log("We are going to move the body segment now");
						// SnakeBody[0].position = OldPosition;

					}

				}

			}


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
			// Play sound effect
			_source.resource = _eatApple;
			_source.Play();

			//Add body segment to snake
			AddBodySegment();


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
		SnakeBodySize = 0;

		for (int i = 0; i < 100; i++)
		{
			SnakeBody[i].gameObject.SetActive(false);
		}

	}





	void SpawnSnakeBodySegment()
	{
		Debug.Log("Spawning Snake Body segment...");
		GameObject newSnakeBodySegment = Instantiate(
		_snakeBodyPrefab,
		Vector3.zero,
		Quaternion.identity
		);
	}


	void AddBodySegment()
	{
		Vector3 headPosition = transform.position;
		Debug.Log("Adding a body segment " + SnakeBodySize);
		Transform nextSegment = SnakeBody[SnakeBodySize];
		nextSegment.position = headPosition;
		nextSegment.gameObject.SetActive(true);
		SnakeBodySize++;
	}



}