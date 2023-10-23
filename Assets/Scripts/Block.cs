using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip hitSound;
    public int points = 100;
    private GameSession gameSession;
    private int timesHit;

    [Header("Block Properties")]
    [SerializeField] private int blockHitsToDestroy = 1; // by default, a block is destroyed by one hit
    [SerializeField] private bool unbreakable = false;

    // Start is called before the first frame update
    void Start()
    {
        timesHit = 0;

        audioSource = GetComponent<AudioSource>();

        gameSession = FindObjectOfType<GameSession>();
        if (gameSession != null)
        {
            gameSession.RegisterBlock(!unbreakable); // here we pass whether the block is breakable
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (unbreakable) return; // If block is unbreakable, ignore the collision/hit

        timesHit++; // Increase hit counter

        if (timesHit >= blockHitsToDestroy)
        {
            DestroyBlock();
        }
        else
        {
            audioSource.PlayOneShot(hitSound);
        }
    }

    private void DestroyBlock()
    {
        audioSource.PlayOneShot(hitSound);


        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        if (gameSession != null)
        {
            gameSession.BlockDestroyed(points);
        }

        Destroy(gameObject, hitSound.length);

    }


}
