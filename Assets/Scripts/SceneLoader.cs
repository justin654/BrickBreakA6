using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public AudioClip gameOverSound;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

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
    public void LoadGameOverScene()
    {
        StartCoroutine(PlaySoundThenLoadScene(gameOverSound, "GameOver"));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator PlaySoundThenLoadScene(AudioClip sound, string sceneName)
    {
        audioSource.PlayOneShot(sound);
        yield return new WaitForSeconds(sound.length);
        SceneManager.LoadScene(sceneName);
    }

}
