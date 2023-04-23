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

    public GameObject coloredTree;

    void Start()
    {
        if(PlayerPrefs.GetInt("beatenGame") == 1)
        {
            coloredTree.SetActive(true);
        }
        else
        {
            coloredTree.SetActive(false);
        }
        //visar continue knappen om man kommit till level 2  - erik
        if (PlayerPrefs.GetInt("FurthestSceneReached") > 1)
        {
            continueButton.SetActive(true);
        }
    }

    //"Continue" button
    //Loads the furthest level you've reached  - erik
    public void ContinueGame()
    {
        levelLoaderScript.transition.SetTrigger("ClickNewGame");
        levelLoaderScript.LoadFurthestLevel();
        
    }

    //"New Game" Button
    //Loads the scene with the next build index (Seen in File -> Build Settings)  - erik
    public void PlayGame()
    {
        PlayerPrefs.SetInt("BatteryCharge", 0);
        PlayerPrefs.SetInt("FurthestSceneReached", 0);
        levelLoaderScript.transition.SetTrigger("ClickNewGame");
        levelLoaderScript.LoadNextLevel();
    }

    //"Settings" button
    //Triggers the settings animation - erik
    public void OpenSettings()
    {
        settingsAnimator.SetTrigger("SettingsOpen");
        settingsAnimator.SetBool("SettingsLoaded", true);
    }

    //Lets your press escape to exit settings - erik
    public void ExitSettings()
    {
        settingsAnimator.SetTrigger("SettingsClose");
    }

    //"Credits" Button
    //Triggeres the credits  - erik
    public void PlayCredits()
    {
        creditsAnimator.SetTrigger("CreditsOpen");
    }

    //"Back" Button in credits
    //Appears after the credits animation is done - erik
    public void ExitCredits()
    {
        creditsAnimator.SetTrigger("CreditsClose");
    }

    //"Quit" Button
    //Quits the game, no effect in the editor - erik
    public void QuitGame()
    {
        print("Exited out of the game");
        Application.Quit();
        saveManager.SaveSettings();
    }

}
