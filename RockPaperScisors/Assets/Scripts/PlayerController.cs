using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using Photon.Realtime;
using System.IO;
using TMPro;
using System;

public class PlayerController : MonoBehaviourPunCallbacks, IPunObservable, IDamageable
{

    public float m_moveSpeed;
    //private float m_Stamina = 10.0f;
    //private bool IsRun = true;
    Vector3 moveAmount;
    Rigidbody m_Rigidbody;

    PhotonView PV;
    public bool isConnect;

    public Camera m_Cam;
    public float m_RotSpeed;
    private float m_xRotate = 0.0f; // 내부 사용할 X축 회전량은 별도 정의 ( 카메라 위 아래 방향 )
    private Flashlight m_Flash;
    public bool IsLight;

    const int MaxHealth = 1;
    int curHealth = MaxHealth;

    PlayerMNG playerManager;
    string murderNum;
    Player Murder;
    public float KillCoolDowntime = 30;
    [SerializeField] GameObject m_txtMurderCoolDownTime;

    // Start is called before the first frame update
    private void Awake()
    {
        m_Flash = GetComponentInChildren<Flashlight>();
        PV = GetComponent<PhotonView>();
        m_Rigidbody = GetComponent<Rigidbody>();
        playerManager = PhotonView.Find((int)PV.InstantiationData[0]).GetComponent<PlayerMNG>();

        if (PhotonNetwork.IsMasterClient)
        {
            murderNum = RoomManager.Inst.MurderActorNumber;
            GetMurderID(murderNum);
            for(int i = 0; i < RoomManager.Inst.m_MasterPlayerList.Count;i++)
            {
                if(RoomManager.Inst.m_MasterPlayerList[i].UserId == murderNum)
                {
                    Murder = RoomManager.Inst.m_MasterPlayerList[i];
                    GetMurder(Murder);
                }
            }
        }
    }
    void Start()
    {
        if (!PV.IsMine)
        {
            IsLight = false;
            Destroy(GetComponentInChildren<Camera>());
            m_txtMurderCoolDownTime.GetComponent<Text>().text = "";
        }
        GameMgr.Inst.gameScene.m_GameUI.m_listPC.Add(this);

        if (PV.IsMine)
        {
            GameMgr.Inst.SetID(PhotonNetwork.LocalPlayer.UserId);

            if (Murder == PhotonNetwork.LocalPlayer)
            {
            }
            else
            {
                Destroy(m_txtMurderCoolDownTime);
                Debug.Log("범인 / 이름 : " + Murder.NickName);
            }
        }
    }

    public void Initialize()
    {
    }
    void FixedUpdate()
    {
        m_Rigidbody.MovePosition(m_Rigidbody.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
    }
    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine == false)
        {
            return;
        }
        if (PhotonNetwork.IsConnected)
            isConnect = true;

        if (Input.GetMouseButtonDown(0))
        {
            GetItemRayCast();
        }
        Rot();
        Move();
        KillFunc();
    }
    void KillFunc()
    {
        if(m_txtMurderCoolDownTime != null)
            m_txtMurderCoolDownTime.GetComponent<Text>().text = string.Format("{0}", (int)KillCoolDowntime);

        if (KillCoolDowntime > 0.0f)
        {
            KillCoolDowntime -= Time.deltaTime;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(PhotonNetwork.LocalPlayer.UserId + " 나는야 / 이름 : " + PhotonNetwork.LocalPlayer.NickName);
            Debug.Log("하지만 머더 넘버는? : " + murderNum);

            if (Murder.UserId == murderNum)
                Debug.Log(Murder.UserId + " 가 범인 / 이름 : " + Murder.NickName);

            if (Murder.UserId == PhotonNetwork.LocalPlayer.UserId && KillCoolDowntime <= 0.0f)
                KillRayCast();
        }
    }
    void Rot()
    {
        // 좌우로 움직인 마우스의 이동량 * 속도에 따라 카메라가 좌우로 회전할 양 계산
        float yRotateSize = Input.GetAxis("Mouse X") * m_RotSpeed;
        // 현재 y축 회전값에 더한 새로운 회전각도 계산
        float yRotate = transform.eulerAngles.y + yRotateSize;

        // 위아래로 움직인 마우스의 이동량 * 속도에 따라 카메라가 회전할 양 계산(하늘, 바닥을 바라보는 동작)
        float xRotateSize = -Input.GetAxis("Mouse Y") * m_RotSpeed;
        // 위아래 회전량을 더해주지만 -45도 ~ 80도로 제한 (-45:하늘방향, 80:바닥방향)
        // Clamp 는 값의 범위를 제한하는 함수
        m_xRotate = Mathf.Clamp(m_xRotate + xRotateSize, -45, 80);

        // 카메라 회전량을 카메라에 반영(X, Y축만 회전)
        this.transform.eulerAngles = new Vector3(0, yRotate, 0);
        m_Cam.transform.eulerAngles = new Vector3(m_xRotate, transform.eulerAngles.y, 0);
    }
    void Move()
    {
        m_Rigidbody.velocity = new Vector3(0, m_Rigidbody.velocity.y, 0);

        if (Input.GetKey(KeyCode.W))// 키보드에 스페이스가 눌려져 있을때
            m_Rigidbody.velocity += transform.forward.normalized * m_moveSpeed;
        if (Input.GetKey(KeyCode.S))
            m_Rigidbody.velocity -= transform.forward.normalized * m_moveSpeed;
        if (Input.GetKey(KeyCode.A))
            m_Rigidbody.velocity -= transform.right.normalized * m_moveSpeed;
        if (Input.GetKey(KeyCode.D))
            m_Rigidbody.velocity += transform.right.normalized * m_moveSpeed;

        //질주
        //if (Input.GetKey(KeyCode.LeftShift) && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) && IsRun)
        //{
        //    m_Stamina -= Time.deltaTime * 2;
        //    m_moveSpeed = 6;
        //}
        //else
        //{
        //    m_moveSpeed = 4.0f;
        //
        //    if (m_Stamina < 10.0f || !IsRun)
        //    {
        //        m_Stamina += Time.deltaTime;
        //    }
        //}
        //if (m_Stamina <= 0.0f)
        //{
        //    IsRun = false;
        //}
        //
        //if(m_Stamina >= 9.999f && !IsRun)
        //{
        //    IsRun = true;
        //}
    }

    //서버에 변수 동기화 해주는 함수 포톤 시리얼라이즈 뷰
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
        }
        else
        {
        }
    }
    //_______________________________//

    private void OnCollisionEnter(Collision collision)
    {
        //장애물 충돌시 벽올라타는것, 뚫고 지나가는것 등등 막아주는거//
        if (collision.gameObject.tag == "Obstacle")
        {
            m_Rigidbody.velocity = new Vector3(0, 0, 0);
        }
        //________________________
    }
    public void Killed(int damage)
    {
        PV.RPC("RPC_Killed", RpcTarget.All, damage);
    }

    [PunRPC]//누가 맞았는감 내가 맞았는감?
    public void RPC_Killed(int damage)
    {
        if (PV.IsMine == false)
            return;

        curHealth -= damage;
        if (curHealth <= 0)
        {
            playerManager.Die();
        }
    }

    //공격할 ㅗ브젝트 검색
    void KillRayCast()
    {
        Ray ray = m_Cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        ray.origin = m_Cam.transform.position;
        if (Physics.Raycast(ray, out RaycastHit hit, 5f))
        {
            print(hit.collider.gameObject.name);
            if (hit.collider.tag == "Player")
            {
                hit.collider.gameObject.GetComponent<IDamageable>()?.Killed((int)5);
                KillCoolDowntime = 1.0f;
            }
        }
    }

    //머더의 UserID 가져오는 것//
    void GetMurderID(string ID)
    {
        if(PhotonNetwork.IsMasterClient)
        {
            PV.RPC("RPC_GetID",RpcTarget.Others,ID);
        }
    }
    [PunRPC]
    void RPC_GetID(string ID)
    {
        murderNum = ID;
    }
    //___________________________//
    //누가 머더인지 가져오는것//
    void GetMurder(Player player)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PV.RPC("RPC_GetMurder", RpcTarget.Others, player);
        }
    }
    [PunRPC]
    void RPC_GetMurder(Player player)
    {
        Murder = player;
    }
    //___________________________//
    //누가 불이 켜졌고 꺼졌나 찾는 것//
    public void OnAndOffLight()
    {
        string str = PhotonNetwork.LocalPlayer.UserId;
        int a = m_Flash.b_LightEnabled ? 1 : 0;
        object[] param = new object[2];
        param[0] = str;
        param[1] = a;
        PV.RPC("RPC_m_FlashLightOnAndOff", RpcTarget.MasterClient, param);
    }
    [PunRPC]
    void RPC_m_FlashLightOnAndOff(object[] param)
    {
        string id = (string)param[0];
        bool bTrue = ((int)param[1] == 1) ? true : false;

        if (PhotonNetwork.IsMasterClient)
        {
            PV.RPC("RPC_m_FlashLightOnAndOff", RpcTarget.Others, param);
        }

        for (int i = 0; i < GameMgr.Inst.gameScene.m_GameUI.m_listPC.Count; i++)
        {
            if (GameMgr.Inst.gameScene.m_GameUI.m_listPC[i].photonView.Owner.UserId == id)
            {
                GameMgr.Inst.gameScene.m_GameUI.m_listPC[i].IsLight = bTrue;
                break;
            }
        }
    }
    //아이템 주울 레이캐스트//
    void GetItemRayCast()
    {
        Ray ray = m_Cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        ray.origin = m_Cam.transform.position;
        if (Physics.Raycast(ray, out RaycastHit hit, 5f))
        {
            if (hit.collider.tag == "Key")
            {
                GetItem(hit.collider.GetComponent<Key>().ID);
                print("어떤 아이템인가 : " + hit.collider.GetComponent<Key>().ID + " 란 물건인데 무어신고?");

                print("어떤 아이템인가 : " + hit.collider.name);
                GetLessCount(GameMgr.Inst.gameScene.m_ItemMng.GottedItemCount);
            }
        }
    }
    //_______________________________//

    //어떤 아이템을 찾았나//
    void GetItem(int id)
    {
        PV.RPC("RPC_GetItem",RpcTarget.MasterClient, id);
    }
    [PunRPC]
    void RPC_GetItem(int id)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            for(int i = 0; i < GameMgr.Inst.gameScene.m_GameUI.m_ListKey.Count;i++)
            {
                if(GameMgr.Inst.gameScene.m_GameUI.m_ListKey[i].ID == id)
                {
                    PhotonNetwork.Destroy(GameMgr.Inst.gameScene.m_GameUI.m_ListKey[i].gameObject);
                }
            }
        }
    }
    //_______________________________//

    //얻은게 키 재료라면 ?????//
    void GetLessCount(int count)
    {
        PV.RPC("RPC_GetLessCount", RpcTarget.MasterClient, count);
    }
    [PunRPC]
    void RPC_GetLessCount(int count)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            count++;
            PV.RPC("RPC_GetLessCount", RpcTarget.Others, count);
        }
        GameMgr.Inst.gameScene.m_ItemMng.GottedItemCount = count;
    }
    //_______________________________//
}
