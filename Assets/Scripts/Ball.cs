using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float InitialBallVelocity = 15.0f;
    public bool isStarted = false;
    private GameObject paddle;
    private Vector2 ballPaddleDelta;
    public AudioClip blockHitSound;
    public AudioClip wallHitSound;
    public AudioClip paddleHitSound;
    private AudioSource audioSource;





    private Rigidbody2D ballRB;



    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        ballRB = GetComponent<Rigidbody2D>();

        Paddle paddleComponent = FindObjectOfType<Paddle>();
        if (paddleComponent != null) 
        {
            paddle = paddleComponent.gameObject; 
        }

        ballPaddleDelta = transform.position - paddle.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStarted)
        {
            PaddleLocked();
            CheckForStart();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;
        // I found out we can use Switch statement instead of a bunch of Ifs. lets give it a shot
        switch (tag)
        {
            case "Block":
                audioSource.PlayOneShot(blockHitSound);
                break;
            case "Wall":
                audioSource.PlayOneShot(wallHitSound);
                break;
            case "Paddle":
                audioSource.PlayOneShot(paddleHitSound);
                break;
            default:
                //if no tag, just play a default block hit sound
                audioSource.PlayOneShot(blockHitSound);
                break;
        }
    }



    void LaunchBall()
    {
        ballRB.velocity = new Vector2(0, InitialBallVelocity);
        Debug.Log("LaunchBall called");
    }

    void CheckForStart()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isStarted = true;
            LaunchBall();
        }
    }

    private void PaddleLocked()
    {
        Vector2 paddlePos = new Vector2(paddle.transform.position.x, paddle.transform.position.y);
        transform.position = paddlePos + ballPaddleDelta;
    }




}
