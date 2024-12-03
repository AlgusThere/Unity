using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 offset;
    public GameObject player;
    public float range;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, player.transform.position + offset, range * Time.deltaTime);
    }
}
