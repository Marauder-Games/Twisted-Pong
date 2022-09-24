using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallMovement : MonoBehaviour
{

    public int moveSpeed = 6;
    public float speedLimiter = 0.7f;
    float inputVertical;
    public Rigidbody2D rb;

    // Timer Stuff
    public float timeRemaining = 10;
    public float paddleScore = 0;
    public float ballScore = 0;
    public bool timerIsRunning = false;
    public Text timeText;

    void Start()
    {
        rb.AddForce(transform.right * moveSpeed);
        timerIsRunning = true;
    }
    // Update is called once per frame
    void Update()
    {
        // Input
        inputVertical = Input.GetAxisRaw("Fire1");

        // Timer
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 30;
                paddleScore += 1;
                transform.localPosition = new Vector3(-6, 0, 0);
                GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                rb.AddForce(transform.right * moveSpeed);
            }
        }
    }

    void FixedUpdate() 
    {
        // Movement
        if (inputVertical > 0)
        {
            rb.AddForce(transform.up * (moveSpeed + 50));
            rb.AddForce(-transform.up * (moveSpeed + 48));
        }
        else if (inputVertical < 0)
        {
            rb.AddForce(-transform.up * (moveSpeed + 50));
            rb.AddForce(transform.up * (moveSpeed + 48));
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Goals"))
        {
            transform.localPosition = new Vector3(-6, 0, 0);
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            rb.AddForce(transform.right * moveSpeed);
            ballScore += 1;
            timeRemaining = 30;
        }
        else
        {
            if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Paddle1"))
            {
                rb.AddForce(transform.right * (int)(moveSpeed * 0.05));
            }
            else if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Paddle2"))
            {
                rb.AddForce(-transform.right * (int)(moveSpeed * 0.05));
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("Time: {0:00}:{1:00} Ball {2}     Paddles {3}", minutes, seconds, ballScore, paddleScore);
    }
}
