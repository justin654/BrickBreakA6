using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    private static GameSession instance;
    private SceneLoader sceneLoader;


    [SerializeField] private float initialGameSpeed = 1f;
    [SerializeField] private float gameSpeed;
    [SerializeField] private string currentSceneName;

    [SerializeField] public int score = 0;
    [SerializeField] int breakableBlocks;

    private void Awake()
    {
        SetupSingleton();
    }

    void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        Time.timeScale = initialGameSpeed;
        gameSpeed = initialGameSpeed;
        currentSceneName = SceneManager.GetActiveScene().name;
    }

    private void SetupSingleton()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentSceneName = scene.name;
        breakableBlocks = 0;

        Debug.Log("Scene loaded: " + scene.name + ", reset breakableBlocks to 0.");
    }

    // Method for changing game speeds
    public void SetGameSpeed(float gameSpeed)
    {
        Time.timeScale = gameSpeed; // update Unity speed
        this.gameSpeed = gameSpeed; //Reflect in inspector for debug
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
        score = 0;
        breakableBlocks = 0;
    }

    public void RegisterBlock(bool isBreakable)
    {
        // Only increment the count for breakable blocks
        if (isBreakable)
        {
            breakableBlocks++;
            Debug.Log("RegisterBlock called. Current breakable blocks: " + breakableBlocks);
        }
    }


    public void BlockDestroyed(int points)
    {
        breakableBlocks--;
        AddToScore(points);
        if (breakableBlocks <= 0)
        {
            LoadNextLevel ();
        }
    }

    private void LoadNextLevel()
    {
        if (currentSceneName == "Level01")
        {
            sceneLoader.LoadLevel2();
        }

    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
