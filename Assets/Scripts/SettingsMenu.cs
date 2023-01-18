using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public Animator settingsAnimator;
    public AudioMixer mixer;

    public SaveData savingScript;

    void Update()
    {
        if(savingScript.gameDataClass.hasMenuThemeMuted == 1)
        {
            FindObjectOfType<AudioManager>().ChangeVolume("MenuTheme", 0.0f);
        }
        else
        {
            FindObjectOfType<AudioManager>().ChangeVolume("MenuTheme", 1.0f);
        }

        if(Input.GetKeyDown(KeyCode.Escape) && !settingsAnimator.GetBool("SettingsLoaded"))
        {
            settingsAnimator.SetTrigger("SettingsClose");
            settingsAnimator.SetBool("SettingsLoaded", false);
        }

        if(Input.GetKeyDown(KeyCode.Escape) && settingsAnimator.GetBool("SettingsLoaded"))
        {
            settingsAnimator.SetTrigger("SettingsClose");
            settingsAnimator.SetBool("SettingsLoaded", false);
            savingScript.SaveSettings();
        }
    }

    public void closeSettingsButton()
    {
        settingsAnimator.SetTrigger("SettingsClose");
        settingsAnimator.SetBool("SettingsLoaded", false);
        savingScript.SaveSettings();
    }
    //Master Volume Slider
    public void setMasterVolume (float masterVolume)
    {
        mixer.SetFloat("MasterVolume", Mathf.Log10(masterVolume) * 20);
        mixer.GetFloat("MasterVolume", out savingScript.gameDataClass.masterVolume);
    }
    //Music Volume Slider
    public void setMusicVolume(float musicVolume)
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(musicVolume) * 20);
        mixer.GetFloat("MusicVolume", out savingScript.gameDataClass.musicVolume);
    }

    //Effects Volume Slider
    public void setEffectsVolume(float effectsVolume)
    {
        mixer.SetFloat("EffectsVolume", Mathf.Log10(effectsVolume) * 20);
        mixer.GetFloat("EffectsVolume", out savingScript.gameDataClass.effectsVolume);
    }

    //Toggle Fullscreen Checkbox
    public void setFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        if(isFullscreen == true)
        {
            savingScript.gameDataClass.isGameFullscreen = 1;
            PlayerPrefs.SetInt("gameIsFullscreen", savingScript.gameDataClass.isGameFullscreen);
        }

        if(isFullscreen == false)
        {
            savingScript.gameDataClass.isGameFullscreen = 0;
            PlayerPrefs.SetInt("gameIsFullscreen", savingScript.gameDataClass.isGameFullscreen);
        }
    }
    
    //Mute Menu Music Checkbox
    public void muteMenuMusic(bool isPlaying)
    {
        if(isPlaying == true)
        {
            savingScript.gameDataClass.hasMenuThemeMuted = 0;
            PlayerPrefs.SetInt("gameMenuThemeMuted", savingScript.gameDataClass.hasMenuThemeMuted);
        }
        if(isPlaying == false)
        {
            savingScript.gameDataClass.hasMenuThemeMuted = 1;
            PlayerPrefs.SetInt("gameMenuThemeMuted", savingScript.gameDataClass.hasMenuThemeMuted);
        }
    }


}
