using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatingVision : MonoBehaviour
{
    //Henry jobbade p� ugglans rotation och syn.
    //Max s�g till att den inte s�g spelaren n�r den borde inte kunna se de.
    [SerializeField]
    public float rotation;
    public bool peepin = false;
    Transform target;
    public bool setRotation;
    public Quaternion minAngles;
    public Quaternion maxAngles;
    public bool whipIt;
    bool energyTimerStarted = false;
    
    public particlesystemscript pss;
    public energiSystem es;

    private void Start()
        {
         target = GameObject.FindWithTag("Player").transform; //States what the player character is
        }
    // Update is called once per frame
    void Update()
    {
        if (peepin == false && setRotation == false) //If statement for if the enemy has the player in its vision cone
        {
            transform.Rotate(0, 0, rotation * Time.deltaTime); //If it doesn't it rotates infinitely
        }

        else if (peepin == false && setRotation == true && whipIt == false)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, minAngles, Time.deltaTime * rotation);
            
            
            if (transform.rotation == minAngles)
            {
                whipIt = true;
            }
        }

        else if (peepin == false && setRotation == true && whipIt == true)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, maxAngles, Time.deltaTime * rotation);
           
            if (transform.rotation == maxAngles)
            {
                whipIt = false;
            }
        }

        else
        {
            transform.up = target.position - transform.position; //If it does it locks onto the player
        }

        if (pss.insideSmoke == true)
        {
            peepin = false; //slutar f�lja dig om du g�mmer inuti r�k - max
        }

        if(peepin == true && energyTimerStarted == false)
        {
            StartCoroutine(energyTimer()); //om man st�r inuti visionen b�rjar en timer f�r att ta bort energi - max
        }
        if(peepin == false)
        {
            StopAllCoroutines(); //st�nger av timern om man l�mnar visionen
        }
    }

    IEnumerator energyTimer()
    {
        energyTimerStarted = true;
        yield return new WaitForSeconds(5);
        es.energyBar -= 1;
        energyTimerStarted = false; //energytimerstarted anv�nds s� att bara en timer startas �t g�ngen - max
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player" && pss.insideSmoke == false)
        {
            peepin = true; //om man inte �r g�md och fienden ser dig kommer den f�lja dig med blicken //max och henry
        }

    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            peepin = false; //stops following you if you leave vision
            
        }
    }
}
