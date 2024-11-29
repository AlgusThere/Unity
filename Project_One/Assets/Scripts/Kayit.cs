using UnityEngine;
using UnityEngine.UI;

public class Kayit : MonoBehaviour
{
    public Text coinText;
    public int coin;

    public string isim;
    public float puan;
    public Text nameText;
    public Text puanText;

    public InputField isimYazilanYer;

    private void Start()
    {
        //PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + 10);
        coin = PlayerPrefs.GetInt("Coin");
        coinText.text = coin.ToString();

        //PlayerPrefs.SetString("Name", "Nane");
        isim = PlayerPrefs.GetString("Name");
        nameText.text = isim;

        PlayerPrefs.SetFloat("Score", PlayerPrefs.GetFloat("Score") + 10.5f);
        puan = PlayerPrefs.GetFloat("Score");
        puanText.text = puan.ToString();

        if(PlayerPrefs.HasKey("Score"))
        {
            Debug.Log("Evet skor deðeri var.");
        }
        else
        {
            Debug.Log("Hayýr skor deðeri yok.");
        }

        PlayerPrefs.DeleteAll();
        PlayerPrefs.DeleteKey("Score");
    }

    public void kayitEt()
    {
        PlayerPrefs.SetString("Name", isimYazilanYer.text);
        isim = PlayerPrefs.GetString("Name");
        nameText.text = isim;
    }
}
