using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    private float currentSpeed;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        //gravity
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        //moving
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = speed * 2;
        }
        else
        {
            currentSpeed = speed;
        }
        controller.Move(move * currentSpeed * Time.deltaTime);
        if (Input.GetButtonDown("Jump") && isGrounded)//jump
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        //gravity
        velocity.y += gravity * Time.deltaTime;
        //moving
        controller.Move(velocity * Time.deltaTime);
    }
}
