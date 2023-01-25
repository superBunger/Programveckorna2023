using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public Animator settingsAnimator;
    public AudioMixer mixer;
    public bool isThemePlaying = true;

    public SaveData savingScript;

    void Update()
    {
        //If satser för att stänga av meny musiken  - erik
        if (isThemePlaying == true && savingScript.muteThemeToggle.isOn == true)
        {
            PlayerPrefs.SetInt("MenuThemeMuted", 0);
            FindObjectOfType<AudioManager>().ChangeVolume("MenuTheme", 1.0f);
        }
        if(isThemePlaying == false && savingScript.muteThemeToggle.isOn == false)
        {
            PlayerPrefs.SetInt("MenuThemeMuted", 1);
            FindObjectOfType<AudioManager>().ChangeVolume("MenuTheme", 0.0f);
        }

        if(Input.GetKeyDown(KeyCode.Escape) && settingsAnimator.GetBool("SettingsLoaded") && SceneManager.GetActiveScene().buildIndex == 0)
        {
            //If sats för att stänga inställningarna och spara spelet - erik
            settingsAnimator.SetTrigger("SettingsClose");
            settingsAnimator.SetBool("SettingsLoaded", false);
            savingScript.SaveSettings();
        }
    }

    //Tillbaka pillen som stänger settings menyn - erik
    public void closeSettingsButton()
    {
        settingsAnimator.SetTrigger("SettingsClose");
        settingsAnimator.SetBool("SettingsLoaded", false);
        savingScript.SaveSettings();
    }
    //Master Volume Slider
    //Ändrar volymen för all musik - erik
    public void setMasterVolume (float masterVolume)
    {
        mixer.SetFloat("MasterVolume", Mathf.Log10(masterVolume) * 20);
        PlayerPrefs.SetFloat("MasterVolume", masterVolume);
    }
    //Music Volume Slider
    //Ändrar volymen för musiken - erik

    public void setMusicVolume(float musicVolume)
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(musicVolume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
    }

    //Effects Volume Slider
    //Ändrar volymen för effekterna - erik
    public void setEffectsVolume(float effectsVolume)
    {
        mixer.SetFloat("EffectsVolume", Mathf.Log10(effectsVolume) * 20);
        PlayerPrefs.SetFloat("EffectsVolume", effectsVolume);
    }

    //Toggle Fullscreen Checkbox
    //byter mellan fullscreen och windowed om man bockar av  - erik
    public void setFullscreen (bool isFullscreen)
    {
        if(isFullscreen == true)
        {
            Screen.fullScreen = isFullscreen;
        }
        if(isFullscreen == false)
        {
            Screen.fullScreen = isFullscreen;
        }
    }

    //Mute Menu Music Checkbox
    //Stänger av musiken i menyn om man bockar av - erik
    public void muteMenuMusic(bool isPlaying)
    {
        if(isPlaying == true)
        {
            isThemePlaying = true;
        }
        if(isPlaying == false)
        {
            isThemePlaying = false;
        }
    }


}
