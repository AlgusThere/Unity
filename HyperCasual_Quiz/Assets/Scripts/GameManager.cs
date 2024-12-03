using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public bool isStarted = false;
    public GameObject startedButton;

    public TextMeshProUGUI soruTextim;

    public TextMeshProUGUI[] cevap1;
    public TextMeshProUGUI[] cevap2;

    public GameObject[] kapi1;
    public GameObject[] kapi2;

    public static GameManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void startButton()
    {
        isStarted = true;
        startedButton.SetActive(false);
    }

    public void Sorular(int indis)
    {
        if (indis == 0)
        {
            string[] sorular = { "Türkiye'nin baþkenti", "The Capital of Turkey" };
            soruTextim.text = sorular[0];

            string[] cevap1Sorular = { "Ankara", "Ankara" };
            string[] cevap2Sorular = { "Ýstanbul", "Istanbul" };

            for (int i = 0; i < 3; i++)
            {
                cevap1[i].text = cevap1Sorular[0];
                cevap2[i].text = cevap2Sorular[0];

                kapi1[i].tag = "Dogru";
                kapi2[i].tag = "Yanlis";
            }

        }

        if (indis == 1)
        {
            string[] sorular = { "En zengin kim?", "Who is the richest?" };
            soruTextim.text = sorular[0];

            string[] cevap1Sorular = { "Elon Musk", "Elon Musk" };
            string[] cevap2Sorular = { "Mark Zuckerberg", "Mark Zuckerberg" };

            for (int i = 0; i < 3; i++)
            {
                cevap1[i].text = cevap1Sorular[0];
                cevap2[i].text = cevap2Sorular[0];

                kapi1[i].tag = "Dogru";
                kapi2[i].tag = "Yanlis";
            }

        }

        if (indis == 2)
        {
            string[] sorular = { "2+2 = ?", "2+2 = ?" };
            soruTextim.text = sorular[0];

            string[] cevap1Sorular = { "5", "5" };
            string[] cevap2Sorular = { "4", "4" };

            for (int i = 0; i < 3; i++)
            {
                cevap1[i].text = cevap1Sorular[0];
                cevap2[i].text = cevap2Sorular[0];

                kapi1[i].tag = "Yanlis";
                kapi2[i].tag = "Dogru";
            }

        }
    }
}
