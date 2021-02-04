using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Adomas Anskaitis
/// 
/// This class contains behaviour to animate the player.
/// </summary>

public class animationController : MonoBehaviour
{
    Animator animator;
    float velocityX = 0.0f;
    float velocityZ = 0.0f;
    float maxVelocity = 1.0f;

    // animation acceleration and deceleration
    float acceleration = 5.0f;
    float deceleration = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool forwardPressed = Input.GetKey("w");
        bool backwardPressed = Input.GetKey("s");
        bool rightPressed = Input.GetKey("d");
        bool leftPressed = Input.GetKey("a");
        bool jumpPressed = Input.GetKey("space");

        // run forwards
        if (forwardPressed && velocityZ < maxVelocity)
        {
            velocityZ += Time.deltaTime * acceleration;
        }

        // reset forwards
        if (!forwardPressed && velocityZ > 0.0f)
        {
            velocityZ -= Time.deltaTime * deceleration;
        }

        // run backwards
        if (backwardPressed && velocityZ > -maxVelocity)
        {
            velocityZ -= Time.deltaTime * acceleration;
        }

        // reset backwards
        if (!backwardPressed && velocityZ < 0.0f)
        {
            velocityZ += Time.deltaTime * deceleration;
        }

        // run to the right
        if (rightPressed && velocityX < maxVelocity)
        {
            velocityX += Time.deltaTime * acceleration;
        }

        // reset right
        if (!rightPressed && velocityX > 0.0f)
        {
            velocityX -= Time.deltaTime * deceleration;
        }

        // run to the left
        if (leftPressed && velocityX > -maxVelocity)
        {
            velocityX -= Time.deltaTime * acceleration;
        }

        // reset left
        if (!leftPressed && velocityX < 0.0f)
        {
            velocityX += Time.deltaTime * deceleration;
        }


        // reset Z
        if (!forwardPressed && !backwardPressed && velocityZ != 0.0f &&
            (velocityZ > -0.05f && velocityZ < 0.05f))
        {
            velocityZ = 0.0f;
        }

        // reset X
        if (!leftPressed && !rightPressed && velocityX != 0.0f &&
            (velocityX > -0.05f && velocityX < 0.05f))
        {
            velocityX = 0.0f;
        }


        // jump
        if (jumpPressed)
        {
            animator.SetTrigger("jump");
        }
        else
        {
            animator.ResetTrigger("jump");
        }

        animator.SetFloat("Velocity Z", velocityZ);
        animator.SetFloat("Velocity X", velocityX);

    }
}
