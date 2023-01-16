using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    float playerSpeed = 350f;
    float boostspeed = 350f;
    float speedDuration = 2.5f;
    public bool speedBoostActive = false;
   
    Vector2 movement;

    public energiSystem es;

    public GameObject smokeBomb;
    public bool smoking;
    GameObject sBombSmoker;
    

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

       
        if (Input.GetKeyDown(KeyCode.Alpha1) && speedBoostActive == false && es.energyBar >= 1)
        {
            playerSpeed += boostspeed;
            StartCoroutine(speedBoostPower());
            speedBoostActive = true;
            es.energyBar -= 1; //om man har nog med energi och trycker på knappen blir man snabbare - max

        }

        IEnumerator speedBoostPower()
        {
            yield return new WaitForSeconds(speedDuration);
            playerSpeed -= boostspeed;
            yield return new WaitForSeconds(1);
            speedBoostActive = false; //den här timern väntar 2.5s för att ta bort farten och sen en till sekund innan man kan använda speed boost igen. - max
        }

        if(Input.GetKeyDown(KeyCode.Alpha2) && es.energyBar >= 2 && smoking == false)
        {
            es.energyBar = -2;
            sBombSmoker = Instantiate(smokeBomb, transform.position, transform.rotation);
            StartCoroutine(smokeBombTimer());
            smoking = true;
            
        }

        IEnumerator smokeBombTimer()
        {
            yield return new WaitForSeconds(10);
            smoking = false;
            Destroy(sBombSmoker);
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
            FindObjectOfType<AudioManager>().Play("BatteryCharge");
            es.energyBar += 1;
            Destroy(collision.gameObject); //lägger till en energi och förstår batteriet när man rör det - max 
        }

        else if (collision.gameObject.tag == "Juggernaut")
        {
            Destroy(gameObject);
        }
    }
}
