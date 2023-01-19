using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb; //för movement
    float playerSpeed = 350f;
    float boostspeed = 350f;
    float speedDuration = 2.5f;
    public bool speedBoostActive = false;
    Vector2 movement;

    public energiSystem es;
    public particlesystemscript pss;

    GameObject bomb; //för bomb, interagera med bomb
    public bool insideWall = false;
    public GameObject bombPrefab; //prefab för att spawna bomb

    ParticleSystem empSystem; //för EMP
    CircleCollider2D cc2D;
    int loopTimer;
   
    public bool smoking; //för rökbomb
    Vector2 homePos = new Vector2(5000, 5000);

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //Referens till rigidbody2D
        pss.gameObject.GetComponent<ParticleSystem>().Stop();
        empSystem = GetComponentInChildren<ParticleSystem>();
        cc2D = GetComponentInChildren<CircleCollider2D>();
        cc2D.enabled = false;
        
       
    }

    void FixedUpdate()
    {
        rb.velocity = playerSpeed * Time.deltaTime * movement.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        //Spelarens input uppdelat i en horisontell och vertikal axel
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

       
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
            loopTimer = 10;
            StartCoroutine(empHitBox()); //skickar en emp om man trycker på 3
        }

        IEnumerator empHitBox()
        {
            while(loopTimer >= 0)
            {
                cc2D.radius += 0.2f;
                yield return new WaitForSeconds(0.1f);
                loopTimer -= 1;
            }
            cc2D.radius += 0.3f;
            yield return new WaitForSeconds(0.5f);
            cc2D.radius = 0.5f;
            cc2D.enabled = false; //hitboxen blir lite större istället för att på direkten blir full storlek, som pulsen - max
        }

        if (Input.GetKeyDown(KeyCode.Alpha4) && es.energyBar == 4 && insideWall == true)
        {
            bomb = Instantiate(bombPrefab, transform.position, transform.rotation);
            bomb.GetComponent<PolygonCollider2D>().enabled = false;
            es.energyBar -= 4;
            bomb.GetComponent<Animator>().SetTrigger("bombTime");
            StartCoroutine(bombTimer());
        }
        
        IEnumerator bombTimer()
        {
            yield return new WaitForSeconds(1);
            bomb.GetComponent<PolygonCollider2D>().enabled = true;
            yield return new WaitForSeconds(1);
            Destroy(bomb);
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

        if (collision.gameObject.tag == "Juggernaut")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "breakableWall")
        {
            insideWall = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "breakableWall")
        {
            insideWall = false;
        }
    }
}
