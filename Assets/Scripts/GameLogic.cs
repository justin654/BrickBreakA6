using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameLogic : MonoBehaviour
{
    [SerializeField] private SceneLoader SceneLoader;
    [SerializeField] private int initialUpperBound = 1000;
    [SerializeField] private int initialLowerBound = 1;
    [SerializeField] private TextMeshProUGUI guessText;
    [SerializeField] private int upperBoundText;
    [SerializeField] private int lowerBoundText;

    private int upperBound;
    private int lowerBound;
    private int guess;

    private void Start()
    {
        Debug.Log("Start Game was invoked");
        lowerBound = initialLowerBound;
        upperBound = initialUpperBound;
        NextGuess();
    }

    private void UpdateBoundTexts()
    {
        lowerBoundText = lowerBound;
        upperBoundText = upperBound;
    }

    private void OnPressHigher()
    {
        Debug.Log("Higher button was pressed");
        if (guess >= upperBound)
        {
            Debug.Log("Cheating detected! You can't go higher.");
            return;
        }

        lowerBound = guess + 1;
        UpdateBoundTexts();
        NextGuess();
    }

    private void OnPressLower()
    {
        Debug.Log("Lower button was pressed");
        if (guess <= lowerBound)
        {
            Debug.Log("Cheating detected! You can't go lower.");
            return;
        }

        upperBound = guess - 1;
        UpdateBoundTexts();
        NextGuess();
    }

    private void OnPressSuccess()
    {
        Debug.Log("Success button pressed");
        SceneLoader.LoadWinScene();
    }

    private void NextGuess()
    {
        if (lowerBound > upperBound)
        {
            Debug.Log("Invalid bounds.");
            UpdateBoundTexts();
            return;
        }

        guess = Random.Range(lowerBound, upperBound + 1);
        UpdateBoundTexts();
        guessText.text = guess.ToString();
    }
}
