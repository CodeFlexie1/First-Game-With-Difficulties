using System;
using Unity.VisualScripting;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SpawnObstacles : MonoBehaviour
{
    public GameObject barrelObstacles;
    public GameObject titleScreen;
    public Button restartButton;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;

    // FIX: Set this to false! The game should NOT be active until we click a button.
    public bool gameIsActive = false;

    private Vector3 spawnPos = new Vector3(24, 0.7f, -2.5f);
    private float spawnRate = 5f;
    private float startDelay = 1.5f;
    private PlayerController myPCS;
    private int score;
    private SpawnPowerUp powerupSpawner; // FIX: Added a reference to your Powerup Spawner

    void Start()
    {
        gameIsActive = false;
        myPCS = GameObject.Find("Player").GetComponent<PlayerController>();

        powerupSpawner = GameObject.Find("Power Up Manager").GetComponent<SpawnPowerUp>();
    }

    void SpawnBarrel()
    {
      
        if (gameIsActive == true && myPCS.gameOver == false)
        {
            Instantiate(barrelObstacles, spawnPos, barrelObstacles.transform.rotation);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score:" + score;
    }

    public void StopGame()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        myPCS.beginGame = false;
        gameIsActive = false;
    }

    public void RestartGame()
    {
        // SceneManager reloads everything fresh, so we don't need to set gameIsActive here
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        gameIsActive = true;
        spawnRate /= difficulty;
        InvokeRepeating("SpawnBarrel", startDelay, spawnRate);
        
        // FIX: Tell the powerups to start spawning too!
        powerupSpawner.StartPowerup();

        UpdateScore(0);
        titleScreen.gameObject.SetActive(false);
        myPCS.GetComponent<Animator>().SetBool("Static_b", false);
        myPCS.GetComponent<Animator>().SetFloat("Speed_f", 1f);
    }
}