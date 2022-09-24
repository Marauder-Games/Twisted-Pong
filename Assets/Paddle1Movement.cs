using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle1Movement : MonoBehaviour
{

    public float moveSpeed = 3f;
    public float speedLimiter = 0.7f;
    float inputVertical;
    public Rigidbody2D rb;

    // Update is called once per frame
    void Update()
    {
        // Input
        inputVertical = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate() 
    {
        // Movement
        if (inputVertical != 0)
        {
            rb.velocity = new Vector2(0f, inputVertical * moveSpeed);
        }
        else
        {
            rb.velocity = new Vector2(0f, 0f);
        }
    }
}
