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

    [System.Serializable]
    public class GameData
    {
        public float masterVolume;
        public float masterVolumeSliderValue;
        public float musicVolume;
        public float musicVolumeSliderValue;
        public float effectsVolume;
        public float effectsVolumeSliderValue;
        public int hasMenuThemeMuted;
        public int isGameFullscreen;
        public int furthestSceneReached;
    } 
    public GameData gameDataClass = new GameData();

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            ApplySettings();
        }
    }

    public void ApplySettings()
    {
        masterSlider.value = PlayerPrefs.GetFloat("gameMasterVolumeSliderValue");
        musicSlider.value = PlayerPrefs.GetFloat("gameMusicVolumeSliderValue");
        effectsSlider.value = PlayerPrefs.GetFloat("gameEffectsVolumeSliderValue");

        settingsScript.mixer.SetFloat("MasterVolume", PlayerPrefs.GetFloat("gameMasterVolume"));
        settingsScript.mixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("gameMusicVolume"));
        settingsScript.mixer.SetFloat("EffectsVolume", PlayerPrefs.GetFloat("gameEffectsVolume"));

        if(PlayerPrefs.GetInt("gameIsFullscreen") == 1)
        {
            Screen.fullScreen = true;
            fullscreenToggle.isOn = true;
        }
        else
        {
            Screen.fullScreen = false;
            fullscreenToggle.isOn = false;
        }
        
        if(PlayerPrefs.GetInt("gameMenuThemeMuted") == 1)
        {
            muteThemeToggle.isOn = true;
            FindObjectOfType<AudioManager>().ChangeVolume("MenuTheme", 0.0f);
            
        }
        else
        {
            muteThemeToggle.isOn = false;
            FindObjectOfType<AudioManager>().ChangeVolume("MenuTheme", 1.0f);
        }

    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("gameMasterVolumeSliderValue", masterSlider.value);
        PlayerPrefs.SetFloat("gameMusicVolumeSliderValue", musicSlider.value);
        PlayerPrefs.SetFloat("gameEffectsVolumeSliderValue", effectsSlider.value);

        PlayerPrefs.SetFloat("gameMasterVolume", gameDataClass.masterVolume);
        PlayerPrefs.SetFloat("gameMusicVolume", gameDataClass.musicVolume);
        PlayerPrefs.SetFloat("gameEffectsVolume", gameDataClass.effectsVolume);

        //PlayerPrefs.SetInt("gameIsFullscreen", gameDataClass.isGameFullscreen);
        //PlayerPrefs.SetInt("gameMenuThemeMuted", gameDataClass.hasMenuThemeMuted);

        PlayerPrefs.SetInt("gameFurthestSceneReached", gameDataClass.furthestSceneReached);
        PlayerPrefs.Save();   
    }

}
