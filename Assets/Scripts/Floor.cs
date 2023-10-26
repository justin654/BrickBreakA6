using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    private GameSession gameSession;

    private void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Debug.Log("You hit the floor and lost, transition to gameover and reset all vars");

            if (gameSession != null)
            {
                gameSession.ResetGame();
            }

            SceneLoader sceneLoader = FindObjectOfType<SceneLoader>();
            if (sceneLoader != null)
            {
                sceneLoader.LoadGameOverScene();
            }
            else
            {
                Debug.LogError("SceneLoader instance was not found.");
            }
        }
    }
}
