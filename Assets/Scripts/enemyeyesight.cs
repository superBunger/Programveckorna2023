using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyeyesight : MonoBehaviour
{
    public FieldOfView raycastScript;
    public pathfinding juggernaut;

    public bool detected = false;
    public bool disabled;

    public bool isAlarming = false; //Is it playing the alarm?
    public bool isDetected = false; //Is it playing the "Ambience" ambience?
    bool isChangingToNormal = false; //Is it trying to change from detected to normal?
    bool isChangingToDetected = false; //Is it trying to change from normal to detected?

    public particlesystemscript pss;
    public UnityEngine.Rendering.Universal.Light2D lightCone;

    private void Start()
    {
        lightCone = GetComponentInChildren<UnityEngine.Rendering.Universal.Light2D>();
        disabled = false;
    }

    Coroutine changeToNormal;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("emp"))
        {
            disabled = true;
            StartCoroutine(disabledTimer()); //stänger av vision om man blir stunned - max
        }
    }
 

    
    void Update()
    {
        
        if (detected == false && isDetected == true && isChangingToNormal == false)
        {
            changeToNormal = StartCoroutine(ChangeAmbienceNormalCooldown(10.0f)); //Byter till vanliga ambience om 10 sekunder - erik
            isChangingToNormal = true; //Bool för att visa att den är mitt i ett byte - erik
        }

        if (detected == true && isDetected == true)
        {
            StopCoroutine(changeToNormal);//Avslutar bytet när man blir upptäckt - erik
        }

        if (pss.insideSmoke == true || disabled == true)
        {
            detected = false; //om man stänger av fienden eller gömmer sig i rök, slutar man vara detected - max
            juggernaut.othersSee = 0;
        }

        if (disabled == true)
        {
            lightCone.enabled = false;
            
        }
        else
        {
            lightCone.enabled = true; //ändrar visionen av och på beroende på om fienden är stunned - max
        }


        if (raycastScript.CanSeePlayer == true && pss.insideSmoke == false && disabled == false)
        {
           
            detected = true;  //om man blir sedd blir detected sann - max och erik
            isChangingToNormal = false;
            

            if (isChangingToDetected == false)
            {
                StartCoroutine(ChangeAmbienceDetectedCooldown(1.75f));
                isChangingToDetected = true;
                //När man blir upptäckt så byter den till detected ambience om 1.75 sekunder - erik
            }

            if (isAlarming == false)
            {
                isAlarming = true;
                FindObjectOfType<AudioManager>().Play("DetectionAlarm");
                //Om det inte spelas ett aktivt alarm så spelas det ett när man blir upptäckt - erik
            }

        }

        if(raycastScript.CanSeePlayer == false)
        {
            
            detected = false;
            
        }
    }

  

    public IEnumerator ChangeAmbienceDetectedCooldown(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        FindObjectOfType<AudioManager>().ChangeAmbienceDetected();
        //En Coroutine som byter till detected ambience - erik

    }

    public IEnumerator ChangeAmbienceNormalCooldown(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        FindObjectOfType<AudioManager>().ChangeAmbienceNormal();
        isDetected = false; 
        isChangingToDetected = false;
        //En Coroutine som byter till normal ambience - erik
    }

    IEnumerator disabledTimer()
    {
        yield return new WaitForSeconds(5); //sätter på vision efter 3 sekunder - max
        disabled = false;
    }
}
