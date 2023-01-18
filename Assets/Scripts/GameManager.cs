using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AudioManager audioManager;
    public SaveData saveManager;
    public LevelLoader levelManager;
    public SettingsMenu settings;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        saveManager.ApplySettings();
        //saveManager.SaveSettings();
    }

}