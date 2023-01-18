using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator creditsAnimator;
    public Animator settingsAnimator;
    public LevelLoader levelLoaderScript;
    public SaveData saveManager;
    public GameObject continueButton;

    void Start()
    {
        if(levelLoaderScript.furthestSceneReached > 1)
        {
            continueButton.SetActive(true);
        }
    }

    public void ContinueGame()
    {
        FindObjectOfType<AudioManager>().StopMusic();
        levelLoaderScript.transition.SetTrigger("ClickNewGame");
        levelLoaderScript.LoadFurthestLevel();
        
    }

    //"New Game" Button
    //Loads the scene with the next build index (Seen in File -> Build Settings)
    public void PlayGame()
    {
        FindObjectOfType<AudioManager>().StopMusic();
        levelLoaderScript.transition.SetTrigger("ClickNewGame");
        levelLoaderScript.LoadNextLevel();
    }

    //"Settings" button
    //Triggers the settings animation
    public void OpenSettings()
    {
        settingsAnimator.SetTrigger("SettingsOpen");
        settingsAnimator.SetBool("SettingsLoaded", true);
    }

    //Lets your press escape to exit settings
    public void ExitSettings()
    {
        settingsAnimator.SetTrigger("SettingsClose");
    }

    //"Credits" Button
    //Triggeres the credits 
    public void PlayCredits()
    {
        creditsAnimator.SetTrigger("CreditsOpen");
    }

    //"Back" Button in credits
    //Appears after the credits animation is done
    public void ExitCredits()
    {
        creditsAnimator.SetTrigger("CreditsClose");
    }

    //"Quit" Button
    //Quits the game, no effect in the editor
    public void QuitGame()
    {
        print("Exited out of the game");
        Application.Quit();
        saveManager.SaveSettings();
    }

}
