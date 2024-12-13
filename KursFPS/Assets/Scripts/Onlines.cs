using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using Photon.Pun.UtilityScripts;
using System.Collections.Generic;
using UnityEngine;
using Photon.Chat.Demo;
using Photon.Pun.Demo.Asteroids;
using Unity.VisualScripting;
using UnityEngine.UI;
using TMPro;

public class Onlines : MonoBehaviourPunCallbacks
{
    public GameObject odaIslemleriPanel;
    public TMP_InputField roomName;
    public TMP_InputField Oyuncu›sim›nput;
    public GameObject odaOlsuturPanel;
    public bool odaOpenOrClose;
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnConnectedToMaster()
    {
        odaIslemleriPanel.SetActive(true);
    }

    public override void OnJoinedLobby()
    {
        
    }

    public void OnJoinRandomRoomButtonClicked()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        string roomName = "Oda " + Random.Range(100, 90000);
        RoomOptions options = new RoomOptions { MaxPlayers = 4 };

        Hashtable optionss = new Hashtable();
        optionss.Add("Time", 120);
        options.CustomRoomProperties = optionss;

        PhotonNetwork.CreateRoom(roomName, options, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(1);
    }

    public void OnBackButtonClicked()
    {

        if (PhotonNetwork.InLobby)
        {
            PhotonNetwork.LeaveLobby();

        }
        odaIslemleriPanel.SetActive(true);
    }

    public void OnCreateRoomButtonClicked()
    {
        RoomOptions options = new RoomOptions
        {
            MaxPlayers = 4,
        };

        Hashtable optionss = new Hashtable();
        optionss.Add("Time", 120);
        options.CustomRoomProperties = optionss;

        PhotonNetwork.CreateRoom(roomName.text, options, TypedLobby.Default);
    }

    public void OnLoginButtonClicked()
    {
        string playerName = Oyuncu›sim›nput.text;

        if (!playerName.Equals(""))
        {
            PhotonNetwork.LocalPlayer.NickName = playerName;
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            Debug.LogError("Oyuncu isimi yok oyuncu ismi giriniz");
        }
    }

    public void odaOlustur()
    {
        odaOpenOrClose = !odaOpenOrClose;
        odaOlsuturPanel.SetActive(odaOpenOrClose);
    }
}
