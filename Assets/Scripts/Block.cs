using UnityEngine;

public class Block : MonoBehaviour
{
    private int timesHit;

    private SpriteRenderer spriteRenderer;
    private GameSession gameSession;
    private AudioSource audioSource;
    public AudioClip hitSound;



    [Header("Block Properties")]
    [SerializeField] private int points = 100;

    [SerializeField] private int blockHitsToDestroy = 1;
    [SerializeField] private int hitsRemaining;
    [SerializeField] private bool unbreakable = false;
    [SerializeField] private Color breakableColor = Color.white;
    [SerializeField] private Color unbreakableColor = Color.red;

    [Header("Explode Settings")]
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
            // I need to change this to update the sprite, not just color
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

        if (explodeOnDestroy == true)
        {
            Explode();
        }
        Destroy(gameObject, hitSound.length);

    }

    private void Explode()
    {
        for (int i = 0; i < numberOfFragments; i++)
        {
            GameObject fragment = Instantiate(fragmentPrefab, transform.position, Quaternion.identity);

            float randomX = Random.Range(-3, 3);
            float randomY = Random.Range(-3, 3);

            Rigidbody2D rb = fragment.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = new Vector2(randomX, randomY);
            }

            Destroy(fragment, delayTime);
        }
    }
}
