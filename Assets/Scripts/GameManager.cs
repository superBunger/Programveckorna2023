using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AudioManager audioManager;
    public SaveData DataManager;
    public LevelLoader LevelManager;
    public SettingsMenu Settings;

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        DataManager.LoadSettings();
    }

}