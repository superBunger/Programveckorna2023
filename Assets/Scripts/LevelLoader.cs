using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader instance;

    public Animator transition;
    public SaveData saveManager;
    public int furthestSceneReached;
    
    void Start()
	{
        //LoadSceneActions();
	}

    public void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LoadSceneActions();
        print("Print: on scene loaded " + SceneManager.GetActiveScene().buildIndex);
        if(SceneManager.GetActiveScene().buildIndex > furthestSceneReached)
        {
            furthestSceneReached = SceneManager.GetActiveScene().buildIndex;
            PlayerPrefs.SetInt("FurthestSceneReached", furthestSceneReached);
            PlayerPrefs.Save();
        }
    }

    public void LoadSceneActions()
    {
        if(SceneManager.GetActiveScene().buildIndex >= 1)
        {
            FindObjectOfType<AudioManager>().Play("Ambience");
            FindObjectOfType<AudioManager>().Play("AmbienceDetected");
            FindObjectOfType<AudioManager>().Play("PlayerFootsteps");
        }
    }

    public void LoadMenuLevel()
    {
        StartCoroutine(LoadNextScene(0));
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadNextScene(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadFurthestLevel()
    {
        StartCoroutine(LoadNextScene(SceneManager.GetActiveScene().buildIndex + PlayerPrefs.GetInt("FurthestSceneReached")));
    }

    public IEnumerator LoadNextScene(int levelIndex)
    {
        transition.SetTrigger("ClickNewGame");
        FindObjectOfType<AudioManager>().StopMusic();
        yield return new WaitForSeconds(1.0f);
        transition.SetTrigger("TransitionStart");
        SceneManager.LoadScene(levelIndex);
        yield return new WaitForSeconds(0.5f);
        LoadSceneActions();
    }

}
