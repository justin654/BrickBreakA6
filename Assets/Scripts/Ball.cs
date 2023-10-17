using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float InitialBallVelocity = 15.0f;
    public bool isStarted = false;
    private GameObject paddle;
    private Vector2 ballPaddleDelta;
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
        if (collision.gameObject.tag == "Paddle")
        {
            audioSource.PlayOneShot(paddleHitSound);
        }
        else if (collision.gameObject.tag == "Wall")
        {
            audioSource.PlayOneShot(wallHitSound);
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
