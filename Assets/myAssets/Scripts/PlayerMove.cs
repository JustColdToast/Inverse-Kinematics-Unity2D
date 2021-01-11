using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    public float forceMultiplier = 1.25f;
    public Animator anim;
    private bool isWalking = false;
    public float playerSpeed = 50f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Get the rigidbody being used
        anim = GetComponent<Animator>();  // Get the animater
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()  // Runs every physics tick
    {
        playerMovement();  // Call the update for player movement
    }

    private void playerMovement()  // Function responsbile for calling playermovement
    {
        anim.SetBool("IsWalking", isWalking);
        // Check if any rotations need to be performed, for movement
        if ((rb.velocity.normalized != new Vector2(Input.GetAxis("Horizontal"), 0).normalized) ^ (Input.GetAxis("Horizontal") == 0))
        {
            //transform.localRotation *= Quaternion.Euler(0, 180, 0);  // Rotate the player 180
        }
        // First check if no input is detected, this starts auto slowing (basically force dampening)
        if (Input.GetAxis("Horizontal") == 0)
        {
            isWalking = false;
            rb.AddForce(-rb.velocity * 2*forceMultiplier *playerSpeed);
        } else
        {
            if (rb.velocity.magnitude == 0)
            {
                rb.velocity = (new Vector2(Input.GetAxis("Horizontal"), 0).normalized)*playerSpeed;
            }
            isWalking = true;
            // First calculate the force to be added
            Vector2 force = new Vector2(0,Input.GetAxis("Horizontal")).normalized * forceMultiplier;  // Normalize the vector with a direction, then add force
            rb.AddForce(force*playerSpeed);
        }
    }
}
