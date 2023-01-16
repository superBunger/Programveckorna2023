using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speedboost : MonoBehaviour
{
    GameObject player;
    PlayerMovement pm;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            pm.playerSpeed = 600;
            Destroy(gameObject);

            
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
        pm = player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
