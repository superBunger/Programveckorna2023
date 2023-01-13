using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class visionConeChangeColour : MonoBehaviour
{
    Color redVision = new Color(0.8773585f, 0.2267888f, 0.2553718f, 0.972549f);
    SpriteRenderer sr;
    enemyeyesight ees;
    GameObject enemy;

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
        if(ees.detected == true)
        {
            print("awesome");
            sr.material.SetColor("_color", redVision);
        }
    }
}
