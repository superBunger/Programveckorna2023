using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    void Awake()
	{
        print("print awake " + SceneManager.GetActiveScene().buildIndex);
        DontDestroyOnLoad(gameObject);
	}

    public void OnEnable()
    {
        print("print on enable " + SceneManager.GetActiveScene().buildIndex);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        print("print on scene loaded " + SceneManager.GetActiveScene().buildIndex);
    }

    public void ChangeSceneAudio()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
        FindObjectOfType<AudioManager>().Play("MenuTheme");
        }
        
        else if(SceneManager.GetActiveScene().buildIndex == 1)
        {
        FindObjectOfType<AudioManager>().Play("Ambience");
        FindObjectOfType<AudioManager>().Play("AmbienceDetected");
        FindObjectOfType<AudioManager>().Play("PlayerFootsteps");
        }
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadNextScene(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public IEnumerator LoadNextScene(int levelIndex)
    {
        yield return new WaitForSeconds(1);
        transition.SetTrigger("TransitionStart");
        SceneManager.LoadScene(levelIndex);
        yield return new WaitForSeconds(1);
        ChangeSceneAudio();
    }

}
