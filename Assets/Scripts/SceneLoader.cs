using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadGameScene()
    {
        SceneManager.LoadScene("Level01");
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene("StartMenu"); 
    }

    public void LoadWinScene()
    {
        SceneManager.LoadScene("Win"); 
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
