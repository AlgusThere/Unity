using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerDeneme : MonoBehaviour
{
    public CharacterController controller;

    [Header("Hareket ve Fizik")]
    [SerializeField] private float hiz, gravity = -9.8f, ziplamaGucu;

    [Header("Zemin Kontrol")]
    [SerializeField] public Transform groundCheck;
    [SerializeField] public float groundDistance = 0.3f;
    [SerializeField] public LayerMask groundMask;

    [Header("Yön ve Yerçekimi")]
    public bool yerdeMi;
    Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }


    void Update()
    {
        yerdeMi = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(yerdeMi && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 hareket = transform.right * x + transform.forward * z;
        controller.Move(hareket * hiz * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && yerdeMi)
        {
            velocity.y = Mathf.Sqrt(ziplamaGucu * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

    }
}
