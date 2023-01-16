using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public float playerSpeed = 200f;
    public float boostspeed = 100f;
    public float coolDown = 1f;
    public float duration = 1f;
    float timer = 0f;
    Vector2 movement;

    public energiSystem es;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //Referens till rigidbody2D
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.velocity.magnitude > 0)
        {
            FindObjectOfType<AudioManager>().ChangeVolume("PlayerFootsteps", 1.0f);
        }
        else
        {
            FindObjectOfType<AudioManager>().ChangeVolume("PlayerFootsteps", 0.0f);
        }

        //Spelarens input uppdelat i en horisontell och vertikal axel
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        timer += Time.deltaTime;
        if (timer > coolDown && Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerSpeed += boostspeed;
            timer = 0;
            if (timer > duration)
            {
                playerSpeed -= boostspeed;
                timer = 0;
            }
        }
    }
    
    void FixedUpdate()
    {
        //Spelarens hastighet som beror på playerSpeed och movement
        rb.velocity = playerSpeed * Time.deltaTime * movement.normalized;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Juggernaut")
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "battery")
        {
            es.energyBar += 1;
            Destroy(collision.gameObject); //lägger till en energi och förstår batteriet när man rör det - max 
        }
    }
}
