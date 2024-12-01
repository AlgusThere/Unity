using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;
using System.Linq;

public class EkBilgiler : MonoBehaviour
{

    //public Camera cam;
    //public float range = 10f;
    ////public LayerMask layer;

    public float mesafe = 10;

    public static EkBilgiler instance = null;
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;

    public float speed;
    public int GearNumber;
    public TextMeshProUGUI gearText;

    public List<TextMeshProUGUI> textList = new List<TextMeshProUGUI>();

    public int scoreWhite;
    public int scoreRed;
    public int scoreBlue;
    public int scoreBlack;

    public int white;
    public int red;
    public int blue;
    public int black;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject newObject;

        for (int i = 0; i < amountToPool; i++)
        {
            newObject = Instantiate(objectToPool);
            newObject.SetActive(false);
            pooledObjects.Add(newObject);
        }

        //string birinci = "Ýstanbul";
        //string ikinci = "Ankara";

        //if (birinci == "Ýstanbul" && ikinci == "Ankara")
        //{
        //    Debug.Log("Ýstanbul daha kalabalýktýr.");
        //}
        //else if(birinci == "Ankara" && ikinci == "Balýkesir")
        //{
        //    Debug.Log("Ankara daha kalabalýk");
        //}


        // TUPLE PATTERN

        //string enKalabalikHangisi(string bir, string iki)

        //=> (bir, iki) switch
        //{
        //    ("Ýstanbul", "Ankara") => "Ýstanbul daha kalabalýk",
        //    ("Ankara", "Balýkesir") => "Balýkesir daha kalabalýk",
        //    ("Bursa", "Aydýn") => "Bursa daha kalabalýk",
        //    ("Ýzmir", "Mersin") => "Ýzmir daha kalabalýk",
        //    (_, _) => "Deðerler yok",
        //};

        //Debug.Log(enKalabalikHangisi("Ýstanbul", "Ankara"));
    }


    public GameObject GetPooledObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }

    //public void NextLevel()
    //{
    //    //SceneManager.LoadScene(1); // 1 yerine "Sahne2" yazýlabilir.
    //    SceneManager.LoadScene(0);
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    //}

    private void Update()
    {
        //RaycastHit hit;
        //if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range)) // Range'den sonra layer eklenebilir.
        //{
        //    Debug.Log(hit.transform.gameObject.name);
        //    Vector3 forward = cam.transform.TransformDirection(Vector3.forward) * range;
        //    Debug.DrawRay(cam.transform.position, forward, Color.red);
        //}

        //HissetmeMenzil();


        // PROPERTY PATTERN

        //float CarGear(EkBilgiler ben, float deneme)
        //    => ben switch
        //    {
        //        { speed: 0 } => GearNumber = 1,
        //        { speed: 10 } => GearNumber = 1,
        //        { speed: 25 } => GearNumber = 2,
        //        { speed: 50 } => GearNumber = 3,
        //        { speed: 75 } => GearNumber = 4,
        //        { speed: 100 } => GearNumber = 5,
        //        { speed: 125 } => GearNumber = 6,
        //        _ => speed
        //    };

        //CarGear(this, speed);
        //gearText.text = GearNumber.ToString();



        //----------------------------
        white = scoreWhite;
        red = scoreRed;
        blue = scoreBlue;
        black = scoreBlack;

        var fruit = new Dictionary<string, int>
        {
            ["white"] = white,
            ["red"] = red,
            ["blue"] = blue,
            ["black"] = black,
        };

        int dictIndex = 4;
        foreach (var item in fruit.OrderBy(x => x.Value))
        {
            fruit[item.Key] = dictIndex;
            dictIndex--;
        }
        dictIndex= 0;

        foreach(var item in fruit)
        {
            textList[dictIndex].text = "" + item.Value;
            if (textList.Count - 1 == dictIndex) break;
            dictIndex++;
        }
        dictIndex = 0;

    }


    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireSphere(transform.position, mesafe);
    //}

    //void HissetmeMenzil()
    //{
    //    Collider[] hitCollliders = Physics.OverlapSphere(transform.position, mesafe);

    //    foreach (var item in hitCollliders)
    //    {
    //        if (item.gameObject.CompareTag("Player"))
    //        {
    //            Debug.Log("Player girdi.");
    //        }
    //    }
    //}

}
