using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SaveData : MonoBehaviour
{
    public SettingsMenu settingsScript;

    public Slider masterSlider;
    public Slider musicSlider;
    public Slider effectsSlider;
    public Toggle fullscreenToggle;
    public Toggle muteThemeToggle;

    public void ApplySettings()
    {
        //Slider Values
        masterSlider.value = PlayerPrefs.GetFloat("MasterVolumeSliderValue");
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolumeSliderValue");
        effectsSlider.value = PlayerPrefs.GetFloat("EffectsVolumeSliderValue");
        //Volume Values
        settingsScript.mixer.SetFloat("MasterVolume", PlayerPrefs.GetFloat("MasterVolume"));
        settingsScript.mixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume"));
        settingsScript.mixer.SetFloat("EffectsVolume", PlayerPrefs.GetFloat("EffectsVolume"));
        //Menu Mute
        if(PlayerPrefs.GetInt("MenuThemeMuted") == 1)
        {
            muteThemeToggle.isOn = false;
            settingsScript.isThemePlaying = false;
        }
        else
        {
            muteThemeToggle.isOn = true;
            settingsScript.isThemePlaying = true;
        }
    }

    public void SaveSettings()
    {
        //Slider Values
        PlayerPrefs.SetFloat("MasterVolumeSliderValue", masterSlider.value);
        PlayerPrefs.SetFloat("MusicVolumeSliderValue", musicSlider.value);
        PlayerPrefs.SetFloat("EffectsVolumeSliderValue", effectsSlider.value);
        
        PlayerPrefs.SetInt("gameHasSaved", 1);
        PlayerPrefs.Save();   
    }

}
