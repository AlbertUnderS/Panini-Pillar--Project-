using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerControllerMENU : MonoBehaviour
{

    public int direction = 0;
    [SerializeField] public float speed;
    [SerializeField] private float JumpHeight;
    public int jumpsLeft = 0;
    private Rigidbody2D rigidbody;
    private bool isGrounded;
    float originSpeed;

    Animator animator;

    public Vector3 teleportLocation = new Vector3(-5, -3.6f);

    public float pickupRange = 1.5f;  
    public Vector2 carryOffset; 
    public KeyCode carryKey = KeyCode.C; 
    private Transform objectToCarry;  
    private bool isCarrying = false;  

    private Vector3 originalScale;

    public int score;

    public TextMeshProUGUI scoreText;


    // Start is called before the first frame update
    void Start()
    {

        rigidbody = GetComponent<Rigidbody2D>();
        isGrounded = true;
        direction = 1;

        originSpeed = speed;

        animator = GetComponent<Animator>();

        originalScale = transform.localScale;

        score = 0;

    }

// Update is called once per frame
void Update()
    {
        

        Vector2 position = transform.position;
        float move = Input.GetAxis("Horizontal");
        position.x = position.x + speed * Time.deltaTime * move;
        transform.position = position;

        carryOffset = new Vector2((float)(direction*1.5), 0f);

        if (jumpsLeft < 2 && Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.AddForce(new Vector2(0, Mathf.Sqrt(-2 * Physics2D.gravity.y * JumpHeight)), ForceMode2D.Impulse);
            isGrounded = false;
            jumpsLeft++;
            animator.SetBool("isJumping", !isGrounded);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            direction = -1;

        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            direction = 1;

        }

        animator.SetBool("isJumping", !isGrounded);

        animator.SetFloat("xVelocity", Math.Abs(move));
        if (isCarrying)
        {
            animator.SetInteger("machState", 0);
        }
        //if (!isCarrying)
        //{



            if (Input.GetKey(KeyCode.W))
            {
                if (Input.GetKey(KeyCode.J))
                {
                    rigidbody.velocity = Vector3.zero;
                    rigidbody.AddForce(new Vector2(0, Mathf.Sqrt(-2 * Physics2D.gravity.y * 2500)), ForceMode2D.Impulse);
                    animator.SetBool("UC", true);

                }
                else
                {
                    animator.SetBool("UC", false);
                }

            }
            else {
                animator.SetBool("UC", false);

            }
        /*
                    if (Input.GetKey(KeyCode.D))
                    {
                        if (Input.GetKeyDown(KeyCode.J))
                        {
                            rigidbody.velocity = Vector3.zero;
                            rigidbody.AddForce(new Vector2(Mathf.Sqrt(-2 * Physics2D.gravity.y * 5000), 0), ForceMode2D.Impulse);
                        }

                    }
                    */


        if (Input.GetKey(KeyCode.J))
        {
            if (!(Input.GetKey(KeyCode.W)) || !(Input.GetKey(KeyCode.S)))
            {
                animator.SetBool("BD", true);
            }
            else {
                animator.SetBool("BD", false);
            }

        }
        else {
            animator.SetBool("BD", false);
        }

        if (Input.GetKey(KeyCode.S))
            {
                if (Input.GetKey(KeyCode.J))
                {
                    animator.SetBool("GP", true);

                    rigidbody.velocity = Vector3.zero;
                    rigidbody.AddForce(new Vector2(0, -(Mathf.Sqrt(-2 * Physics2D.gravity.y * 7500))), ForceMode2D.Impulse);
                }
                else {
                    animator.SetBool("GP", false);
                }

            }
            else
            {
                animator.SetBool("GP", false);

            }
            /*
            if (Input.GetKey(KeyCode.A))
            {
                if (Input.GetKeyDown(KeyCode.J))
                {
                    rigidbody.velocity = Vector3.zero;
                    rigidbody.AddForce(new Vector2(-(Mathf.Sqrt(-2 * Physics2D.gravity.y * 5000)), 0), ForceMode2D.Impulse);
                }

            }*/
            if (Input.GetKey(KeyCode.LeftShift))
            {
                animator.SetBool("mach?", true);
                if (speed< 25)
                {
                    animator.SetFloat("machState", .1f);
                    speed = speed + (float).05;
                }
                if (speed < 30 && speed > 25)
                {
                    animator.SetFloat("machState", .2f);
                    speed = speed + (float).01;
                }
                if (speed >= 30)
                {
                    animator.SetFloat("machState", 3f);
                    speed = speed + (float).005;
                }
            }
            else
            {
                animator.SetBool("mach?", false);
                if (speed > originSpeed)
                {
                    animator.SetInteger("machState", 0);
                    speed = speed - (speed / 9);
                }

            }

       // }
        RaycastHit2D wall = Physics2D.Raycast(position, new Vector2(direction, 0), 1, LayerMask.GetMask("WallFloor"));
        if (wall.collider != null)
        {
            
            if (speed > originSpeed)
            {
                speed = originSpeed;
                if (direction > 0)
                {
                    rigidbody.AddForce(new Vector2(-(Mathf.Sqrt(-2 * Physics2D.gravity.y * 100)), 25), ForceMode2D.Impulse);
                }
                else
                {
                    rigidbody.AddForce(new Vector2((Mathf.Sqrt(-2 * Physics2D.gravity.y * 100)), 25), ForceMode2D.Impulse);
                }
            }
        }

        

            if (Input.GetKeyDown(KeyCode.J))
        {
            if (isCarrying)
            {
                // Drop the object if already carrying
                DropObject();
            }
            else
            {
                // Attempt to pick up an object if not currently carrying
                TryPickUpObject();
            }
        }

        // If carrying an object, update its position to stay above the player
        if (isCarrying && objectToCarry != null)
        {
            objectToCarry.position = (Vector2)transform.position + carryOffset;
        }

        FlipObject();
    }

    private void FixedUpdate()
    {
        

        animator.SetFloat("yVelocity", rigidbody.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
            jumpsLeft = 0;
            isGrounded = true;
        
        // Check if the collider has the tag "LapPortal"
        if (collision.CompareTag("LapPortal"))
        {
            // Teleport the player (or object with this script) to the specified location
            transform.position = teleportLocation;

            // Optional: Add an effect, sound, or visual feedback if needed
            Debug.Log("Teleported to location: " + teleportLocation);
        }
    }





    private void TryPickUpObject()
    {
        // Find all colliders within the pickup range
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, pickupRange);

        foreach (Collider2D collider in colliders)
        {
            // Check if the collider's object has the "bomb" tag
            if (collider.CompareTag("bomb"))
            {
                // Set the object to carry and enable carrying mode
                objectToCarry = collider.transform;
                isCarrying = true;

                break; // Exit the loop once we've picked up an object
            }
        }
    }

    private void DropObject()
    {
        if (objectToCarry != null)
        {

            // Clear the carried object and reset carrying state
            objectToCarry = null;
            isCarrying = false;
        }
    }



        private void FlipObject()
        {
            // Ensure direction is either -1 (left) or 1 (right)
            direction = Mathf.Clamp(direction, -1, 1);

            // Flip the object if facing left (direction is -1) or set to original scale if facing right (direction is 1)
            if (direction == -1)
            {
                // Set x scale to negative to flip
                transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
            }
            else if (direction == 1)
            {
                // Set x scale to positive to reset the flip
                transform.localScale = originalScale;
            }
        }

    public void IncreaseScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    // Update the score display text
    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = score+"";
        }
    }

}
