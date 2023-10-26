using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public AudioClip gameOverSound;
    private AudioSource audioSource;
    private GameSession gameSession;

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
        if (gameSession != null)
        {
            gameSession.ResetGame(); // We reset here as well for safe keeping
        }
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

    public void LoadLevel2()
    {
        SceneManager.LoadScene("Level02");
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
