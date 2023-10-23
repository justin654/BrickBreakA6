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
    private int hitsRemaining;

    [Header("Block Properties")]
    [SerializeField] private int blockHitsToDestroy = 1;
    [SerializeField] private bool unbreakable = false;


    // Start is called before the first frame update
    void Start()
    {
        timesHit = 0;
        hitsRemaining = blockHitsToDestroy;

        audioSource = GetComponent<AudioSource>();

        gameSession = FindObjectOfType<GameSession>();
        if (gameSession != null)
        {
            gameSession.RegisterBlock(!unbreakable);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (unbreakable) return;

        timesHit++;
        hitsRemaining--;

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
