using UnityEngine;
using UnityEngine.UI;

public class KayitDeneme : MonoBehaviour
{
    public InputField yazi;
    public string deger;
    public Text yaziText;

    private void Start()
    {
        deger = PlayerPrefs.GetString("Yazisi");
        yaziText.text = deger;
    }

    public void yaz()
    {
        PlayerPrefs.SetString("Yazisi",yazi.text);
        deger = PlayerPrefs.GetString("Yazisi");
        yaziText.text = deger;

    }
}
