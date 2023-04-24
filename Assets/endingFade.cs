using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endingFade : MonoBehaviour
{
    public energiSystem es;
    [SerializeField] public CanvasGroup myUIGroup;
    public float Fade;
    public LevelLoader levelLoaderScript;
    private void Update()
    {
        if (es.hasKey)
        { 
         myUIGroup.alpha += Time.deltaTime;
        }

        if (myUIGroup.alpha >= 1) 
        {
            levelLoaderScript.LoadNextScene(0);
        }
    }
}
