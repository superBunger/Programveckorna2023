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
        //En void funktion som händer varje gång man laddas in i en scen  - erik
        //Här sparar koden den längsta scenen man har nått  - erik
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
        //Funktion som laddar musiken för spelet scener  - erik
        if (SceneManager.GetActiveScene().buildIndex >= 1)
        {
            FindObjectOfType<AudioManager>().Play("Ambience");
            FindObjectOfType<AudioManager>().Play("AmbienceDetected");
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
        //Funktion som laddar nästa leveln i build index ordningen  - erik
        StartCoroutine(LoadNextScene(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadFurthestLevel()
    {
        //Laddar den längsta scenen man har nått (tar det värdet från save systemet - erik
        StartCoroutine(LoadNextScene(SceneManager.GetActiveScene().buildIndex + PlayerPrefs.GetInt("FurthestSceneReached")));
    }

    public IEnumerator LoadNextScene(int levelIndex)
    {
        //Börjar animationen för scene transition
        //Fade-ar ut musiken
        //Väntar i en sekund
        //Laddar nästa scen
        //Startar animationen för att avsluta scene transition (visa spelet)
        //Väntar i 0.5 sekunder så alla scripts och deras funktioner hinner ladda in
        //Startar musiken för varje scen
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
