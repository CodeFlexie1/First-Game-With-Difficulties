using Unity.VisualScripting;
using UnityEngine;

public class SpawnPowerUp : MonoBehaviour
{
    public GameObject powerupItem;
    private Vector3 spawnPos = new Vector3(24, 0.7f, -2);
    private float startDelay = 12.5f;
    private float spawnRate = 11.5f;
    private PlayerController myPCT;
    private SpawnObstacles manager;

    void Start()
    {
        myPCT = GameObject.Find("Player").GetComponent<PlayerController>();
        manager = GameObject.Find("Spawn Manager").GetComponent<SpawnObstacles>();
    }

    public void SpawnPowerup()
    {
        if (myPCT.gameOver == false && myPCT.beginGame == true && manager.gameIsActive == true)
        {
            Instantiate(powerupItem, spawnPos, powerupItem.transform.rotation);
        }
    }

    public void StartPowerup()
    {
        InvokeRepeating("SpawnPowerup", startDelay, spawnRate);
    }
}
