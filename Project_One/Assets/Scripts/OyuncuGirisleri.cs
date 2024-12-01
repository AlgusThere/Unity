using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OyuncuGirisleri : MonoBehaviour
{
    Rigidbody body;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //if(Input.GetKey(KeyCode.A))
        //{
        //    Debug.Log("A Tuşunu basılıyor");
        //}

        //if(Input.GetKeyDown(KeyCode.S))
        //{
        //    Debug.Log("S Tuşuna basıldı.");
        //}

        //if(Input.GetKeyUp(KeyCode.D))
        //{
        //    Debug.Log("D Tuşunu basılmayı bırakıldı.");
        //}

        //if(Input.GetButton("Fire1"))
        //{
        //    Debug.Log("Sol tık basılıyor.");
        //}

        //if(Input.GetButtonDown("Fire2"))
        //{
        //    Debug.Log("Sağ tık basıldı.");
        //}

        //if(Input.GetMouseButton(0))
        //{
        //    Debug.Log("Mouse sol tık basılıyor.");
        //}

        //if(Input.GetKey(KeyCode.Mouse0))
        //{
        //    Debug.Log("Get Key'den gelen mouse tıklanması.");
        //}

        //float x = Input.GetAxis("Horizontal"); // A ve D tuşlarını alır.
        //float z = Input.GetAxis("Vertical"); // W ve S tuşlarını alır.

        //body.linearVelocity = new Vector3(x * 10, transform.position.y , z * 10);

        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    body.AddForce(transform.up * 2000);
        //}

        //if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        //{

        //}

        //Debug.Log(Input.touchCount);

        //float a = Input.acceleration.x;
        //Debug.Log(a);
    }
}
