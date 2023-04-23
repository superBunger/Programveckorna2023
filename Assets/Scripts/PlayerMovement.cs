using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public SpriteRenderer sr;
    public BoxCollider2D box2d;
    public GameObject playerLight;

    public GameObject keycard;
    public Rigidbody2D rb; //för movement
    float playerSpeed = 350f;
    float boostspeed = 350f;
    float speedDuration = 2.5f;
    public bool speedBoostActive = false;
    

    Vector2 movement;
    Animator animator;
    bool routineStartedRight;
    bool routineStartedLeft;
    bool routineStartedUp;
    bool routineStartedDown;
    public GameObject lockedDoor;

    public energiSystem es;
    public particlesystemscript pss;

    GameObject bomb; //för bomb, interagera med bomb
    public bool insideWall = false;
    public GameObject bombPrefab; //prefab för att spawna bomb
    GameObject wallDestroy;

    ParticleSystem empSystem; //för EMP
    CircleCollider2D cc2D;
    int loopTimer;
   
    public bool smoking; //för rökbomb
    Vector2 homePos = new Vector2(5000, 5000);

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //Referens till rigidbody2D - William
        pss.gameObject.GetComponent<ParticleSystem>().Stop();
        empSystem = GetComponentInChildren<ParticleSystem>();
        cc2D = GetComponentInChildren<CircleCollider2D>();
        cc2D.enabled = false;

        animator = GetComponent<Animator>();

        print(transform.rotation);

        lockedDoor.SetActive(false);
        es.energyBar = PlayerPrefs.GetInt("BatteryCharge");
    }

    void FixedUpdate()
    {
        rb.velocity = playerSpeed * Time.deltaTime * movement.normalized; //Rigidbody2D rörelse - William
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.magnitude > 0)
        {
            FindObjectOfType<AudioManager>().ChangeVolume("PlayerFootsteps", 1.0f);
        }
        else
        {
            FindObjectOfType<AudioManager>().ChangeVolume("PlayerFootsteps", 0.0f);
        }

        //Spelarens input uppdelat i en horisontell och vertikal axel - William
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");


        if (movement.x > 0 && routineStartedRight == false) //Höger
        {
            animator.SetBool("playerRight", true);

        }
        else
        {
            animator.SetBool("playerRight", false);
            
        }

       

        if (movement.x < 0 && routineStartedLeft == false) //Vänster
        {
            animator.SetBool("playerLeft", true);

        }
        else
        {
            animator.SetBool("playerLeft", false);
            
        }


        if (movement.y > 0 && routineStartedUp == false) //Upp
        {
            animator.SetBool("playerButt", true);

        }
        else
        {
            animator.SetBool("playerButt", false);
         
        }

      

        if (movement.y < 0 && routineStartedDown == false) //Ner
        {
            animator.SetBool("playerForward", true);

        }
        else
        {
            animator.SetBool("playerForward", false);
          
        }


        if (Input.GetKeyDown(KeyCode.Alpha2) && speedBoostActive == false && es.energyBar >= 2)
        {
            FindObjectOfType<AudioManager>().Play("BatteryDischarge");
            playerSpeed += boostspeed;
            StartCoroutine(speedBoostPower());
            speedBoostActive = true;
            es.energyBar -= 2; //om man har nog med energi och trycker på knappen blir man snabbare - max

        }

        IEnumerator speedBoostPower()
        {
            yield return new WaitForSeconds(speedDuration);
            playerSpeed -= boostspeed;
            yield return new WaitForSeconds(1);
            speedBoostActive = false; //den här timern väntar 2.5s för att ta bort farten och sen en till sekund innan man kan använda speed boost igen. - max
        }

        if(Input.GetKeyDown(KeyCode.Alpha3) && es.energyBar >= 3 && smoking == false)
        {
            FindObjectOfType<AudioManager>().Play("BatteryDischarge");
            es.energyBar -= 3;
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

        if (Input.GetKeyDown(KeyCode.Alpha4) && es.energyBar >= 4)
        {
            FindObjectOfType<AudioManager>().Play("BatteryDischarge");
            es.energyBar -= 4;
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
            cc2D.radius = 0.125f;
            cc2D.enabled = false; //hitboxen blir lite större istället för att på direkten blir full storlek, som pulsen - max
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && es.energyBar == 1 && insideWall == true)
        {
            FindObjectOfType<AudioManager>().Play("BatteryDischarge");
            bomb = Instantiate(bombPrefab, transform.position, transform.rotation);
            es.energyBar -= 1;
            bomb.GetComponent<Animator>().SetTrigger("bombTime");
            StartCoroutine(bombTimer());
        }
        
        IEnumerator bombTimer()
        {
            yield return new WaitForSeconds(0.85f);
            Destroy(bomb);
            wallDestroy = FindObjectOfType<BreakWall>().gameObject;
            Destroy(wallDestroy);
        }        
    }
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Juggernaut")
        {
            //Destroy(gameObject);
        }
        if (collision.gameObject.tag == "enemy")
        {
            es.energyBar -= 1; //om man rör hunden förlorar man energi - max
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
            sr.enabled = false;
            box2d.enabled = false;
            rb.simulated = false;
            playerLight.SetActive(false);
            StartCoroutine(FindObjectOfType<LevelLoader>().GameOver(5.0f));
            StartCoroutine(FindObjectOfType<AudioManager>().StopMusicCoroutine());
            StartCoroutine(RestartLevel(5.0f));

            // disable spriterenderer
            // disable box collider
            // trigger gameover animations
            // start coroutine
        }

        if (collision.gameObject.tag == "breakableWall")
        {
            insideWall = true;
        }

        if (collision.gameObject.tag == "Keycard")
        {
            FindObjectOfType<AudioManager>().Play("KeycardPickup");
            es.hasKey = true;
            Destroy(keycard);
            
        }
        if (collision.gameObject.tag == "Door" && es.hasKey == false) //Visar "This door is locked" när spelaren nuddar dörren och saknar nyckeln - William
        {
            print("This door is locked");
            lockedDoor.SetActive(true);
        }
        if (collision.gameObject.tag == "Door" && es.hasKey == true)
        {
            lockedDoor.SetActive(false);
            print("go to next level");

            PlayerPrefs.SetInt("BatteryCharge", es.energyBar);
            FindObjectOfType<LevelLoader>().LoadNextLevel();
        }

      

    }

    public IEnumerator RestartLevel(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        //es.energyBar = 0;
        print("loaded next scene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "breakableWall")
        {
            insideWall = false;
        }

        if (collision.gameObject.tag == "Door") //Gömmer "This door is locked" texten när spelarens slutar nudda dörren - William
        {
            
            lockedDoor.SetActive(false);
        }
    }
}
