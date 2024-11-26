using UnityEngine;

public class Kutuphane : MonoBehaviour
{
    //Transform
    //Quaternion
    //Vector2 - Vector3
    //Mathf
    //GameObject
    //Instantiate
    //Destroy


    public Transform bNoktasi;
    public Vector3 a = new Vector3(1, 2 ,3 );
    public Vector3 b = new Vector3(4, 3 ,2 );

    public float current = 0;
    public float target = 10;
    public float maxDelta = 1;

    public float xValue = 0f;
    public float xMin = 0;
    public float xMax = 10;

    public GameObject kup;
    public GameObject namlu;

    public GameObject[] ornekObj;

    private void Start()
    {
        //transform.position = Vector3.zero;
        //transform.position = Vector3.left;
        //transform.position = Vector3.forward;
        //transform.position = Vector3.up;

        //Debug.Log(Vector3.Max(a,b));
        //Debug.Log(Vector3.Min(a,b));

        //Quaternion rot = Quaternion.Euler(0,30,0);
        //transform.rotation = rot;

        //transform.DetachChildren(); || Tüm child'larý parent yapar.

        //if(transform.IsChildOf(bNoktasi.transform)) || bNoktasi objesinin çocuðuysa
        //{
        //    Debug.Log("Evet bu ana obje");
        //}

        //Debug.Log(transform.GetChild(0).name); || Ýlk çocuðun adý

        //Debug.Log(transform.childCount);

        //transform.parent = bNoktasi;


        //gameObject.tag = "Oyuncu";

        //gameObject.GetComponent<BoxCollider>().isTrigger = true;

        //gameObject.AddComponent<SphereCollider>();

        //if(gameObject.activeInHierarchy == false )
        //{
        //    Debug.Log("Pasif");
        //}
        //else
        //{
        //    Debug.Log("Aktif");
        //}

        //gameObject.layer = 2;

        //gameObject.isStatic = true; || Sabitlik aktif.

        //gameObject.layer = LayerMask.NameToLayer("Ground");


        //float degerIki = Mathf.Sqrt(9); || Karekök.
        //Debug.Log(degerIki);
        //float deger = Mathf.Ceil(9.4f);
        //Debug.Log(deger);

        //if(Mathf.Approximately(1.0f, 20.0f / 20.0f)) || Yirminin yirmiye bölümü 1 mi.
        //{
        //    Debug.Log("Deðer eþit oldu");
        //}

        //float degerBir = Mathf.Abs(-47); || Mutlak deðer.
        //Debug.Log(degerBir);

        //Instantiate(kup, namlu.transform.position,transform.rotation);
        //Destroy(gameObject,5);

        //ornekObj = GameObject.FindGameObjectWithTag("Ornek");
        //ornekObj = GameObject.FindGameObjectsWithTag("Ornek");

    }

    private void Update()
    {
        //transform.position = Vector3.Lerp(transform.position, bNoktasi.position, 5f * Time.deltaTime); || MoveTowards'a göre daha smooth bir geçiþi var.

        //float distanceOrnek = Vector3.Distance(transform.position, bNoktasi.position);
        //Debug.Log(distanceOrnek);

        //transform.position = Vector3.MoveTowards(transform.position, bNoktasi.position, 5f * Time.deltaTime); || Sabit hýz kullanýr.

        //transform.position = Vector2.Lerp(transform.position, bNoktasi.position, 5f * Time.deltaTime); || Z ekseninde hareket etmez.

        //transform.rotation = Quaternion.Lerp(transform.rotation, bNoktasi.rotation, 5f *  Time.deltaTime);

        //transform.rotation = Quaternion.RotateTowards(transform.rotation, bNoktasi.rotation, 100f * Time.deltaTime);

        //transform.Rotate(55 * Time.deltaTime, 0 ,0,Space.Self);

        //transform.RotateAround(bNoktasi.transform.position, Vector3.up,20 * Time.deltaTime);

        //transform.Translate(0,0, 10f * Time.deltaTime);

        //transform.LookAt(bNoktasi); || BNoktasi objesine bakar.

        //float x = Mathf.Clamp(xValue, xMin, xMax); || Deðer sýnýrlama(FPS kamera bakýþý sýnýrlama).
        //transform.position = new Vector3(x,0,0);

        //current = Mathf.MoveTowards(current, target, maxDelta * Time.deltaTime); || Belirlenen hedefe kadar deðer gider.

    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Mermi"))
        {
            //10 Hasar
            Debug.Log("10 hasar eksildi. Mermi çarptý");
        }
        else if(other.gameObject.CompareTag("Roket"))
        {
            //100 Hasar
            Debug.Log("100 hasar eksildi. Roket çarptý");
        }
    }
}