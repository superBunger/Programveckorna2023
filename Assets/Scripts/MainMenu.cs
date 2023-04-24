using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private CanvasGroup myUIGroup;
    [SerializeField] private bool fadeIn = true;

    public Animator creditsAnimator;
    public Animator settingsAnimator;
    public LevelLoader levelLoaderScript;
    public SaveData saveManager;
    public GameObject continueButton;

    public GameObject coloredTree;
    public GameObject logo;
    public Sprite invertedLogo;
    public Sprite outvertedLogo;
    public Camera cam;
    //I've given up. - Henry
    public Button coninueText;
    public Button settingsText;
    public Button CreditsText;
    public Button NewGameText;
    public Button QuitText;
    public TextMeshProUGUI HenryText;
    public TextMeshProUGUI HenryRoleText;
    public TextMeshProUGUI WilliamText;
    public TextMeshProUGUI WilliamRoleText;
    public TextMeshProUGUI LeeText;
    public TextMeshProUGUI LeeRoleText;
    public TextMeshProUGUI ErikText;
    public TextMeshProUGUI ErikRoleText;
    public TextMeshProUGUI SamText;
    public TextMeshProUGUI SamRoleText;
    public TextMeshProUGUI MaxText;
    public TextMeshProUGUI MaxRoleText;
    public TextMeshProUGUI RonnieText;
    public TextMeshProUGUI RonnieRoleText;
    void Start()
    {
        if(PlayerPrefs.GetInt("beatenGame") == 1)
        {
            coloredTree.SetActive(true);
            logo.gameObject.GetComponent<Image>().sprite = invertedLogo;
            cam.backgroundColor = Color.white;

            ColorBlock cbConinue1 = coninueText.colors;
            ColorBlock cbConbinue2 = coninueText.colors;
            cbConinue1.normalColor = Color.black;
            cbConbinue2.highlightedColor = Color.grey;
            coninueText.colors = cbConinue1;
            coninueText.colors = cbConbinue2;

            settingsText.colors = cbConinue1;
            settingsText.colors = cbConbinue2;

            CreditsText.colors = cbConinue1;
            CreditsText.colors = cbConbinue2;

            NewGameText.colors = cbConinue1;
            NewGameText.colors = cbConbinue2;

            QuitText.colors = cbConinue1;
            QuitText.colors = cbConbinue2;
        }
        else
        {
            coloredTree.SetActive(false);
            logo.gameObject.GetComponent<Image>().sprite = outvertedLogo;
            cam.backgroundColor = Color.black;

            ColorBlock cbConinue2 = coninueText.colors;
            ColorBlock cbConbinue2 = coninueText.colors;
            cbConinue2.normalColor = Color.white;
            cbConbinue2.highlightedColor = Color.gray;
            coninueText.colors = cbConbinue2;
            coninueText.colors = cbConinue2;

            settingsText.colors = cbConinue2;
            settingsText.colors = cbConbinue2;

            CreditsText.colors = cbConinue2;
            CreditsText.colors = cbConbinue2;

            NewGameText.colors = cbConinue2;
            NewGameText.colors = cbConinue2;

            QuitText.colors = cbConinue2;
            QuitText.colors = cbConinue2;
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
        while(myUIGroup.alpha > 0)
        {
            myUIGroup.alpha -= Time.deltaTime;
        }
        Application.Quit();
        saveManager.SaveSettings();
    }

    public void Update()
    {
        if (fadeIn = true)
        {
            myUIGroup.alpha += Time.deltaTime;
            if (myUIGroup.alpha >= 1)
            {
                fadeIn = false;
            }
        }
    }



}
