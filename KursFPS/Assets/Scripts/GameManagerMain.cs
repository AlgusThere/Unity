using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerMain : MonoBehaviour
{
    public GameObject player;
    public Transform[] genelSpawn;
    public GameObject cam;

    public GameObject[] elimdekiSilahlar;
    public GameObject[] bedendekiSilahlar;

    public Slider slider;
    public GameObject deadPanel;
    public TextMeshProUGUI yenidenDogmaText;

    public static GameManagerMain instance = null;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        GameObject obje = PhotonNetwork.Instantiate(prefabName: "Player", position: genelSpawn[PhotonNetwork.LocalPlayer.ActorNumber].transform.position, Quaternion.identity, group: 0, data: null);
        //GameObject obje = PhotonNetwork.Instantiate(prefabName: "Player", position: genelSpawn[1].transform.position, Quaternion.identity, group: 0, data: null);
        player = obje;
        InvokeRepeating("KameraEkleme", 1f, 1f);
    }


    void Update()
    {

    }
    void KameraEkleme()
    {
        cam.transform.parent = player.transform;
        cam.transform.localPosition = new Vector3(0, 0.5f, 0);
        StartCoroutine(CameraController.instance.playerBodyInstance());
    }
}
