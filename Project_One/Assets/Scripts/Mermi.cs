using System.Collections;
using UnityEngine;

public class Mermi : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(setFalse());
    }
    private void Update()
    {
        transform.Translate(0 , 0 ,10 * Time.deltaTime);
    }

    IEnumerator setFalse()
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }
}
