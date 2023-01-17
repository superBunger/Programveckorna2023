using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SaveData : MonoBehaviour
{
    public SettingsMenu settingsScript;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        
    }

    [System.Serializable]
    public class GameData
    {
        public float masterVolume;
        public float musicVolume;
        public float effectsVolume;
        public bool hasMenuThemeMuted;
        public bool isGameFullscreen;
    }
    public GameData gameDataClass = new GameData();

    public void LoadSettings()
    {
        settingsScript.setMasterVolume(PlayerPrefs.GetFloat("gameMasterVolume"));
        settingsScript.setMusicVolume(PlayerPrefs.GetFloat("gameMasterVolume"));
        settingsScript.setEffectsVolume(PlayerPrefs.GetFloat("gameMasterVolume"));
        settingsScript.setFullscreen(PlayerPrefs.GetInt("gameIsFullscreen") == 1 ? true : false);
        settingsScript.setFullscreen(PlayerPrefs.GetInt("gameMenuThemeMuted") == 1 ? true : false);
    }

    public void UpdateSettings()
    {
        settingsScript.mixer.GetFloat("MasterVolume", out gameDataClass.masterVolume);
        settingsScript.mixer.GetFloat("MusicVolume", out gameDataClass.musicVolume);
        settingsScript.mixer.GetFloat("EffectsVolume", out gameDataClass.effectsVolume);

        PlayerPrefs.SetFloat("gameMasterVolume", gameDataClass.masterVolume);
        PlayerPrefs.SetFloat("gameMusicVolume", gameDataClass.musicVolume);
        PlayerPrefs.SetFloat("gameEffectsVolume", gameDataClass.effectsVolume);
        PlayerPrefs.SetInt("gameIsFullscreen", gameDataClass.isGameFullscreen ? 1 : 0);
        PlayerPrefs.SetInt("gameMenuThemeMuted", gameDataClass.hasMenuThemeMuted ? 1 : 0);
        PlayerPrefs.Save();
    }

    void Update()
    {

    }
}
