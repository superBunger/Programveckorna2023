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

    //Laddar alla settings för musik och om meny musiken är mute-ad - erik
    public void ApplySettings()
    {
        //Slider Values
        masterSlider.value = PlayerPrefs.GetFloat("MasterVolumeSliderValue");
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolumeSliderValue");
        effectsSlider.value = PlayerPrefs.GetFloat("EffectsVolumeSliderValue");
        //Volume Values
        settingsScript.mixer.SetFloat("MasterVolume", Mathf.Log10(PlayerPrefs.GetFloat("MasterVolume")) * 20);
        settingsScript.mixer.SetFloat("MusicVolume", Mathf.Log10(PlayerPrefs.GetFloat("MusicVolume")) * 20);
        settingsScript.mixer.SetFloat("EffectsVolume", Mathf.Log10(PlayerPrefs.GetFloat("EffectsVolume")) * 20);
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

    //Sparar alla inställningar - erik
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
