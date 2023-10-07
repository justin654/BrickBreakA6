using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float InitialBallVelocity = 5.0f;

    private Rigidbody2D ballRB;



    // Start is called before the first frame update
    void Start()
    {
        ballRB = GetComponent<Rigidbody2D>(); //cache it for use 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 0 for left mouse btn
        {
            LaunchBall();
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


}
