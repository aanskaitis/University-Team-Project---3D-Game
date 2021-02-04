using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Adomas Anskaitis
/// 
/// This class contains behaviour for the player to become moveable
/// with key presses.
/// </summary>

public class characterMovement : MonoBehaviour
{
    CharacterController controller;
    public float jumpHeight = 3.5f;
    public float gravity = 15.0f;
    public float playerSpeed = 3.0f;

    Vector3 velocity;
    public bool isJumping = false;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // taking the input for character movement
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        move = transform.TransformDirection(move);

        // apply different gravity while the character is not jumping
        // so that the character falls quicker from the stairs and fences
        // which looks better
        if (!isJumping)
        {
            move.y -= gravity * 15.0f * Time.deltaTime;
        }
        
        // moving the character
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (Input.GetKey("space"))
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        if (isJumping)
        {
            velocity.y -= gravity * Time.fixedDeltaTime;
            controller.Move(velocity * Time.fixedDeltaTime);
            isJumping = !controller.isGrounded;
        }
    }

    void Jump()
    {
        if (!isJumping)
        {
            isJumping = true;
            velocity.y = Mathf.Sqrt(2 * gravity * jumpHeight);
        }
    }
}
