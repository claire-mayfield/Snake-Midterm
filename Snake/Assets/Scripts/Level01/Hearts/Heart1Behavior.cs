using UnityEngine;

public class Heart1Behavior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (SnakeBehavior.Lives == 0)
        {
            Destroy(gameObject);
        }
    }
}
