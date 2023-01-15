using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public Animator settingsAnimator;
    public AudioMixer mixer;
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !settingsAnimator.GetBool("SettingsLoaded"))
        {
            settingsAnimator.SetTrigger("SettingsClose");
            settingsAnimator.SetBool("SettingsLoaded", false);
        }

        if(Input.GetKeyDown(KeyCode.Escape) && settingsAnimator.GetBool("SettingsLoaded"))
        {
            settingsAnimator.SetTrigger("SettingsClose");
            settingsAnimator.SetBool("SettingsLoaded", false);
        }
    }

    //Master Volume Slider
    public void setMasterVolume (float masterVolume)
    {
        mixer.SetFloat("MasterVolume", Mathf.Log10(masterVolume) * 20);
    }
    //Music Volume Slider
    public void setMusicVolume(float musicVolume)
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(musicVolume) * 20);
    }

    //Effects Volume Slider
    public void setEffectsVolume(float effectsVolume)
    {
        mixer.SetFloat("EffectsVolume", Mathf.Log10(effectsVolume) * 20);
    }

    //Toggle Fullscreen Checkbox
    public void setFullscreen (bool isFullscreen)
    {
        
        Screen.fullScreen = isFullscreen;
    }
    
    //Mute Menu Music Checkbox
    public void muteMenuMusic(bool isPlaying)
    {
        if(!isPlaying)
        {
            FindObjectOfType<AudioManager>().ChangeVolume("MenuTheme", 1.0f);
        }
        if(isPlaying)
        {
            FindObjectOfType<AudioManager>().ChangeVolume("MenuTheme", 0.0f);
        }
    }

}
