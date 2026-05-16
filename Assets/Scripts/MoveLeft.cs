using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float moveSpeed = 10;
    public float rollSpeed = 60;
    private float leftBoundary = -13;
    private float bottomBoundary = -3f;
    private PlayerController myPCS;

    // FIX: Added the manager variable
    private SpawnObstacles manager;

    void Start()
    {
        myPCS = GameObject.Find("Player").GetComponent<PlayerController>();

        // FIX: Find the Spawn Manager
        manager = GameObject.Find("Spawn Manager").GetComponent<SpawnObstacles>();
    }

    void Update()
    {
        // FIX: Added the manager.gameIsActive check to the if statement!
        if (myPCS.gameOver == false && manager.gameIsActive == true)
        {
            transform.Translate(Vector3.left * Time.deltaTime * moveSpeed, Space.World);

            float rotation = -moveSpeed * rollSpeed * Time.deltaTime;
            transform.Rotate(Vector3.down, rotation);

            if (transform.position.x < leftBoundary && CompareTag("Obstacle"))
            {
                Destroy(gameObject);
            }
            if (transform.position.y < bottomBoundary && CompareTag("Obstacle"))
            {
                Destroy(gameObject);
            }
        }
    }
}