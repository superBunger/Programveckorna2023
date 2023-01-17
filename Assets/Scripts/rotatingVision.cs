using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatingVision : MonoBehaviour
{
    [SerializeField]
    
    float rotation;
    public bool peepin = false;
    Transform target;



    public particlesystemscript pss;

    private void Start()
        {

        
        target = GameObject.FindWithTag("Player").transform; //States what the player character is

        
        }
    // Update is called once per frame
    void Update()
    {
        if (peepin == false) //If statement for if the enemy has the player in its vision cone
        {
            transform.Rotate(0, 0, rotation + 100 * Time.deltaTime); //If it doesn't it rotates infinitely
        }

        else
        {
            transform.up = target.position - transform.position; //If it does it locks onto the player
        }

        if (pss.insideSmoke == true)
        {
            peepin = false; //slutar f�lja dig om du g�mmer dig - max
        }
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
