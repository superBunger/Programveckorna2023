using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class visionConeChangeColour : MonoBehaviour
{

    Color redVision = new Color(0.8773585f, 0.2267888f, 0.2553718f, 0.3f);
    Color yellowVision = new Color(1f, 0.9592047f, 0.2962264f, 0.3f);
    SpriteRenderer sr;
    enemyeyesight ees;
    GameObject enemy; 

    bool routineStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        enemy = FindObjectOfType<enemyeyesight>().gameObject;
        ees = enemy.GetComponent<enemyeyesight>();

    }

    // Update is called once per frame
    void Update()
    {
        if (ees.detected == false)
        {
            if (routineStarted == false) //routine started anv�nds f�r att se till att bara en routine �r active �t g�ngen - max och erik
            {
                StartCoroutine(redVisionTimer()); //startar timern f�r att �ndra tillbaka f�rgen om man inte st�r i synen - m, e
            }
            
        }
        
        if (ees.detected == true)
        {
            StopAllCoroutines();  //stoppar timern om man g�r in i grejen igen - m, e
            routineStarted = false; //�ndra routinestarted s� att en ny routine f�r b�rja - m, e
            sr.color = redVision; //�ndrar f�rg till r�d - m, e
        }
    }

    IEnumerator redVisionTimer() //timer f�r att �ndra f�rg tillbaka till gul - m, e
    {
        routineStarted = true; 
        //print("coroutine started");
        yield return new WaitForSeconds(3);
        ees.isAlarming = false;
        sr.color = yellowVision;
        routineStarted = false;
       
    }


}
