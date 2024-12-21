using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Range(50f,500f)]
    public float sens;
    public GameObject player;

    float xRot = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void Update()
    {
        float MouseX = Input.GetAxis("Mouse X") * sens * Time.deltaTime;
        float MouseY = Input.GetAxis("Mouse Y") * sens * Time.deltaTime;

        xRot -= MouseY;
        xRot = Mathf.Clamp(xRot, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
        player.transform.Rotate(Vector3.up * MouseX);
    }
}
