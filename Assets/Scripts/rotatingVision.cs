using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatingVision : MonoBehaviour
{
    float rotation;
    bool peepin = false;
    Transform target;

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
            transform.right = target.position - transform.position; //If it does it locks onto the player
        }
        
        }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            print("FUCKING THING IN SIGHT SHOOT IT DOWN!");
            peepin = true;
            //Sends a message to the console and sets the bool to have the enemy lock on to the player
        }
    }
}