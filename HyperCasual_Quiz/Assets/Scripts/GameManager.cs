using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public bool isStarted = false;
    public GameObject startedButton;

    public TextMeshProUGUI soruTextim;

    public TextMeshProUGUI[] cevap1;
    public TextMeshProUGUI[] cevap2;

    public GameObject[] kapi1;
    public GameObject[] kapi2;

    public GameObject settingPanel;
    public bool settingBool = false;
    public int languageOrDil;

    public static GameManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        languageOrDil = PlayerPrefs.GetInt("Dil");
    }

    public void startButton()
    {
        isStarted = true;
        startedButton.SetActive(false);
        GameManager.instance.Sorular(PlayerController.instance.sorularIndis);
    }

    public void nextLevel()
    {
        SceneManager.LoadScene(0);
    }

    public void CloseOrOpenSetting()
    {
        settingBool = !settingBool;
        settingPanel.SetActive(settingBool);
    }

    public void turkceDil()
    {
        //languageOrDil = 0;
        PlayerPrefs.SetInt("Dil", 0);
        languageOrDil = PlayerPrefs.GetInt("Dil");
    }
    public void ingilizceDil()
    {
        //languageOrDil = 1;
        PlayerPrefs.SetInt("Dil", 1);
        languageOrDil = PlayerPrefs.GetInt("Dil");
    }

    public void Sorular(int indis)
    {
        if (indis == 0)
        {
            string[] sorular = { "Türkiye'nin baþkenti", "The Capital of Turkey" };
            soruTextim.text = sorular[languageOrDil];

            string[] cevap1Sorular = { "Ankara", "Ankara" };
            string[] cevap2Sorular = { "Ýstanbul", "Istanbul" };

            for (int i = 0; i < 3; i++)
            {
                cevap1[i].text = cevap1Sorular[languageOrDil];
                cevap2[i].text = cevap2Sorular[languageOrDil];

                kapi1[i].tag = "Dogru";
                kapi2[i].tag = "Yanlis";
            }

        }

        if (indis == 1)
        {
            string[] sorular = { "En zengin kim?", "Who is the richest?" };
            soruTextim.text = sorular[languageOrDil];

            string[] cevap1Sorular = { "Elon Musk", "Elon Musk" };
            string[] cevap2Sorular = { "Mark Zuckerberg", "Mark Zuckerberg" };

            for (int i = 0; i < 3; i++)
            {
                cevap1[i].text = cevap1Sorular[languageOrDil];
                cevap2[i].text = cevap2Sorular[languageOrDil];

                kapi1[i].tag = "Dogru";
                kapi2[i].tag = "Yanlis";
            }

        }

        if (indis == 2)
        {
            string[] sorular = { "2+2 = ?", "2+2 = ?" };
            soruTextim.text = sorular[languageOrDil];

            string[] cevap1Sorular = { "5", "5" };
            string[] cevap2Sorular = { "4", "4" };

            for (int i = 0; i < 3; i++)
            {
                cevap1[i].text = cevap1Sorular[languageOrDil];
                cevap2[i].text = cevap2Sorular[languageOrDil];

                kapi1[i].tag = "Yanlis";
                kapi2[i].tag = "Dogru";
            }

        }
    }
}
