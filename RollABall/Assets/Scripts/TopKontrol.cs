using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TopKontrol : MonoBehaviour
{

    public Rigidbody fizik;
    public int hiz;

    //public float ziplamaGucu;
    //public bool yereDegme;
    //public float gravity = -9.81f;

    public int puan;
    public int objeSayisi;

    public TextMeshProUGUI puanText;
    public TextMeshProUGUI oyunBittiText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        fizik = GetComponent<Rigidbody>();
        //transform.Translate(0, 0, 10f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.Translate(0, 0, 0.01f);

        //transform.Rotate(0, 0, 0.1f);

        //if(Input.GetKeyDown(KeyCode.Escape))
        //{
        //    Debug.Log("ESC tuþuna basýldý.");
        //}

        //if(Input.GetKeyDown(KeyCode.E))
        //{
        //    Debug.Log("E tuþuna basýldý.");
        //}

        float yatay = Input.GetAxis("Horizontal");
        float dikey = Input.GetAxis("Vertical");

        //float ziplama = Input.GetAxis("Jump");

        //Debug.Log("Yatay = " + yatay);
        //Debug.Log("Dikey = " + dikey);

        Vector3 vektor = new Vector3(yatay, 0, dikey);

        fizik.AddForce(vektor * hiz);

        //Debug.Log(transform.position);

        //Ziplama();

    }

    //void Ziplama()
    //{

    //    if (Input.GetButtonDown("Jump"))
    //    {
    //        fizik.AddForce(Vector3.up * ziplamaGucu, ForceMode.Impulse);
    //    }


    //}

    //void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.collider.tag == "Ground")
    //    {
    //        yereDegme = true;
    //    }
    //}

    //void OnCollisionExit(Collision collision)
    //{
    //    if (collision.collider.tag == "Ground")
    //    {
    //        yereDegme = false;
    //    }
    //}


    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Çarpýþma Oldu.");

        other.gameObject.SetActive(false);
        puan++;
        //puan += 1;
        //puan = puan + 1;
        //Debug.Log("Sayaç = " + puan);

        puanText.text = "Puan: " + puan;

        if (puan == objeSayisi)
        {
            oyunBittiText.gameObject.SetActive(true);
        }
    }

    //void OnTriggerExit(Collider other)
    //{
    //    other.gameObject.SetActive(false);
    //}

    //void OnTriggerStay(Collider other)
    //{
    //    other.gameObject.SetActive(false);
    //}

}
