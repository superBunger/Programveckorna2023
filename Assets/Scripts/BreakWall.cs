using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakWall : MonoBehaviour

    
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Henry
        //Om vägen rör juggernauten så exploderar den
        if (collision.gameObject.tag == "Juggernaut")
        {
            Destroy(this.gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bomb")
        {
            print("awesome");
            Destroy(this.gameObject);
        }
    }

}
