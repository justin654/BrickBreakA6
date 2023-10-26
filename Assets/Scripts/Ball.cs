using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float initialBallVelocity = 20.0f;
    public bool isStarted = false;
    private GameObject paddle;
    private Vector2 ballPaddleDelta;
    public AudioClip blockHitSound;
    public AudioClip wallHitSound;
    public AudioClip paddleHitSound;
    private AudioSource audioSource;

    private Rigidbody2D ballRB;

    [Header("Ball Debug")]
    [SerializeField] private float currentBallVelocity; // BUG Ball ends up losing some velocity when it hits something odd, trying to see how slow it ends up 

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
        currentBallVelocity = ballRB.velocity.magnitude;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isStarted) return; //We dont want the collision sound playing while ball is frozen to paddle

        string tag = collision.gameObject.tag;
        audioSource.pitch = Random.Range(0.95f, 1.05f);
        // I found out we can use Switch statement instead of a bunch of Ifs. lets give it a shot
        switch (tag)
        {
            case "Block":
                audioSource.PlayOneShot(blockHitSound);
                break;
            case "Wall":
                audioSource.pitch = Random.Range(0.8f, 1.2f);
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
        ballRB.velocity = new Vector2(0, initialBallVelocity);
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
        var position = paddle.transform.position;
        Vector2 paddlePos = new Vector2(position.x, position.y);
        transform.position = paddlePos + ballPaddleDelta;
    }

}
