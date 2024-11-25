using UnityEngine;

public class Ders : MonoBehaviour
{
    [HideInInspector]
    public string ornek;
    [SerializeField]
    private string ornek2;

    public int sayi;
    public float virgulluSayi;
    public bool trueOrFalse;

    void Start()
    {
        ornek = "isim";
        ornek2 = "isim1";

        sayi = 10;

        virgulluSayi = 10.50f;

        trueOrFalse = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
