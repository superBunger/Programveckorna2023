using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExplodeViolently : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Die");
    }

    IEnumerator Die()
    {
        yield return new WaitForSecondsRealtime(15);
        print("Rotting rn on jah");
        Application.Quit();
    }
}
