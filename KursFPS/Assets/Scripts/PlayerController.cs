using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviourPunCallbacks
{
    public CharacterController controller;

    public float horizontal;
    public float vertical;

    public float PlayerSpeed = 5f;
    public float RunSpeed = 10f;
    public float FirstSpeed = 5f;

    public float gravity = -9.81f;
    public float jumpHeight = 2f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public float originalHeight;
    public float crouchHeight;

    public bool Egildi = false;
    public bool Kalkti = true;

    Vector3 velocity;
    bool isGrounded;

    PhotonView view;

    public GameObject[] elimdekiSilahlar;
    public GameObject[] bedendekiSilahlar;

    public float saglik = 100f;

    public float yenidenDogmaSuresi = 5;
    public TextMeshProUGUI yenidenCanlanmaText;
    public Slider slider;

    public GameObject deadPanel;

    void Start()
    {
        view = GetComponent<PhotonView>();
        saglik = 100;
        slider = GameManager.instance.slider;
        deadPanel = GameManager.instance.deadPanel;
        yenidenCanlanmaText = GameManager.instance.yenidenDogmaText;
        elimdekiSilahlar = GameManager.instance.elimdekiSilahlar;

        if (!view.IsMine)
        {
            gameObject.tag = "Enemy";
        }
    }


    void Update()
    {
        if (view.IsMine)
        {
            Hareket();
            slider.value = saglik;
            if (Input.GetKeyDown(KeyCode.Alpha1) && elimdekiSilahlar[0].activeInHierarchy == false)
            {
                elimdekiSilahlar[1].SetActive(false);

                elimdekiSilahlar[0].SetActive(true);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) && elimdekiSilahlar[1].activeInHierarchy == false)
            {
                elimdekiSilahlar[0].SetActive(false);

                elimdekiSilahlar[1].SetActive(true);
            }

            if (saglik <= 0)
            {
                view.RPC("meshPasifEt", RpcTarget.All);

                if (yenidenDogmaSuresi >= 0)
                {
                    yenidenDogmaSuresi -= Time.deltaTime;
                    yenidenCanlanmaText.text = "Revival Time = " + ((int)yenidenDogmaSuresi).ToString();
                }
                else
                {
                    gameObject.GetComponent<CharacterController>().enabled = true;

                    view.RPC("meshAktifEt", RpcTarget.All);
                    float x = Random.Range(-10, 10);
                    float z = Random.Range(-10, 10);
                    gameObject.transform.position = new Vector3(x, 8, z);
                    deadPanel.SetActive(false);
                    yenidenDogmaSuresi = 5;
                    saglik = 100;
                    slider.value = slider.maxValue;
                }
            }
        }
        else
        {
            //benim olmayan iþlemler
        }

        //Hareket();

    }

    void Hareket()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Egildi == true)
            {
                PlayerSpeed = 3f;
                FirstSpeed = 3f;
            }
            else
            {
                PlayerSpeed = RunSpeed;
            }
        }
        else
        {
            PlayerSpeed = FirstSpeed;
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        controller.Move(move * PlayerSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);


        if (Input.GetKey(KeyCode.LeftControl))
        {
            controller.height = Mathf.Lerp(controller.height, crouchHeight, 3f * Time.deltaTime);
            PlayerSpeed = 3f;
            FirstSpeed = 3f;
            jumpHeight = 0f;
            Egildi = true;
            Kalkti = false;
        }
        else
        {
            controller.height = Mathf.Lerp(controller.height, originalHeight, 3f * Time.deltaTime);
            PlayerSpeed = 5f;
            FirstSpeed = 5f;
            jumpHeight = 2f;
            Egildi = false;
            Kalkti = true;
        }
    }

    [PunRPC]
    void darbever(int darbegucu)
    {
        saglik -= darbegucu;

        if (saglik <= 0)
        {
            if (view.IsMine)
            {
                deadPanel.SetActive(true);

                for (int i = 0; i < GameManager.instance.elimdekiSilahlar.Length; i++)
                {
                    GameManager.instance.elimdekiSilahlar[i].GetComponent<Weapon>().toplamMermiSayisi = GameManager.instance.elimdekiSilahlar[i].GetComponent<Weapon>().maksimumMermiSayisi;
                    GameManager.instance.elimdekiSilahlar[i].GetComponent<Weapon>().toplamMermiText.text = GameManager.instance.elimdekiSilahlar[i].GetComponent<Weapon>().maksimumMermiSayisi.ToString();

                    GameManager.instance.elimdekiSilahlar[i].GetComponent<Weapon>().sarjorKapasitesi = GameManager.instance.elimdekiSilahlar[i].GetComponent<Weapon>().maksimumSarjorSayisi;
                    GameManager.instance.elimdekiSilahlar[i].GetComponent<Weapon>().kalanMermiText.text = GameManager.instance.elimdekiSilahlar[i].GetComponent<Weapon>().maksimumSarjorSayisi.ToString();
                }
            }
        }
    }

    [PunRPC]

    void meshAktifEt()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        saglik = 100;
    }

    [PunRPC]

    void meshPasifEt()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }
}
