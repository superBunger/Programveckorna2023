using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    void Awake()
	{
        DontDestroyOnLoad(gameObject);
	}

    public Animator transition;

    public void LoadNextLevel()
    {
        StartCoroutine(LoadNextScene(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public IEnumerator LoadNextScene(int levelIndex)
    {
        
        yield return new WaitForSeconds(1);
        transition.SetTrigger("TransitionStart");
        SceneManager.LoadScene(levelIndex);
    }

}
