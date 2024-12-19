using UnityEngine;

public class ObjeKontrol : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Vector3 vektor = new Vector3(15, 30, 45);
        transform.Rotate(vektor * 0.01f);
    }
}
