using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Pun;

public class Weapon : MonoBehaviour
{
    public Camera benimCam;

    //public Vector3 offset;
    public GameObject firePoint;
    public GameObject mermi;

    public float menzil;
    Animator animatorum;
    //public AudioSource atesSes;
    float iceridenAtesEtmeSikligi;
    public float disaridanAtesEtmeSikligi;
    public int darbeGucu;
    public bool atesEdebilirmi;
    //public ParticleSystem kanEfekti;
    //public ParticleSystem muzzleEfekti;
    //public ParticleSystem zeminEfekti;

    public int toplamMermiSayisi = 90;
    public int sarjorKapasitesi = 30;
    int kalanMermi;
    public TextMeshProUGUI toplamMermiText;
    public TextMeshProUGUI kalanMermiText;

    public bool isShot = true;
    public bool isReload = true;

    public bool reloadVar = false;

    public Coroutine reloadCoroutine;

    public int maksimumMermiSayisi;
    public int maksimumSarjorSayisi;

    private void Start()
    {
        animatorum = GetComponent<Animator>();
        kalanMermi = sarjorKapasitesi;
        toplamMermiText.text = toplamMermiSayisi.ToString();
        kalanMermiText.text = kalanMermi.ToString();
        animatorum.SetTrigger("EleAlma");
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Mouse0) && isShot)
        {
            if(atesEdebilirmi && Time.time > iceridenAtesEtmeSikligi && kalanMermi != 0)
            {
                AtesEt();
                iceridenAtesEtmeSikligi = Time.time + disaridanAtesEtmeSikligi;
            }
        }
        if(Input.GetKeyDown(KeyCode.R) && isReload)
        {
            ReloadKontrol();


            if(toplamMermiSayisi == 0 || sarjorKapasitesi == kalanMermi)
            {
                isShot = true;
                isReload = true;
            }
            else
            {
                isShot = false;
                isReload = false;
            }

            reloadCoroutine = StartCoroutine("endAnimation");
        }
    }

    IEnumerator endAnimation()
    {
        yield return new WaitForSeconds(1F);
        animatorum.SetBool("Reload", false);
        isShot = true;
        isReload = true;
        reloadVar = false;
    }

    public void AtesEt()
    {
        kalanMermi--;
        kalanMermiText.text = kalanMermi.ToString();

        AtesEtmeTeknikIslemleri();

        RaycastHit hit;
        if(Physics.Raycast(benimCam.transform.position + benimCam.transform.forward * 1.5f , benimCam.transform.forward, out hit, menzil))
        {
            if(hit.transform.gameObject.CompareTag("Enemy"))
            {
                //Instantiate(kanEfekti, hit.point, Quaternion.LookRotation(hit.normal));
                if(hit.collider.gameObject.GetComponent<PlayerController>().saglik > 0)
                {
                    hit.collider.gameObject.GetComponent<PhotonView>().RPC("darbever", RpcTarget.All, darbeGucu);
                }
            }

            //if(hit.transform.gameObject.CompareTag("Ground"))
            //{
            //    Instantiate(zeminEfekt, hit.point, Quaternion.LookRotation(hit.normal));
            //}
        }

        //atesSes.Play();
    }

    void ReloadKontrol()
    {
        if(kalanMermi < sarjorKapasitesi && toplamMermiSayisi != 0)
        {
            reloadVar = true;
            animatorum.SetBool("Reload", true);
        }
    }

    void ReloadIslemi()
    {
        if(kalanMermi == 0)
        {
            if(toplamMermiSayisi <= sarjorKapasitesi)
            {
                kalanMermi = toplamMermiSayisi;
                toplamMermiSayisi = 0;
            }
            else
            {
                toplamMermiSayisi -= sarjorKapasitesi;
                kalanMermi = sarjorKapasitesi;
            }
        }
        else
        {
            if(toplamMermiSayisi <= sarjorKapasitesi)
            {
                int olusanDeger = kalanMermi + toplamMermiSayisi;
                if(olusanDeger > sarjorKapasitesi)
                {
                    kalanMermi = sarjorKapasitesi;
                    toplamMermiSayisi = olusanDeger - sarjorKapasitesi;
                }
                else
                {
                    kalanMermi += toplamMermiSayisi;
                    toplamMermiSayisi = 0;
                }
            }
            else
            {
                int MevcutMermimiz = sarjorKapasitesi - kalanMermi;
                toplamMermiSayisi -= MevcutMermimiz;
                kalanMermi = sarjorKapasitesi;
            }
        }
        toplamMermiText.text = toplamMermiSayisi.ToString();
        kalanMermiText.text = kalanMermi.ToString();
    }

    void AtesEtmeTeknikIslemleri()
    {
        //Instantiate(mermi, firePoint.transform.position, firePoint.transform.rotation);
        PhotonNetwork.Instantiate(prefabName: "Mermi", position: firePoint.transform.position, firePoint.transform.rotation, group: 0, data: null);
        //MuzzleEfekt.Play();
    }
}
