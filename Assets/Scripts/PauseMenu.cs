using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public SaveData saveManager;
    public LevelLoader levelLoaderScript;
    public Animator settingsAnimator;
    public Animator pauseAnimator;

    public IEnumerator OpenSettings()
    {
        pauseAnimator.SetTrigger("PauseClose");
        yield return new WaitForSeconds(0.1f);
        pauseAnimator.SetBool("PauseLoaded", false);
        settingsAnimator.SetTrigger("SettingsOpen");
        settingsAnimator.SetBool("SettingsLoaded", true);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            OpenSettings();
        }
        if (SceneManager.GetActiveScene().buildIndex > 0 && Input.GetKeyDown(KeyCode.Escape) && !pauseAnimator.GetBool("PauseLoaded") && !settingsAnimator.GetBool("SettingsLoaded"))
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
        StartCoroutine(OpenSettings());
    }

    public void QuitMenu()
    {
        pauseAnimator.SetTrigger("PauseClose");
        pauseAnimator.SetBool("PauseLoaded", false);
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
