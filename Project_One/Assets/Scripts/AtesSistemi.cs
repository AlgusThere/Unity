using UnityEngine;

public class AtesSistemi : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject instance = EkBilgiler.instance.GetPooledObject();
            instance.transform.position = transform.position;
            instance.transform.rotation = transform.rotation;
            instance.SetActive(true);
        }
    }
}
