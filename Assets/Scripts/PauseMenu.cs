using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public SaveData saveManager;
    public LevelLoader levelLoaderScript;
    public Animator pauseAnimator;

    void Start()
    {
        
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex > 0 && Input.GetKeyDown(KeyCode.Escape) && !pauseAnimator.GetBool("PauseLoaded"))
        {
            pauseAnimator.SetTrigger("PauseOpen");
            pauseAnimator.SetBool("PauseLoaded", true);
        }
    }

    public void Resume()
    {
        pauseAnimator.SetTrigger("PauseClose");
        pauseAnimator.SetBool("PauseLoaded", false);
    }

    public void Settings()
    {
        pauseAnimator.SetTrigger("PauseClose");
        pauseAnimator.SetBool("PauseLoaded", false);
        pauseAnimator.SetTrigger("SettingsOpen");
        pauseAnimator.SetBool("SettingsLoaded", true);
    }

    public void QuitMenu()
    {
        FindObjectOfType<AudioManager>().StopMusic();
        levelLoaderScript.transition.SetTrigger("ClickNewGame");
        levelLoaderScript.LoadMenuLevel();
    }

    public void QuitGame()
    {
        print("Exited out of the game");
        Application.Quit();
        saveManager.SaveSettings();
    }
}
