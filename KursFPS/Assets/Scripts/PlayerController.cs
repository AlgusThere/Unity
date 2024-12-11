using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviourPunCallbacks
{
    public CharacterController controller;

    public float horizontal;
    public float vertical;

    public float playerSpeed = 5f;
    public float runSpeed = 10f;
    public float firstSpeed;

    public float gravity = -9.81f;
    public float jumpHeight = 2f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public float originalHeight;
    public float crouchHeight;

    public bool Egildi = false;
    public bool Kalkti = true;

    Vector3 velocity;
    bool isGrounded;

    void Hareket()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            if(Egildi == true)
            {
                playerSpeed = 3f;
                firstSpeed = 3f;
            }
            else
            {
                playerSpeed = runSpeed;
            }
        }
        else
        {
            playerSpeed = firstSpeed;
        }

        isGrounded = Physics.CheckSphere(groundCheck.position,groundDistance,groundMask);
        
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        controller.Move(move * playerSpeed * Time.deltaTime);
    }
}
