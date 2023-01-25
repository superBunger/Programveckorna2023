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

    //Coroutinen för att öppna inställningar från paus menyn - erik
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
        //Öppnar paus menyn om man trycker på escape, är redan inte i paus menyn och är inte i settings menyn - erik
        if (SceneManager.GetActiveScene().buildIndex > 0 && Input.GetKeyDown(KeyCode.Escape) && !pauseAnimator.GetBool("PauseLoaded") && !settingsAnimator.GetBool("SettingsLoaded"))
        {
            pauseAnimator.SetTrigger("PauseOpen");
            pauseAnimator.SetBool("PauseLoaded", true);
        }
    }

    //Resume knappen
    //Stänger paus menyn - erik
    public void Resume()
    {
        pauseAnimator.SetTrigger("PauseClose");
        pauseAnimator.SetBool("PauseLoaded", false);
    }

    //Settings knappen
    //Startar coroutinen som öppnar inställningarna och stänger paus menyn - erik
    public void Settings()
    {
        StartCoroutine(OpenSettings());
    }

    //Sparar progress och går tillbaka till huvudmenyn - erik
    public void QuitMenu()
    {
        saveManager.SaveSettings();
        pauseAnimator.SetTrigger("PauseClose");
        pauseAnimator.SetBool("PauseLoaded", false);
        levelLoaderScript.transition.SetTrigger("ClickNewGame");
        levelLoaderScript.LoadMenuLevel();
    }

    //Sparar progress och stänger spelet - erik
    public void QuitGame()
    {
        saveManager.SaveSettings();
        Application.Quit();
        print("Exited out of the game");
    }
}
