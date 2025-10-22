using UnityEngine;

public class AppleBehavior : MonoBehaviour
{
    public KeyCode Restart;


    // Random position will be the position we want to place the object
    Vector2 randomPosition;
	
    public float xRange = 3.7f;
    // xRange the range in the x axis that the object can be placed
	
    public float yRange = 3.7f;
    // yRange the range in the y axis that the object can be placed

    void AppleRandomLocation()
        {
            // xPosition and yPosition are set to random values with the ranges
            float xPosition = Random.Range(0 - xRange, 0 + xRange);
            float yPosition = Random.Range(0 - yRange, 0 + yRange);

            // randomPosition is then given values xPosition and yPosition, making it a random vector
            randomPosition = new Vector2(xPosition, yPosition);


            // randomPosition now describes a random position for our object, so it is then moved to it.
            transform.position = randomPosition;
            // now the object has been moved, completeting the process of placing it in a random position
        }


    void Start()
    {
        AppleRandomLocation();
    }

    void Update()
    {
        if (Input.GetKey(Restart))
        {
            Debug.Log("Moving Apple to random location");
            AppleRandomLocation();
        }
    }
	
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Snake"))
            {
				Debug.Log("Apple Eaten");
                Manager.Instance.ScorePoint();
                Debug.Log("Moving Apple to random location");
                AppleRandomLocation();
                Debug.Log("Spawning new part of the snake.");

                // Move evil apples to new random location
                EvilAppleBehavior.Instance.EvilAppleRandomLocation();

        }

    }

    
}
