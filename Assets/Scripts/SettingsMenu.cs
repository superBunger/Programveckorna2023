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
        PlayerPrefs.SetFloat("MasterVolume", masterVolume);
    }
    //Music Volume Slider
    public void setMusicVolume(float musicVolume)
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(musicVolume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
    }

    //Effects Volume Slider
    public void setEffectsVolume(float effectsVolume)
    {
        mixer.SetFloat("EffectsVolume", Mathf.Log10(effectsVolume) * 20);
        PlayerPrefs.SetFloat("EffectsVolume", effectsVolume);
    }

    //Toggle Fullscreen Checkbox
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
