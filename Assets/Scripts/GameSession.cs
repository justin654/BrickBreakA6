using UnityEngine.SceneManagement;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    private static GameSession _instance;
    private SceneLoader sceneLoader;

    [Header("Game Properties")]
    [SerializeField] private float initialGameSpeed = 1f;
    [SerializeField] private float gameSpeed;
    [SerializeField] public int score = 0;

    [Header("Level Properties")]
    [SerializeField] private string currentSceneName;
    [SerializeField] private int breakableBlocks;

    private void Awake()
    {
        SetupSingleton();
    }

    void Start()
    {
        Time.timeScale = initialGameSpeed;
        gameSpeed = initialGameSpeed;

        sceneLoader = FindObjectOfType<SceneLoader>();
        currentSceneName = SceneManager.GetActiveScene().name; // Store current scene we are in
    }

    private void SetupSingleton()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        //Subscribe to sceneLoaded event so we can reset the breakableBlocks to 0 when new scene happens. This is to fix
        //the bug where the breakableBlocks were compounding when a new level was loaded.
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // When a new scene loads, we update the currentSceneName(So we know where we are, and what level is next) and reset breakableBlocks to 0.
        currentSceneName = scene.name;
        breakableBlocks = 0;

        Debug.Log("Scene loaded: " + scene.name + ", reset breakableBlocks to 0.");
    }

    // Method for changing game speeds
    public void SetGameSpeed(float gameSpeed)
    {
        Time.timeScale = gameSpeed; // update Unity speed
        this.gameSpeed = gameSpeed; //Reflect in inspector for debug - Rider says this is shawdowing
    }

    public void AddToScore(int pointsToAdd)
    {
        score += pointsToAdd;
    }

    public int GetScore()
    {
        return score;
    }

    public void ResetGame()
    {
        // When we retry/replay, ensure we are resetting things so it doesn't keep carrying the values
        score = 0;
        breakableBlocks = 0;
    }

    public void RegisterBlock(bool isBreakable)
    {
        // We use this method to count ONLY the breakable blocks in the current level. 
        if (!isBreakable) return;
        breakableBlocks++;
        Debug.Log("RegisterBlock called. Current breakable blocks: " + breakableBlocks);
    }

    public void BlockDestroyed(int points)
    {
        // When block is destroyed, decrement the breakableBlocks, add points to our score, and if 0 or less left, lefts go to next level
        breakableBlocks--;
        AddToScore(points);
        if (breakableBlocks <= 0)
        {
            LoadNextLevel();
        }
    }

    private void LoadNextLevel()
    {
        if (currentSceneName == "Level01") // Doing it this way as the professor a while back mentioned something about not using buildIndex.
        {
            sceneLoader.LoadLevel2();
            SetGameSpeed(1.2f);
        }
        else if (currentSceneName == "Level02")
        {
            sceneLoader.LoadWinScene();
        }

    }

    private void OnDestroy()
    {
        // When block gets destroyed unsubscribe to the sceneLoaded event
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
