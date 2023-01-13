using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyeyesight : MonoBehaviour
{

    public bool detected = false;
    public bool colourChangeBack = false;


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && detected == true)
        {
            detected = false; // om man slutar bli sedd blir den falsk, och en kod för att ändra tillbaka färgen börjar - max och erik
            colourChangeBack = true;
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            detected = true; //om man blir sedd blir detected sann - max och erik
        }

       
    

    }


}
