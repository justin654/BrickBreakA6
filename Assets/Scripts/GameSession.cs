using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField] private float initialGameSpeed = 1f;
    [SerializeField] private float gameSpeed;

    private void Awake()
    {
        Debug.Log("In Awake() on " + transform.name);
        if ((FindObjectsOfType<GameSession>().Length) > 1)
        {
            Debug.Log(transform.name + " is a dulpcate instance of GameSession, going away");
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            Debug.Log(transform.name + " has the Singleton for GameSession");
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        Time.timeScale = initialGameSpeed;
        gameSpeed = initialGameSpeed;
    }

    // Method for changing game speeds
    public void SetGameSpeed(float gameSpeed)
    {
        Time.timeScale = gameSpeed; // update Unity speed
        this.gameSpeed = gameSpeed; //Reflect in inspector for debug
    }
}
