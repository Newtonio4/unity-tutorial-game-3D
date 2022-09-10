using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public ParticleSystem destroyParticle;
    public ParticleSystem enemyCollisionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound;
    public AudioClip chashSound;
    public AudioClip enemySound;
    public TextMeshProUGUI scaleText;

    private GameManager gameManager;
    private AudioSource playerAudio;
    private float speed = 10.0f;
    private float moveBound = 23;
    private float jumpHeight = 400;
    private int maxSize = 10;
    private float maxScalePoints = 30;
    private bool isOnGround = true;
    private Rigidbody playerRb;

    private int translationX;
    private int translationZ;

    public int scale = 1;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        playerAudio = GetComponent<AudioSource>();
        playerRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && isOnGround)
            dirtParticle.Play();

        if (Input.GetKeyUp(KeyCode.LeftShift) && isOnGround)
            dirtParticle.Stop();

        if (Input.GetKey(KeyCode.LeftShift) && isOnGround)
            speed = 17;
        else
            speed = 10;

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRb.AddForce(Vector2.up * jumpHeight, ForceMode.Impulse);
            isOnGround = false;

            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }

        translationX = 0;
        translationZ = 0;

        if (transform.position.x < -moveBound)
            transform.position = new Vector3(-moveBound, transform.position.y, transform.position.z);
        else if (transform.position.x > moveBound)
            transform.position = new Vector3(moveBound, transform.position.y, transform.position.z);
        else if (transform.position.z < -moveBound)
            transform.position = new Vector3(transform.position.x, transform.position.y, -moveBound);
        else if (transform.position.z > moveBound)
            transform.position = new Vector3(transform.position.x, transform.position.y, moveBound);
        else
        {
            if (Input.GetKey(KeyCode.A))
            {
                translationX += 1;
                translationZ -= 1;
            }
            if (Input.GetKey(KeyCode.W))
            {
                translationX -= 1;
                translationZ -= 1;
            }
            if (Input.GetKey(KeyCode.D))
            {
                translationX -= 1;
                translationZ += 1;
            }
            if (Input.GetKey(KeyCode.S))
            {
                translationX += 1;
                translationZ += 1;
            }

            if (translationX != 0 || translationZ != 0)
            {
                if (translationX == 0 && translationZ == 2)
                    transform.localEulerAngles = new Vector3(0, 0, 0);
                else if (translationX == -1 && translationZ == 1)
                    transform.localEulerAngles = new Vector3(0, -45, 0);
                else if (translationX == -2 && translationZ == 0)
                    transform.localEulerAngles = new Vector3(0, -90, 0);
                else if (translationX == -1 && translationZ == -1)
                    transform.localEulerAngles = new Vector3(0, -135, 0);
                else if (translationX == 1 && translationZ == 1)
                    transform.localEulerAngles = new Vector3(0, 45, 0);
                else if (translationX == 2 && translationZ == 0)
                    transform.localEulerAngles = new Vector3(0, 90, 0);
                else if (translationX == 1 && translationZ == -1)
                    transform.localEulerAngles = new Vector3(0, 135, 0);
                else if (translationX == 0 && translationZ == -2)
                    transform.localEulerAngles = new Vector3(0, 180, 0);

                transform.Translate(Vector3.forward * Time.deltaTime * speed);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(collision.gameObject);

            if (scale < maxScalePoints)
            {
                scale++;
                ChangePlayerScale();
                var newParticle = Instantiate(destroyParticle, collision.contacts[0].point, collision.gameObject.transform.rotation);
                Destroy(newParticle.gameObject, 2);

                playerAudio.PlayOneShot(chashSound, 1.0f);
            }
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            var enemyCost = collision.gameObject.GetComponent<Enemy>().cost;
            if (scale > enemyCost)
            {
                Destroy(collision.gameObject);
                scale -= enemyCost;
                ChangePlayerScale();
                var newParticle = Instantiate(enemyCollisionParticle, collision.contacts[0].point, collision.gameObject.transform.rotation);
                Destroy(newParticle.gameObject, 2);

                playerAudio.PlayOneShot(enemySound, 1.0f);
            }
            else
            {
                Destroy(gameObject);

                gameManager.GameOver();
            }
        }
    }

    void ChangePlayerScale()
    {
        float size = (1 + (maxSize - 1) * scale / maxScalePoints) / 4;
        transform.localScale = new Vector3(size, size, size);

        scaleText.text = "SCALE: " + scale;
    }
}
