using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip hitSound;
    public int points = 100;
    private GameSession gameSession;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        gameSession = FindObjectOfType<GameSession>();
        if (gameSession != null)
        {
            gameSession.RegisterBlock();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        audioSource.PlayOneShot(hitSound);
        Debug.Log("I was hit by: " + collision.gameObject.name);

        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        if (gameSession != null)
        {
            gameSession.BlockDestroyed(points);
        }

        Destroy(gameObject, hitSound.length);
    }
}
