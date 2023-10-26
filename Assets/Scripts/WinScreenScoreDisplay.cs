using TMPro;
using UnityEngine;

public class WinScreenScoreDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameSession gameSession = FindObjectOfType<GameSession>();

        if (gameSession != null)
        {
            TextMeshProUGUI finalScoreText = GetComponent<TextMeshProUGUI>();
            finalScoreText.text = "Final Score: " + gameSession.GetScore();
            gameSession.ResetGame(); // Display the final score, then reset it for if we decide to play again.
        }
        else
        {
            // The GameSession wont exist if you just play from the Win scene, so throw debug if I do that
            Debug.LogError("GameSession not found! Did you start at Win scene without playing?");
        }
    }
}