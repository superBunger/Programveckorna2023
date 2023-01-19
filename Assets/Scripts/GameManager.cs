using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public AudioManager audioManager;
    public SaveData saveManager;
    public LevelLoader levelManager;
    public SettingsMenu settings;

    private static GameManager _instance;

    public static GameManager Instance 
    { 
        get
        { 
            return _instance; 
        } 
    }

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(Instance.gameObject);
            _instance = this;
        }
        else
        {
            _instance = this;
        }
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        levelManager.LoadSceneActions();
        if(PlayerPrefs.HasKey("gameHasSaved"))
        {
            saveManager.ApplySettings();
        }   
    }
}