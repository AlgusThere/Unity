using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviourPunCallbacks
{
    [Header("Giris Paneli")]
    public GameObject GirisPaneli;
    public InputField OyuncuisimInput;

    [Header("Oyun Secim Paneli")]
    public GameObject OyunSecimi;

    [Header("Oda Olusturma Paneli")]
    public GameObject OdaOlusturPaneli;
    public InputField OdaismiInput;
    public InputField MaksOyuncuInput;

    [Header("Random Odaya Giris Paneli")]
    public GameObject RandomodayaGirPaneli;

    [Header("Oda listesi Paneli")]
    public GameObject OdalistePaneli;
    public GameObject OdalistesiContent;
    public GameObject OdalistesiSatirPrefab;
    public Text Genelinfobilgileri;

    [Header("Oda içi Paneli")]
    public GameObject OdaiciPanel;
    public Text Odainfobilgileri;

    public Button OyunaBaslaButon;
    public GameObject OyunculistesiSatirPrefab;

    private Dictionary<string, RoomInfo> OdaCacheList;
    private Dictionary<string, GameObject> OdaListeElemanlari;
    private Dictionary<int, GameObject> OyuncuListeElemanlari;

    [Header("Loading Paneli")]
    public GameObject LoadingPanel;
    public TextMeshProUGUI BaglantiDurumText;

    public void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        OdaCacheList = new Dictionary<string, RoomInfo>();
        OdaListeElemanlari = new Dictionary<string, GameObject>();       
    }

    public override void OnConnectedToMaster()
    {
        LoadingPanel.SetActive(false);
        BaglantiDurumText.text = "Bağlandı";
        SetActivePanel(OyunSecimi.name);

    }

    public override void OnJoinedLobby()
    {
        OdaCacheList.Clear();
        ClearRoomListView();
        BaglantiDurumText.text = "Lobby'e bağlanılıyor";
    }

    public override void OnLeftLobby()
    {
        OdaCacheList.Clear();
        ClearRoomListView();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        SetActivePanel(OyunSecimi.name);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        SetActivePanel(OyunSecimi.name);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        string roomName = "Oda " + Random.Range(100, 90000);
        RoomOptions options = new RoomOptions { MaxPlayers = 6 };
        Hashtable optionss = new Hashtable();
        optionss.Add("Time", 120);
        options.CustomRoomProperties = optionss;
        PhotonNetwork.CreateRoom(roomName, options, null);
    }

    public override void OnJoinedRoom()
    {

        OdaCacheList.Clear();

        SetActivePanel(OdaiciPanel.name);

        if (OyuncuListeElemanlari == null)
        {
            OyuncuListeElemanlari = new Dictionary<int, GameObject>();

        }

        foreach (Player p in PhotonNetwork.PlayerList)
        {
            GameObject entry = Instantiate(OyunculistesiSatirPrefab);
            entry.transform.SetParent(OdaiciPanel.transform);
            entry.transform.localScale = Vector3.one;

            entry.GetComponent<PlayerListEntry>().Initialize(p.ActorNumber, p.NickName);


            Odainfobilgileri.text = "Odanın Adı : " + PhotonNetwork.CurrentRoom.Name + " | Odayı kuran kişi :  " + PhotonNetwork.MasterClient.NickName + " | Mevcut oyuncu : " + PhotonNetwork.CurrentRoom.PlayerCount + " | Maksimum oyuncu : " + PhotonNetwork.CurrentRoom.MaxPlayers;

            if (p.CustomProperties.TryGetValue("IsPlayerReady", out object isPlayerReady))
            {
                entry.GetComponent<PlayerListEntry>().SetPlayerReady((bool)isPlayerReady);

            }

            OyuncuListeElemanlari.Add(p.ActorNumber, entry);

        }

        OyunaBaslaButon.gameObject.SetActive(CheckPlayersReady());

        Hashtable props = new Hashtable
        {
            {"PlayerLoadedLevel", false }

        };
        PhotonNetwork.LocalPlayer.SetCustomProperties(props);


    }

    public override void OnLeftRoom()
    {
        SetActivePanel(OyunSecimi.name);

        foreach (GameObject entry in OyuncuListeElemanlari.Values)
        {
            Destroy(entry.gameObject);
        }
        OyuncuListeElemanlari.Clear();
        OyuncuListeElemanlari = null;

    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {

        GameObject entry = Instantiate(OyunculistesiSatirPrefab);
        entry.transform.SetParent(OdaiciPanel.transform);
        entry.transform.localScale = Vector3.one;

        entry.GetComponent<PlayerListEntry>().Initialize(newPlayer.ActorNumber, newPlayer.NickName);
       
        OyuncuListeElemanlari.Add(newPlayer.ActorNumber, entry);
        OyunaBaslaButon.gameObject.SetActive(CheckPlayersReady());
        Odainfobilgileri.text = "Odanın Adı : " + PhotonNetwork.CurrentRoom.Name + " | Odayı kuran kişi :  " + PhotonNetwork.MasterClient.NickName + " | Mevcut oyuncu : " + PhotonNetwork.CurrentRoom.PlayerCount + " | Maksimum oyuncu : " + PhotonNetwork.CurrentRoom.MaxPlayers;
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Destroy(OyuncuListeElemanlari[otherPlayer.ActorNumber].gameObject);
        OyuncuListeElemanlari.Remove(otherPlayer.ActorNumber);
        OyunaBaslaButon.gameObject.SetActive(CheckPlayersReady());
        Odainfobilgileri.text = "Odanın Adı : " + PhotonNetwork.CurrentRoom.Name + " | Odayı kuran kişi :  " + PhotonNetwork.MasterClient.NickName + " | Mevcut oyuncu : " + PhotonNetwork.CurrentRoom.PlayerCount + " | Maksimum oyuncu : " + PhotonNetwork.CurrentRoom.MaxPlayers;
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {

        if (PhotonNetwork.LocalPlayer.ActorNumber  == newMasterClient.ActorNumber)
        {
            OyunaBaslaButon.gameObject.SetActive(CheckPlayersReady());

        }
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (OyuncuListeElemanlari == null)
        {
            OyuncuListeElemanlari = new Dictionary<int, GameObject>();

        }

        if (OyuncuListeElemanlari.TryGetValue(targetPlayer.ActorNumber, out GameObject entry))
        {
            if (changedProps.TryGetValue("IsPlayerReady" , out object isPlayerReady))
            {
                entry.GetComponent<PlayerListEntry>().SetPlayerReady((bool) isPlayerReady);

            }
        }

        OyunaBaslaButon.gameObject.SetActive(CheckPlayersReady());
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        ClearRoomListView();
        UpdateCachedRoomList(roomList);
        UpdateRoomListView();
    }

    public void OnBackButtonClicked()
    {
       
        if (PhotonNetwork.InLobby)
        {
            PhotonNetwork.LeaveLobby();

        }
        SetActivePanel(OyunSecimi.name);
    }

    public void OnCreateRoomButtonClicked()
    {
        string roomName = OdaismiInput.text;
        roomName = (roomName.Equals(string.Empty)) ? "Oda " + Random.Range(100, 90000) : roomName;

        byte.TryParse(MaksOyuncuInput.text, out byte maxPlayers);
        maxPlayers = (byte)Mathf.Clamp(maxPlayers, 2, 8);
        
        
        RoomOptions options = new RoomOptions {  
            MaxPlayers = maxPlayers,
        };

        Hashtable optionss = new Hashtable();
        optionss.Add("Time", 120);
        options.CustomRoomProperties = optionss;

        PhotonNetwork.CreateRoom(roomName, options, TypedLobby.Default);

    }

    public void OnJoinRandomRoomButtonClicked()
    {
        SetActivePanel(RandomodayaGirPaneli.name);
        PhotonNetwork.JoinRandomRoom();
    }

    public void OnLeaveGameButtonClicked()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void OnLoginButtonClicked()
    {
        string playerName = OyuncuisimInput.text;

        if (!playerName.Equals(""))
        {
            PhotonNetwork.LocalPlayer.NickName = playerName;
            PhotonNetwork.ConnectUsingSettings();
            LoadingPanel.SetActive(true);
            BaglantiDurumText.text = "Bağlanıyor";
        }
        else
        {
            Debug.LogError("Oyuncu isimi tanımsız");
        }
    }

    public void OnRoomListButtonClicked()
    {      

        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();

        }
        SetActivePanel(OdalistePaneli.name);

        InvokeRepeating("istatistikbak",0,3);
    }

    void istatistikbak()
    {
        if (OdalistePaneli.activeSelf)
        {
            Genelinfobilgileri.text = "Toplam oda sayısı : " + PhotonNetwork.CountOfRooms + " | Odalardaki oyuncu sayısı : " + PhotonNetwork.CountOfPlayersInRooms + " | Oda arayan oyuncu sayısı : " + PhotonNetwork.CountOfPlayersOnMaster + " | Toplam oyuncu sayısı : " + PhotonNetwork.CountOfPlayers;

        }else
        {
            CancelInvoke("istatistikbak");
        }
    }

    public void OnStartGameButtonClicked()
    {
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.CurrentRoom.IsVisible = false;
        PhotonNetwork.LoadLevel(1);
    }

    private bool CheckPlayersReady()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return false;

        }
        foreach (Player p in PhotonNetwork.PlayerList)
        {
            if (p.CustomProperties.TryGetValue("IsPlayerReady", out object isPlayerReady))
            {

                if (!(bool)isPlayerReady)
                    return false;

            }else
            {
                return false;
            }
        }

        return true;

    }

    private void ClearRoomListView()
    {
        foreach (GameObject entry in OdaListeElemanlari.Values)
        {

            Destroy(entry.gameObject);

        }

        OdaListeElemanlari.Clear();

    }

    public void LocalPlayerPropertiesUpdated()
    {
        OyunaBaslaButon.gameObject.SetActive(CheckPlayersReady());
    }

    public void SetActivePanel(string activePanel)
    {
        GirisPaneli.SetActive(activePanel.Equals(GirisPaneli.name));
        OyunSecimi.SetActive(activePanel.Equals(OyunSecimi.name));
        OdaOlusturPaneli.SetActive(activePanel.Equals(OdaOlusturPaneli.name));
        RandomodayaGirPaneli.SetActive(activePanel.Equals(RandomodayaGirPaneli.name));
        OdalistePaneli.SetActive(activePanel.Equals(OdalistePaneli.name));
        OdaiciPanel.SetActive(activePanel.Equals(OdaiciPanel.name));
    }

    private void UpdateCachedRoomList(List<RoomInfo> roomList)
    {

        foreach (RoomInfo info in roomList)
        {
            if (!info.IsOpen || !info.IsVisible || info.RemovedFromList)
            {
                if (OdaCacheList.ContainsKey(info.Name))
                {
                    OdaCacheList.Remove(info.Name);
                }
                continue;
            }

            if (OdaCacheList.ContainsKey(info.Name))
            {
                OdaCacheList[info.Name] = info;
            }

            else
            {
                OdaCacheList.Add(info.Name, info);
            }
        }

    }

    private void UpdateRoomListView()
    {

        foreach (RoomInfo info in OdaCacheList.Values)
        {
            GameObject entry = Instantiate(OdalistesiSatirPrefab);
            entry.transform.SetParent(OdalistesiContent.transform);
            entry.transform.localScale = Vector3.one;
            entry.GetComponent<RoomListEntry>().Initialize(info.Name, (byte)info.PlayerCount, (byte)info.MaxPlayers,(info.MaxPlayers == info.PlayerCount));
            OdaListeElemanlari.Add(info.Name, entry);
        }


    }
}
