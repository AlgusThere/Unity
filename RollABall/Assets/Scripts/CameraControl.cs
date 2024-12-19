using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject top;

    public Vector3 aradakiFark;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        aradakiFark = transform.position - top.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = top.transform.position + aradakiFark;
    }
}
