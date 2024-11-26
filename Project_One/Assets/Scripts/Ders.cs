using UnityEngine;

public class Ders : MonoBehaviour
{
    [HideInInspector]
    public string ornek;
    //[SerializeField]
    //private string ornek2;

    [Header("BAÞLIK")]
    public int sayi;
    public float virgulluSayi;
    public bool trueOrFalse;

    public int matematikOrnek1;
    public int matematikOrnek2;

    void Start()
    {
        ornek = "isim";
        //ornek2 = "isim1";

        //sayi = 10;

        virgulluSayi = 10.50f;

        trueOrFalse = true;


        int sonucNegatif = matematikOrnek1 - matematikOrnek2;
        int sonucPozitif = matematikOrnek1 + matematikOrnek2;
        int sonucCarpma = matematikOrnek1 * matematikOrnek2;
        int sonucBolme = matematikOrnek1 / matematikOrnek2;
        Debug.Log("Negatif:" + sonucNegatif);
        Debug.Log("Pozitif:" + sonucPozitif);
        Debug.Log("Çarpma:" + sonucCarpma);
        Debug.Log("Bölme:" + sonucBolme);
    }

    // Update is called once per frame
    void Update()
    {
        //sayi++;

        //transform.Translate(0, 0, 10 * Time.time);

        //Time.timeScale = 0.1f;
    }
}
