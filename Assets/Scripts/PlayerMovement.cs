using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    float playerSpeed = 350f;
    float boostspeed = 350f;
    float speedDuration = 2.5f;
    public bool speedBoostActive = false;
   
    Vector2 movement;

    public energiSystem es;
    public particlesystemscript pss;

    public GameObject bomb;
    GameObject bombThing; //för att interagera med den;

    ParticleSystem empSystem;
    CircleCollider2D cc2D;
   
    public bool smoking; //kollar om den röker - m
    Vector2 homePos = new Vector2(5000, 5000);

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //Referens till rigidbody2D
        ParticleSystem smokerSystem = pss.gameObject.GetComponent<ParticleSystem>();
        smokerSystem.Stop();
        empSystem = GetComponentInChildren<ParticleSystem>();
        cc2D = GetComponentInChildren<CircleCollider2D>();
        cc2D.enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Spelarens input uppdelat i en horisontell och vertikal axel
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        rb.velocity = playerSpeed * Time.deltaTime * movement.normalized;
       
        if (Input.GetKeyDown(KeyCode.Alpha1) && speedBoostActive == false && es.energyBar >= 1)
        {
            playerSpeed += boostspeed;
            FindObjectOfType<AudioManager>().Play("BatteryDischarge");
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
            es.energyBar -= 2;
            pss.gameObject.transform.position = transform.position;
            ParticleSystem smokerSystem = pss.gameObject.GetComponent<ParticleSystem>();
            smokerSystem.Play();
            StartCoroutine(smokeBombTimer());
            smoking = true; //gör en rök bomb om man trycker 2 (flyttar den till dig och sen tillbaka bort) - max
            
        }

        IEnumerator smokeBombTimer()
        {
            yield return new WaitForSeconds(10);
            pss.gameObject.GetComponent<ParticleSystem>().Stop();
            yield return new WaitForSeconds(8);
            pss.gameObject.transform.position = homePos;
            smoking = false; //gör så att man kan lägga en ny smokebomb
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && es.energyBar >= 3)
        {
            es.energyBar -= 3;
            empSystem.Play();
            cc2D.enabled = true;
            StartCoroutine(empHitBox()); //skickar en emp om man trycker på 3
        }

        IEnumerator empHitBox()
        {
            cc2D.radius += 0.3f;
            yield return new WaitForSeconds(0.1f);
            cc2D.radius += 0.3f;
            yield return new WaitForSeconds(0.1f);
            cc2D.radius += 0.3f;
            yield return new WaitForSeconds(0.1f);
            cc2D.radius += 0.3f;
            yield return new WaitForSeconds(0.1f);
            cc2D.radius += 0.3f;
            yield return new WaitForSeconds(0.1f);
            cc2D.radius += 0.3f;
            yield return new WaitForSeconds(0.1f);
            cc2D.radius += 0.3f;
            yield return new WaitForSeconds(0.1f);
            cc2D.radius += 0.3f;
            yield return new WaitForSeconds(0.1f);
            cc2D.radius += 0.3f;
            yield return new WaitForSeconds(0.1f);
            cc2D.radius += 0.3f;
            yield return new WaitForSeconds(0.1f);
            cc2D.radius += 0.3f;
            yield return new WaitForSeconds(0.5f);
            cc2D.radius = 0.5f;
            cc2D.enabled = false; //dålig kod jag vet men hitboxen blir lite större istället för att på direkten blir full storlek, som pulsen - max
        }

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

        else if (collision.gameObject.tag == "Breakable wall" && es.energyBar == 4 && Input.GetKeyDown(KeyCode.Alpha4))
        {
            
            bombThing.GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(bombTimer());
            es.energyBar -= 4;

        }
    }

    IEnumerator bombTimer()
    {
        yield return new WaitForSeconds(1);
        bombThing.GetComponent<BoxCollider2D>().enabled = true;
        bombThing.GetComponent<BoxCollider2D>().enabled = false;
    }

}
