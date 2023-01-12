using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyeyesight : MonoBehaviour
{
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            print("player detected"); //om fienden ser en spelare kommer den säga det - max
            
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
