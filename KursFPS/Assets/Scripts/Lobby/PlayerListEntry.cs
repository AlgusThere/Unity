using UnityEngine;
using UnityEngine.UI;
using ExitGames.Client.Photon;
using Photon.Pun;

public class PlayerListEntry : MonoBehaviour
{
    public Text OyuncuAdi;
    public Button OyuncuHazirbuton;
    public GameObject OyuncuSprite;
    private int oyuncuid;
    private bool Hazirmi;

    public void Start()
    {
        if (PhotonNetwork.LocalPlayer.ActorNumber != oyuncuid)
        {
            OyuncuHazirbuton.gameObject.SetActive(false);
        }
        else
        {
            Hashtable props = new Hashtable() { { "IsPlayerReady", Hazirmi } };
            PhotonNetwork.LocalPlayer.SetCustomProperties(props);

            OyuncuHazirbuton.onClick.AddListener(() =>
            {

                Hazirmi = !Hazirmi;
                SetPlayerReady(Hazirmi);

                Hashtable props2 = new Hashtable() { { "IsPlayerReady", Hazirmi } };
                PhotonNetwork.LocalPlayer.SetCustomProperties(props2);

                if (PhotonNetwork.IsMasterClient)
                {
                    FindObjectOfType<GameManager>().LocalPlayerPropertiesUpdated();
                }

            });

        }
        
    }

    public void Initialize(int playerId, string playerName)
    {
        oyuncuid = playerId;
        OyuncuAdi.text = playerName;
    }

    public void SetPlayerReady(bool playerReady)
    {
        OyuncuHazirbuton.GetComponentInChildren<Text>().text = playerReady ? "Oyuncu Hazır" : "Hazır Ver";
        OyuncuSprite.SetActive(playerReady);
    }
}
