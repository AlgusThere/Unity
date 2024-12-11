using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour
{
    public GameObject[] Arabalar;
    public GameObject[] ArabalarBoyasi;

    public int aktifAracIndex = 0;
    public int oyundakiAktifCarIndis = 0;

    public float coin;
    public TextMeshProUGUI coinText;

    public float price;

    public TextMeshProUGUI buyText;
    public TextMeshProUGUI priceText;
    public GameObject useText;

    public Material[] renkler;

    public static ShopManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        Time.timeScale = 1;
        oyundakiAktifCarIndis = PlayerPrefs.GetInt("Car");
        Arabalar[oyundakiAktifCarIndis].SetActive(true);
        aktifAracIndex = oyundakiAktifCarIndis;

        PlayerPrefs.SetFloat("Coin", 150);

        PlayerPrefs.GetFloat("Coin");
        coin = PlayerPrefs.GetFloat("Coin");
        coinText.text = coin.ToString();

        if (PlayerPrefs.GetString("One") == "Have")
        {
            Arabalar[0].GetComponent<CarInfo>().carPrice = 0;
            priceText.enabled = false;
            useText.SetActive(true);
            buyText.enabled = false;
        }
        if (PlayerPrefs.GetString("Two") == "Have")
        {
            Arabalar[1].GetComponent<CarInfo>().carPrice = 0;
            priceText.enabled = false;
            useText.SetActive(true);
            buyText.enabled = false;
        }

        foreach (var item in ArabalarBoyasi)
        {
            if (PlayerPrefs.GetString(item.GetComponent<CarInfo>().CarColorName) == "Blue")
            {
                item.GetComponent<MeshRenderer>().materials[0].color = renkler[0].color;
                PlayerPrefs.SetInt("Renk", 0);
            }
            else if (PlayerPrefs.GetString(item.GetComponent<CarInfo>().CarColorName) == "Red")
            {
                item.GetComponent<MeshRenderer>().materials[0].color = renkler[1].color;
                PlayerPrefs.SetInt("Renk", 1);
            }
        }
    }

    public void StartButton()
    {
        SceneManager.LoadScene(1);
    }

    public void Next()
    {
        if (aktifAracIndex != Arabalar.Length - 1)
        {
            Arabalar[aktifAracIndex].SetActive(false);
            aktifAracIndex++;
            Arabalar[aktifAracIndex].SetActive(true);
            price = Arabalar[aktifAracIndex].GetComponent<CarInfo>().carPrice();
            priceText.text = price.ToString();

            if (price == 0)
            {
                priceText.enabled = false;
                useText.SetActive(true);
                buyText.enabled = false;
            }
            else
            {
                priceText.enabled = true;
                useText.SetActive(false);
                buyText.enabled = true;
            }
        }
        else
        {
            Arabalar[aktifAracIndex].SetActive(false);
            aktifAracIndex = 0;
            Arabalar[aktifAracIndex].GetComponent<CarInfo>().carPrice;
            priceText.text = price.ToString();

            if (price == 0)
            {
                priceText.enabled = false;
                useText.SetActive(true);
                buyText.enabled = false;
            }
            else
            {
                priceText.enabled = true;
                useText.SetActive(false);
                buyText.enabled = true;
            }
        }
    }

    public void Back()
    {
        if (aktifAracIndex != 0)
        {
            Arabalar[aktifAracIndex].SetActive(false);
            aktifAracIndex--;
            Arabalar[aktifAracIndex].SetActive(true);
            price = Arabalar[aktifAracIndex].GetComponent<CarInfo>().carPrice();
            priceText.text = price.ToString();

            if (price == 0)
            {
                priceText.enabled = false;
                useText.SetActive(true);
                buyText.enabled = false;
            }
            else
            {
                priceText.enabled = true;
                useText.SetActive(false);
                buyText.enabled = true;
            }
        }
        else
        {
            Arabalar[aktifAracIndex].SetActive(false);
            aktifAracIndex = Arabalar.Length - 1;
            Arabalar[aktifAracIndex].GetComponent<CarInfo>().carPrice;
            priceText.text = price.ToString();

            if (price == 0)
            {
                priceText.enabled = false;
                useText.SetActive(true);
                buyText.enabled = false;
            }
            else
            {
                priceText.enabled = true;
                useText.SetActive(false);
                buyText.enabled = true;
            }
        }
    }

    public void MyBuy(int indis)
    {
        if(useText.activeInHierarchy == true)
        {
            oyundakiAktifCarIndis = aktifAracIndex;
            PlayerPrefs.SetInt("Car", oyundakiAktifCarIndis);
        }
        else if(useText.activeInHierarchy == false)
        {
            if(coin >= price)
            {
                coin -= price;
                PlayerPrefs.SetFloat("Coin", PlayerPrefs.GetFloat("Coin") - price);
                
                coinText.text = coin.ToString();

                oyundakiAktifCarIndis = aktifAracIndex;
                Arabalar[aktifAracIndex].GetComponent<CarInfo>().carPrice = 0;
                priceText.text = Arabalar[aktifAracIndex].GetComponent<CarInfo>().carPrice.ToString();

                priceText.enabled = false;
                useText.SetActive(true);
                buyText.enabled = false;

                PlayerPrefs.SetString(Arabalar[aktifAracIndex].GetComponent<CarInfo>().carName, "Have");

                PlayerPrefs.SetInt("Car", oyundakiAktifCarIndis);
            }
        }
    }

    public void renkAyarla(int number)
    {
        if(number == 0)
        {
            ArabalarBoyasi[aktifAracIndex].GetComponent<MeshRenderer>().materials[0].color = renkler[0].color;
            PlayerPrefs.SetString(ArabalarBoyasi[aktifAracIndex].GetComponent<CarInfo>().CarColorName, "Blue");
        }
        else if (number == 1)
        {
            ArabalarBoyasi[aktifAracIndex].GetComponent<MeshRenderer>().materials[0].color = renkler[1].color;
            PlayerPrefs.SetString(ArabalarBoyasi[aktifAracIndex].GetComponent<CarInfo>().CarColorName, "Red");
        }
    }
}
