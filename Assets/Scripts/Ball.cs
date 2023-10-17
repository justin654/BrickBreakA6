using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float InitialBallVelocity = 5.0f;
    public bool isStarted = false;
    private GameObject paddle;
    private Vector2 ballPaddleDelta;


    private Rigidbody2D ballRB;



    // Start is called before the first frame update
    void Start()
    {
        ballRB = GetComponent<Rigidbody2D>();
        Paddle paddleComponent = FindObjectOfType<Paddle>();
        paddle = paddleComponent.gameObject;
        paddle = FindObjectOfType<Paddle>();
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
        Debug.Log("The " + gameObject.name + " has collided with " + collision.gameObject.name);
    }

    void LaunchBall()
    {
        ballRB.velocity = new Vector2(0, InitialBallVelocity);
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
