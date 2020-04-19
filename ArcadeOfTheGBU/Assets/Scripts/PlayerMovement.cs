using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Stats")]
    public float maxSpeed;
    public float acceleration;
    public float deceleration;  

    [Header("Inputs")]
    public string horizontalInput;
    public string verticalInput;

    private float horizontal;
    private float vertical;

    [System.NonSerialized] // So that I don't see movement in editor.
    public Vector2 movement;
    private Vector2 moveTarget;

    [Header("Components")]
    private Rigidbody2D rigBody;
    private BoxCollider2D coll;


    private void Start()
    {
        rigBody = GetComponent<Rigidbody2D>();

        if (coll != null) // For now not needed
            coll = GetComponent<BoxCollider2D>();
    }

    private void Update() // REMEMBER INPUTS HERE
    {
        horizontal = Input.GetAxisRaw(horizontalInput);
        vertical = Input.GetAxisRaw(verticalInput);        
    }


    private void FixedUpdate() // REMEMBER CALCULATIONS HERE
    {
        Move();        
    }


    void Move()
    {
        
        movement = new Vector2(horizontal, vertical); // movement is just both inputs into one variable

        moveTarget = movement * maxSpeed; // The target the player is suposed to follow

        if(moveTarget != Vector2.zero)
        {
            //Set moving state/ animation state
            rigBody.velocity = Vector2.MoveTowards(rigBody.velocity, moveTarget, acceleration * Time.deltaTime);
        }
        else
        {
            //Stop State/animation
            rigBody.velocity = Vector2.MoveTowards(rigBody.velocity, Vector2.zero, deceleration * Time.deltaTime);
        }
    }

    void flipSprite()
    {
        if(horizontal < 0)
        {
            // Flip Right
        }
        else if (horizontal > 0)
        {
            // Flip Left
        }
        else if (vertical < 0)
        {
            // Flip Up
        }
        else if (vertical > 0)
        {
            // Flip Down
        }
    }


}
