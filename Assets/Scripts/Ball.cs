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



    private Rigidbody2D ballRB;



    // Start is called before the first frame update
    void Start()
    {
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
            AudioSource.PlayClipAtPoint(paddleHitSound, transform.position);
        }
        else if (collision.gameObject.tag == "Wall")
        {
            AudioSource.PlayClipAtPoint(wallHitSound, transform.position);
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
