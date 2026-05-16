using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    public float speed = 20;
    private Vector3 startpos;
    private float repeatSize;
    private PlayerController myPlayerController;
    private SpawnObstacles manager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startpos = transform.position;
        repeatSize = GetComponent<BoxCollider>().size.x / 2;
        myPlayerController = GameObject.Find("Player").GetComponent<PlayerController>();
        manager = GameObject.Find("Spawn Manager").GetComponent<SpawnObstacles>();
    }

    // Update is called once per frame
    void Update()
    {
        BackgroundMovement();
        
    }
    void BackgroundMovement()
    {
        if (myPlayerController.gameOver == false && manager.gameIsActive == true)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);

            if (transform.position.x < startpos.x - repeatSize)
            {
                transform.position = startpos;
            }
        }
    }
}
