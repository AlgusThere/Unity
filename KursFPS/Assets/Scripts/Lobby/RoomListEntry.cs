using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class RoomListEntry : MonoBehaviour
{
    public Text OdaAdiText;
    public Text OdadakiOyuncuSayisiText;
    public Button GirisButonu;
    public Text OdasifresiText;
    string OdaAdi;
    bool Odadurum;

    public void Start()
    {
        if (!Odadurum)
        {
            GirisButonu.interactable = true;
            GirisButonu.onClick.AddListener(() =>
            {
                if (PhotonNetwork.InLobby)
                {
                    PhotonNetwork.LeaveLobby();
                }
                PhotonNetwork.JoinRoom(OdaAdi);

            });
        }
        else
        {
            GirisButonu.interactable = false;
        }
    }

    public void Initialize(string odaadi, byte MevcutOyuncu, byte MaksimumOyuncu, bool gelendurum)
    {
        Odadurum = gelendurum;
        OdaAdi = odaadi;
        OdaAdiText.text = odaadi;
        OdadakiOyuncuSayisiText.text = MevcutOyuncu + " / " + MaksimumOyuncu;
    }
}
