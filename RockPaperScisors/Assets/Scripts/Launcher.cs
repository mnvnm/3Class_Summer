using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.Demo.PunBasics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;

public class Launcher : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_InputField m_RoomInput = null; // 멀티 플레이시 방 이름 적는곳
    [SerializeField] TMP_InputField m_NickNameInput = null; // 닉네임 적는곳
    [SerializeField] TMP_Text m_TxtError = null; // 무슨 에러인지 확인하는 텍스트
    [SerializeField] TMP_Text m_TxtRoomName = null; // 보여질 방이름 텍스트
    [SerializeField] Transform RoomListContent = null; // 방 프리팹 생성할 트랜스폼
    [SerializeField] GameObject m_RoomListItemPrefab = null; // 방 프리팹
    [SerializeField] Transform PlayerListContent = null; // 방 내의 플레이어 프리팹 생성 트랜스폼
    [SerializeField] GameObject m_PlayerListItemPrefab = null; // 방 플레이어 프리팹
    [SerializeField] Button StartGameButton = null; // 게임 시작할 버튼
    [SerializeField] Button OkBtn = null; // 확인 버튼
    // Start is called before the first frame update
    void Awake()
    {
        print("Server Connecting");
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 60;
        Screen.SetResolution(1280, 720, false);
        Connect();
    }

    private void Start()
    {
        OkBtn.onClick.AddListener(OnEnterNickname);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && PhotonNetwork.IsConnected) LeaveGame();
    }

    public void Connect() => PhotonNetwork.ConnectUsingSettings();

    public override void OnConnectedToMaster()
    {
        JoinLobby();
        print("Connected to server");
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    public void OnEnterNickname()
    {
        if (m_NickNameInput.text != "")
            PhotonNetwork.NickName = m_NickNameInput.text;

        else
            PhotonNetwork.NickName = "Player" + Random.Range(0, 1000).ToString("0000");

        MenuManager.Inst.OpenMenu("Loading");
        MenuManager.Inst.OpenMenu("Title");
    }

    public void LeaveGame()
    {
        PhotonNetwork.Disconnect();
        Application.Quit();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        print("Disconnect");
    }

    public void JoinLobby() => PhotonNetwork.JoinLobby();
    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(m_RoomInput.text)) return;

        PhotonNetwork.CreateRoom(m_RoomInput.text, new RoomOptions { MaxPlayers = 4, PublishUserId = true });
        MenuManager.Inst.OpenMenu("Loading");
    }
    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        MenuManager.Inst.OpenMenu("Loading");
    }
    public void JoinRandomRoom() => PhotonNetwork.JoinRandomRoom();
    public void LeaveRoom() => PhotonNetwork.LeaveRoom();

    public override void OnJoinedLobby()
    {
        MenuManager.Inst.OpenMenu("Nickname");
        print("Connected to Lobby");
    }
    public override void OnCreatedRoom() => print("Room Create Success");
    public override void OnJoinedRoom()
    {
        MenuManager.Inst.OpenMenu("Room");
        m_TxtRoomName.text = PhotonNetwork.CurrentRoom.Name;

        Player[] player = PhotonNetwork.PlayerList;

        foreach (Transform child in PlayerListContent)
        {
            Destroy(child.gameObject);
        }
        RoomManager.Inst.m_PlayerList.Clear();

        for (int i = 0; i < player.Length; i++)
        {
            Instantiate(m_PlayerListItemPrefab, PlayerListContent).GetComponent<PlayerListItem>().SetUp(player[i]);
        }

        StartGameButton.gameObject.SetActive(PhotonNetwork.IsMasterClient);
        print("Room Join Success");
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        MenuManager.Inst.OpenMenu("Error");
        m_TxtError.text = "Room Creation Failed : " + message;
        print("Room Create Failed");
    }
    public override void OnJoinRoomFailed(short returnCode, string message) => print("Room Join Failed");
    public override void OnJoinRandomFailed(short returnCode, string message) => print("Room Random Failed");

    public void Leave_Room()
    {
        PhotonNetwork.LeaveRoom();
        MenuManager.Inst.OpenMenu("Loading");
    }

    public override void OnLeftRoom()
    {
        MenuManager.Inst.OpenMenu("Title");
    }


    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (Transform child in RoomListContent)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList)
                continue;

            Instantiate(m_RoomListItemPrefab, RoomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);

            Debug.Log(roomList[i].Name);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(m_PlayerListItemPrefab, PlayerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
    }

    public void StartGame()
    {
        MenuManager.Inst.OpenMenu("Loading");
        PhotonNetwork.LoadLevel(1);
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        StartGameButton.gameObject.SetActive(PhotonNetwork.IsMasterClient);
    }
}
