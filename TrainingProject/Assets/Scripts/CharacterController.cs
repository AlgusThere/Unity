using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    public Rigidbody player;

    public float hiz;


    void Start()
    {
        player = GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
        float yatay = Input.GetAxis("Horizontal");
        float dikey = Input.GetAxis("Vertical");

        Vector3 hareket = transform.right * yatay + transform.forward * dikey;
        transform.Translate(hareket * hiz * Time.deltaTime, Space.World);

        float zipla = Input.GetAxis("Jump");

        if(Input.GetKeyDown(KeyCode.Space))
        {

        }
    }
}
