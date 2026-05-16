using UnityEngine;

public class PowerupMovement : MonoBehaviour
{
    private float speed = 10;
    private float leftBoundary = -13;

    // FIX: Added references to the Player and the Manager so the gems know when to freeze!
    private PlayerController myPCS;
    private SpawnObstacles manager;

    void Start()
    {
        // FIX: Find both scripts the moment the gem spawns
        myPCS = GameObject.Find("Player").GetComponent<PlayerController>();
        manager = GameObject.Find("Spawn Manager").GetComponent<SpawnObstacles>();
    }

    void Update()
    {
        // FIX: Wrap the movement and the destroy logic in the exact same if statement!
        // Now gems will wait patiently on the menu, and freeze if the player dies.
        if (myPCS.gameOver == false && manager.gameIsActive == true)
        {
            // Move left
            transform.Translate(Vector3.left * Time.deltaTime * speed, Space.World);

            // If it goes off screen, destroy it
            if (transform.position.x < leftBoundary)
            {
                Destroy(gameObject);
            }
        }
    }
}
