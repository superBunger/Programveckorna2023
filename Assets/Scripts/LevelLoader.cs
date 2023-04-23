using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader instance;

    public Animator transition;
    public Animator gameOverAnimator;
    public SaveData saveManager;
    public int furthestSceneReached;

    public IEnumerator GameOver(float seconds)
    {
        gameOverAnimator.SetTrigger("GameOver");
        gameOverAnimator.SetBool("isGameOver", true);
        yield return new WaitForSeconds(seconds);
        gameOverAnimator.SetTrigger("doneGamingOver");
        gameOverAnimator.SetBool("isGameOver", false);

    }

    void Start()
	{
        //Startar med funktionen som startar musik - erik
        LoadSceneActions();
	}

    public void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //En void funktion som h�nder varje g�ng man laddas in i en scen  - erik
        //H�r sparar koden den l�ngsta scenen man har n�tt  - erik
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
        //Funktion som laddar musiken f�r spelet scener  - erik
        if (SceneManager.GetActiveScene().buildIndex >= 1 && SceneManager.GetActiveScene().buildIndex < 9)
        {
            FindObjectOfType<AudioManager>().Play("Ambience");
            FindObjectOfType<AudioManager>().Play("AmbienceDetected");
            FindObjectOfType<AudioManager>().Play("PlayerFootsteps");
        }
        else if(SceneManager.GetActiveScene().buildIndex == 9)
        {
            FindObjectOfType<AudioManager>().Play("PlayerFootsteps");
        }
    }

    public void LoadMenuLevel()
    {
        //Funktion som laddar menyn  - erik
        StartCoroutine(LoadNextScene(0));
    }

    public void LoadNextLevel()
    {
        //Funktion som laddar n�sta leveln i build index ordningen  - erik
        StartCoroutine(LoadNextScene(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadFurthestLevel()
    {
        //Laddar den l�ngsta scenen man har n�tt (tar det v�rdet fr�n save systemet - erik
        StartCoroutine(LoadNextScene(SceneManager.GetActiveScene().buildIndex + PlayerPrefs.GetInt("FurthestSceneReached")));
    }

    public IEnumerator LoadNextScene(int levelIndex)
    {
        //B�rjar animationen f�r scene transition
        //Fade-ar ut musiken
        //V�ntar i en sekund
        //Laddar n�sta scen
        //Startar animationen f�r att avsluta scene transition (visa spelet)
        //V�ntar i 0.5 sekunder s� alla scripts och deras funktioner hinner ladda in
        //Startar musiken f�r varje scen
        //Gjort av Erik
        transition.SetTrigger("ClickNewGame");
        FindObjectOfType<AudioManager>().StopMusic();
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(levelIndex);
        transition.SetTrigger("TransitionStart");
        yield return new WaitForSeconds(0.5f);
        LoadSceneActions();
    }

}
