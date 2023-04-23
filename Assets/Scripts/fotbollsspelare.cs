using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class fotbollsspelare : MonoBehaviour
{
    public SpriteRenderer sr;
    public BoxCollider2D box2d;

    public Rigidbody2D rb; //för movement
    float playerSpeed = 350f;

    Vector2 dir;
    Animator animator;
    bool routineStartedRight;
    bool routineStartedLeft;
    bool routineStartedUp;
    bool routineStartedDown;

    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //Referens till rigidbody2D - William

        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        rb.velocity = playerSpeed * Time.deltaTime * dir; //Rigidbody2D rörelse - William
    }

    // Update is called once per frame
    void Update()
    {

        //Spelarens input uppdelat i en horisontell och vertikal axel - William
       if(Input.GetKeyDown(up))
       {
            dir.y = 1;
       }
       if (Input.GetKeyDown(down))
       {
            dir.y = -1;
       }
       if (Input.GetKeyDown(left))
       {
            dir.x = -1;
       }
       if (Input.GetKeyDown(right))
       {
            dir.x = 1;
       }
       if(Input.GetKeyUp(up) || Input.GetKeyUp(down))
        {
            dir.y = 0;
        }
       if(Input.GetKeyUp(left)||Input.GetKeyUp(right))
        {
            dir.x = 0;
        }


        if (dir.x > 0 && routineStartedRight == false) //Höger
        {
            animator.SetBool("playerRight", true);

        }
        else
        {
            animator.SetBool("playerRight", false);

        }


        if (dir.x < 0 && routineStartedLeft == false) //Vänster
        {
            animator.SetBool("playerLeft", true);

        }
        else
        {
            animator.SetBool("playerLeft", false);

        }


        if (dir.y > 0 && routineStartedUp == false) //Upp
        {
            animator.SetBool("playerButt", true);

        }
        else
        {
            animator.SetBool("playerButt", false);

        }



        if (dir.y < 0 && routineStartedDown == false) //Ner
        {
            animator.SetBool("playerForward", true);

        }
        else
        {
            animator.SetBool("playerForward", false);

        }
    }
}
