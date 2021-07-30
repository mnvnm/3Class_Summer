using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScene : MonoBehaviour
{
    public HudUI m_HudUI;
    public GameUI m_GameUI;
    public ItemSpawnManager m_ItemMng;
    public BattleFSM BtlFSM = new BattleFSM();
    void Awake()
    {
        if (!AssetMgr.Inst.IsInstalled)
            AssetMgr.Inst.Initialize();

        GameMgr.Inst.Initialize();
        GameMgr.Inst.LoadFile();
    }

    void Start()
    {
        GameMgr.Inst.SetGameScene(this);
        BtlFSM.Initialize(OnEnter_Ready, OnEnter_Wave, OnEnter_Game, OnEnter_Result);

        BtlFSM.SetGameState();
        print("시작");
    }

    public void StartGame()
    {
        GameMgr.Inst.InitStart();
    }

    void OnEnter_Ready()
    {
    }
    void OnEnter_Wave()
    {

    }
    void OnEnter_Game()
    {
        m_GameUI.Initialize();
        m_HudUI.Initialize();
        print("게임, 허드 이니셜라이즈 성공");
    }
    void OnEnter_Result()
    {
    }

    void OnCallbackInvoke_Start()
    {
        BtlFSM.SetGameState();
    }

    public void ExitGame()
    {
        BtlFSM.SetNoneState();
        SceneManager.LoadScene("MainScene");
    }

    public void OnCallBack_MsgBoxExit(bool isOk)
    {
        if(isOk)
        {
            ExitGame();
        }
        else
        {

        }
    }
    void Update()
    {
        if (BtlFSM != null)
        {
            BtlFSM.OnUpdate();
            GameMgr.Inst.OnUpdate(Time.deltaTime);
        }
    }
}
