using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakWall : MonoBehaviour

    
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Henry
        //Om v�gen r�r juggernauten s� exploderar den
        if (collision.gameObject.tag == "Juggernaut")
        {
            Destroy(this.gameObject);
        }
    }

  
}
