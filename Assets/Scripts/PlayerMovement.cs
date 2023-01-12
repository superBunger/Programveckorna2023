using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public float playerSpeed = 1f;
    Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //Referens till rigidbody2D
    }

    // Update is called once per frame
    void Update()
    {
        //Spelarens input uppdelat i en horisontell och vertikal axel
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        //Spelarens hastighet som beror på playerSpeed och movement
        rb.velocity = playerSpeed * Time.deltaTime * movement;
    }
}
