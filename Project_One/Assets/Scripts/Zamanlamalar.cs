using System.Collections;
using UnityEngine;

public class Zamanlamalar : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(timer());
        //Invoke("Deneme", 2.0f);
        InvokeRepeating("Deneme", 2.0f, 2.0f);

    }

    void Deneme()
    {
        Debug.Log("Selamlar");
    }

    IEnumerator timer()
    {
        Debug.Log("Ýlk Çalýþtý");
        yield return new WaitForSeconds(5);
        Debug.Log("Sonradan Çalýþtý.");
        CancelInvoke();
    }
}
