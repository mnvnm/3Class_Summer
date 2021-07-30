using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Awake()
    {
        print("Server Connecting");
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 30;
        Screen.SetResolution(1024, 768, false);
        Connect();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && PhotonNetwork.IsConnected) PhotonNetwork.Disconnect();
    }

    public void Connect() => PhotonNetwork.ConnectUsingSettings();

    public override void OnConnectedToMaster()
    {
        JoinLobby();
        print("Connected to server");
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void Disconnect() => PhotonNetwork.Disconnect();

    public override void OnDisconnected(DisconnectCause cause)
    {
        print("Disconnect");
    }

    public void JoinLobby() => PhotonNetwork.JoinLobby();
    public void CreateRoom()
    {
    }
    public void JoinRoom(RoomInfo info)
    {
    }
    public void JoinRandomRoom() => PhotonNetwork.JoinRandomRoom();
    public void LeaveRoom() => PhotonNetwork.LeaveRoom();

    public override void OnJoinedLobby()
    {
        print("Connected to Lobby");
        PhotonNetwork.NickName = "Player" + Random.Range(0, 1000).ToString("0000");
    }
    public override void OnCreatedRoom() => print("Room Create Success");
    public override void OnJoinedRoom()
    {

        Player[] player = PhotonNetwork.PlayerList;

        for (int i = 0; i < player.Length; i++)
        {
        }
        print("Room Join Success");
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        print("Room Create Failed");
    }
    public override void OnJoinRoomFailed(short returnCode, string message) => print("Room Join Failed");
    public override void OnJoinRandomFailed(short returnCode, string message) => print("Room Random Failed");

    public void Leave_Room()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel("GameScene");
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
    }
}
