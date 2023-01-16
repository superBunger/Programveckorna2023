using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SaveData : MonoBehaviour
{
    public SettingsMenu settingsScript;
    string saveFilePath;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        saveFilePath = Application.dataPath + "/gamedata.json";
    }
    
    [System.Serializable]
    public class GameData
    {
        public float masterVolume;
        public float musicVolume;
        public float effectsVolume;
        public bool hasMenuThemeMuted;
        
    }
    public GameData gameDataClass = new GameData();
    
    public void writeFile()
    {
        // Serialize the object into JSON and save string.
        string jsonString = JsonUtility.ToJson(gameDataClass);

        // Write JSON to file.
        File.WriteAllText(saveFilePath, jsonString);
    }

    public void readFile()
    {
        string jsonString = File.ReadAllText(saveFilePath);
        JsonUtility.FromJson<GameData>(jsonString);

    }

    public void UpdateSettings()
    {
        settingsScript.mixer.GetFloat("MasterVolume", out gameDataClass.masterVolume);
        settingsScript.mixer.GetFloat("MusicVolume", out gameDataClass.musicVolume);
        settingsScript.mixer.GetFloat("EffectsVolume", out gameDataClass.effectsVolume);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSettings();
    }
}
