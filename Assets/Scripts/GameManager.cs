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

    void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        levelManager.LoadSceneActions();
        if(PlayerPrefs.HasKey("gameHasSaved"))
        {
            saveManager.ApplySettings();
        }   
    }
}