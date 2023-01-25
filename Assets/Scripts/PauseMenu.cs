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

    //Coroutinen f�r att �ppna inst�llningar fr�n paus menyn - erik
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
        //�ppnar paus menyn om man trycker p� escape, �r redan inte i paus menyn och �r inte i settings menyn - erik
        if (SceneManager.GetActiveScene().buildIndex > 0 && Input.GetKeyDown(KeyCode.Escape) && !pauseAnimator.GetBool("PauseLoaded") && !settingsAnimator.GetBool("SettingsLoaded"))
        {
            pauseAnimator.SetTrigger("PauseOpen");
            pauseAnimator.SetBool("PauseLoaded", true);
        }
    }

    //Resume knappen
    //St�nger paus menyn - erik
    public void Resume()
    {
        pauseAnimator.SetTrigger("PauseClose");
        pauseAnimator.SetBool("PauseLoaded", false);
    }

    //Settings knappen
    //Startar coroutinen som �ppnar inst�llningarna och st�nger paus menyn - erik
    public void Settings()
    {
        StartCoroutine(OpenSettings());
    }

    //Sparar progress och g�r tillbaka till huvudmenyn - erik
    public void QuitMenu()
    {
        saveManager.SaveSettings();
        pauseAnimator.SetTrigger("PauseClose");
        pauseAnimator.SetBool("PauseLoaded", false);
        levelLoaderScript.transition.SetTrigger("ClickNewGame");
        levelLoaderScript.LoadMenuLevel();
    }

    //Sparar progress och st�nger spelet - erik
    public void QuitGame()
    {
        saveManager.SaveSettings();
        Application.Quit();
        print("Exited out of the game");
    }
}
