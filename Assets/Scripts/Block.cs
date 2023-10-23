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
    private SpriteRenderer spriteRenderer;


    [Header("Block Properties")]
    [SerializeField] private int blockHitsToDestroy = 1;
    [SerializeField] private int hitsRemaining;

    [SerializeField] private bool unbreakable = false;

    [Header("Color Properties")]
    [SerializeField] private Color breakableColor = Color.white;
    [SerializeField] private Color unbreakableColor = Color.red;

    [Header("Explode Properties")]
    public bool explodeOnDestroy;
    public GameObject fragmentPrefab;
    public int numberOfFragments = 5;
    public float delayTime = 0.1f;





    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        gameSession = FindObjectOfType<GameSession>();

        if (gameSession != null)
        {
            gameSession.RegisterBlock(!unbreakable);
        }

        if (unbreakable)
        {
            spriteRenderer.color = unbreakableColor;
        }
        else
        {
            spriteRenderer.color = breakableColor;
        }


        timesHit = 0;
        hitsRemaining = blockHitsToDestroy;
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

    public int GetHitsRemaining()
    {
        return hitsRemaining;
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
        if (explodeOnDestroy == true){
            Explode();
        }
        Destroy(gameObject, hitSound.length);

    }

    private void Explode()
    {
        for (int i = 0; i < numberOfFragments; i++)
        {
            GameObject fragment = Instantiate(fragmentPrefab, transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));

            Rigidbody2D rb = fragment.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 randomForce = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * Random.Range(2f, 5f);
                rb.AddForce(randomForce, ForceMode2D.Impulse);
            }
            Destroy(fragment, delayTime);
        }
    }
}
