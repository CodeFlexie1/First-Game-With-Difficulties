using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody myRB;
    public float jumpForce = 10;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool gameOver = false; // FIX: Start this as false
    public bool hasPowerup = false;
    public bool beginGame = true;
    public float powerupStrength = 15;
    public GameObject powerupID;
    private Animator myAnimator;
    private AudioSource myAudioSource;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public AudioClip barrelExplosionSound;
    public ParticleSystem dirtParticles;
    public ParticleSystem crashParticles;
    public ParticleSystem barrelExpload;

    private SpawnObstacles mySPO;

    void Start()
    {
        beginGame = true;
        gameOver = false;
        myRB = GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0, -9.81f, 0);
        Physics.gravity *= gravityModifier;
        myAnimator = GetComponent<Animator>();
        myAudioSource = GetComponent<AudioSource>();
        mySPO = GameObject.Find("Spawn Manager").GetComponent<SpawnObstacles>();
        myAnimator.SetBool("Static_b", true);
        dirtParticles.Stop();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && beginGame == true && isOnGround && !gameOver && mySPO.gameIsActive == true)
        {
            myRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            myAnimator.SetTrigger("Jump_trig");
            myAudioSource.PlayOneShot(jumpSound, 0.5f);
            dirtParticles.Stop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerUpDuration());
            powerupID.SetActive(true);
        }
    }

    IEnumerator PowerUpDuration()
    {
        yield return new WaitForSeconds(8);
        hasPowerup = false;
        powerupID.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && beginGame == true && mySPO.gameIsActive == true)
        {
            isOnGround = true;
            dirtParticles.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            if (hasPowerup == true)
            {
                Destroy(collision.gameObject);
                barrelExpload.Play();
                mySPO.UpdateScore(1);
                myAudioSource.PlayOneShot(barrelExplosionSound, 0.5f);
            }
            else
            {
                myAnimator.SetBool("Death_b", true);
                gameOver = true;
                myAudioSource.PlayOneShot(crashSound, 0.5f);
                PlayerExplosion();
                dirtParticles.Stop();
                beginGame = false;
                mySPO.StopGame();
            }
        }
    }

    void PlayerExplosion()
    {
        crashParticles.Play();
    }
}
