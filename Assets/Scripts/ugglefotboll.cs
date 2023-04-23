using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ugglefotboll : MonoBehaviour
{
    Rigidbody2D rb2d;

    Vector2 pos = new Vector2(0, 0.5f);

    int leftGoals = 0;
    int rightGoals = 0;
    TMP_Text goalText;

    void resetBall()
    {
        transform.position = pos;
        goalText = ;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "leftgoal")
        {
            rightGoals++;
            resetBall();
        }

        if (collision.gameObject.tag == "rightgoal")
        {
            leftGoals++;
            resetBall();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        transform.position = pos;
        leftGoals = 0;
        rightGoals = 0;
        goalText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y > -0.5)
        {
            rb2d.gravityScale = 0.05f;
        }
        else
        {
            rb2d.gravityScale = -0.05f;
        }
    }
}
