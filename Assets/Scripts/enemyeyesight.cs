using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyeyesight : MonoBehaviour
{
    public pathfinding juggernaut;

    public bool detected = false;
    public bool disabled = false;

    public bool isAlarming = false; //Is it playing the alarm?
    public bool isDetected = false; //Is it playing the "Ambience" ambience?
    bool isChangingToNormal = false; //Is it trying to change from detected to normal?
    bool isChangingToDetected = false; //Is it trying to change from normal to detected?

    public particlesystemscript pss;
    public UnityEngine.Rendering.Universal.Light2D lightCone;



    Coroutine changeToNormal;
    void Update()
    {
        if (detected == false && isDetected == true && isChangingToNormal == false)
        {
            changeToNormal = StartCoroutine(ChangeAmbienceNormalCooldown(10.0f));
            isChangingToNormal = true;
        }

        if (detected == true && isDetected == true)
        {
            StopCoroutine(changeToNormal);
        }

        if (pss.insideSmoke == true || disabled == true)
        {
            detected = false; //om man stänger av fienden eller gömmer sig i rök, slutar man vara detected - max
        }

        if (disabled == true)
        {
            lightCone.enabled = false;
        }
        else
        {
            lightCone.enabled = true; //ändrar visionen av och på beroende på om fienden är stunned - max
        }
    }

    private void Start()
    {
        lightCone = GetComponentInChildren<UnityEngine.Rendering.Universal.Light2D>();
}

    public IEnumerator ChangeAmbienceDetectedCooldown(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        FindObjectOfType<AudioManager>().ChangeAmbienceDetected();

    }

    public IEnumerator ChangeAmbienceNormalCooldown(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        FindObjectOfType<AudioManager>().ChangeAmbienceNormal();
        isDetected = false;
        isChangingToDetected = false;
    }

    IEnumerator disabledTimer()
    {
        yield return new WaitForSeconds(3); //sätter på vision efter 3 sekunder - max
        disabled = false;
    }
    

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player" && detected == true)
        {
            detected = false; // om man slutar bli sedd blir den falsk, och en kod f�r att �ndra tillbaka f�rgen b�rjar - max och erik
            isDetected = true;
            juggernaut.othersSee -= 1;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && pss.insideSmoke == false && disabled == false)
        {
                detected = true;  //om man blir sedd blir detected sann - max och erik
                isChangingToNormal = false;
                juggernaut.othersSee += 1;

                if (isChangingToDetected == false)
                {
                    StartCoroutine(ChangeAmbienceDetectedCooldown(1.75f));
                    isChangingToDetected = true;
                }

                if (isAlarming == false)
                {
                    isAlarming = true;
                    FindObjectOfType<AudioManager>().Play("DetectionAlarm");
                }
         
        }

        if(collision.gameObject.tag == "emp")
        {
            disabled = true;
            StartCoroutine(disabledTimer()); //stänger av vision om man blir stunned
        }

    }
}
