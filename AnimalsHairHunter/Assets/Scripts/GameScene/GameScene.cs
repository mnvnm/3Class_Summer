using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    public HudUI m_HudUI;
    public GameUI m_GameUI;
    public BattleFSM BtlFSM = new BattleFSM();

    private void Awake()
    {
        GameMgr.Inst.SetGameScene(this);

        AssetMgr.Inst.Initialize();
        GameMgr.Inst.Initialize();
        GameMgr.Inst.m_saveInfo.LoadFile();
    }
    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        BtlFSM.Initialize(OnEnter_ReadyState, OnEnter_GameState, OnEnter_WaveState, OnEnter_BossState, OnEnter_ResultState);
        BtlFSM.SetCallback_GameStateOnExit(OnExitGame);
        BtlFSM.SetReadyState();
    }

    void OnEnter_ReadyState()
    {
        AudioManager.Inst.PlayBGM("GameBGM");
        m_HudUI.m_Fade.FadeOut();
        SetVisibleHud();
        Debug.Log("Ready");
        BtlFSM.SetGameState();
    }
    void OnEnter_GameState()
    {
        GameMgr.Inst.Game_Initialize();
        m_GameUI.Initialize();
        m_HudUI.Initialize();
        Debug.Log("Game");
    }
    void OnEnter_WaveState()
    {
        GameMgr.Inst.m_gameInfo.ToSave();
        Invoke("SetBossState",1.5f);
        Debug.Log("Wave");
    }
    void OnEnter_BossState()
    {
        m_HudUI.m_Fade.FadeOut();
        VisibleBoss();
        SetInvisibleHud();
        Debug.Log("Boss");
    }
    void OnEnter_ResultState()
    {
        VisibleBoss();
        Debug.Log("Result");
        if (GameMgr.Inst.m_gameInfo.m_IsSuccess)
        {
            m_HudUI.m_BossResultUI.OpenUI();

        }

        else
        {
            m_HudUI.m_BossFailedUI.OpenUI();
        }
    }
    void VisibleBoss()
    {
        m_GameUI.m_BossObj.SetActive(true);
    }


    void SetVisibleHud()
    {
        m_HudUI.m_FeverGageUI.gameObject.SetActive(true);
        m_HudUI.m_MenuDlg.VisibleGameObj(true);
        m_HudUI.m_HudClickDlg.VisibleAll();
    }
    void SetInvisibleHud()
    {
        m_HudUI.m_FeverGageUI.gameObject.SetActive(false);
        m_HudUI.m_MenuDlg.VisibleGameObj(false);
        m_HudUI.m_HudClickDlg.InvisibleAll();
    }
    void SetBossState()
    {
        BtlFSM.SetBossState();
    }
    public void OnExitGame()
    {

    }
    private void Update()
    {
        GameMgr.Inst.m_gameInfo.OnUpdate();
        if(BtlFSM != null)
        {
            BtlFSM.OnUpdate();
        }
    }

    private void OnApplicationQuit()
    {
        GameMgr.Inst.m_gameInfo.ToSave();
    }
}
