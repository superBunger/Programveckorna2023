using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyeyesight : MonoBehaviour
{
    public pathfinding juggernaut;

    public bool detected = false;


    public bool isAlarming = false; //Is it playing the alarm?
    public bool isDetected = false; //Is it playing the "Ambience" ambience?
    bool isChangingToNormal = false; //Is it trying to change from detected to normal?
    bool isChangingToDetected = false; //Is it trying to change from normal to detected?

    public particlesystemscript pss;

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

    }

    private void Start()
    {
       
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
        if (collision.gameObject.tag == "Player")
        {

            if (pss.insideSmoke == false)
            {
                detected = true;  //om man blir sedd blir detected sann - max och erik
                isChangingToNormal = false;
                juggernaut.othersSee += 1;

                if (isChangingToDetected == false)
                {
                    StartCoroutine(ChangeAmbienceDetectedCooldown(5.0f));
                    isChangingToDetected = true;
                }

                if (isAlarming == false)
                {
                    isAlarming = true;
                    FindObjectOfType<AudioManager>().Play("DetectionAlarm");
                }
            }            
        }
    }
}
