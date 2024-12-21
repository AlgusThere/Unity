using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;

    [Header("Hareket ve Fizik")]
    [SerializeField] private float hiz, gravity = -9.8f, ziplamaGucu = 1f;

    [Header("Yön ve Yerçekimi")]
    [SerializeField] private bool yerdeMi;
    Vector3 Velocity;

    [Header("Zemin Kontrol")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.3f;
    [SerializeField] private LayerMask groundMask;


    void Start()
    {
        controller = GetComponent<CharacterController>();
    }


    void Update()
    {
        yerdeMi = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (yerdeMi && Velocity.y < 0)
        {
            Velocity.y = -2f;
        }

        float yatay = Input.GetAxis("Horizontal");
        float dikey = Input.GetAxis("Vertical");

        Vector3 hareket = transform.right * yatay + transform.forward * dikey;
        controller.Move(hareket * hiz * Time.deltaTime);
        //transform.Translate(hareket * hiz * Time.deltaTime, Space.World);

        if (Input.GetButtonDown("Jump") && yerdeMi)
        {
            Velocity.y = Mathf.Sqrt(ziplamaGucu * gravity * -2f);
        }

        Velocity.y += gravity * Time.deltaTime;

        controller.Move(Velocity * Time.deltaTime);
    }

}
