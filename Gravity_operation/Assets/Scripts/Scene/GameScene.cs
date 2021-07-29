using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    public BattleFSM btlFSM = new BattleFSM();
    public GameUI m_gameUI = null;
    public HudUI m_hudUI = null;

    public int CurStageID = 0;
    public bool bIsSuccessed = false;
    public bool IsPause = false;
    private bool IsCheatOn = false;

    [SerializeField] Transform StageParent = null;
    [SerializeField] GameObject LoadingObj = null;

    private void Awake()
    {
        CurStageID = StageMgr.Inst.StageIndex;
        AssetMgr.Inst.Initialize();
        GameMgr.Inst.Initialize();
        GameMgr.Inst.SetGameScene(this);
        GameMgr.Inst.gameInfo.Initialize();
    }
    void Start()
    {
        Stage_Init(); 
        btlFSM.Initialize(OnEnter_EditState,OnEnter_WaveState, OnEnter_GameState, OnEnter_ResultState, OnEnter_ActState);
        btlFSM.SetEditState();

        m_gameUI.Initialize();
        m_hudUI.Initialize();
    }
    void Update()
    {
        GameMgr.Inst.gameInfo.OnUpdate();

        if (btlFSM != null)
            btlFSM.OnUpdate();


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale += 1;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            Time.timeScale = 1;
        }
    }

    public void Restart()
    {
        bIsSuccessed = false;
        btlFSM.SetEditState();
    }
    void OnEnter_EditState()
    {
        GameMgr.Inst.camControll.Initialize();
        m_hudUI.editDlg.Restart();
    }

    void OnEnter_WaveState()
    {
        if (bIsSuccessed)// 성공 했으면 결과창
            btlFSM.SetResultState();

        else // 아니면 계속 게임 시작
        {
            btlFSM.SetGameState();
        }
    }

    void OnEnter_GameState()
    {
        m_gameUI.Initialize();
        m_gameUI.Shoot();
    }

    void OnEnter_ResultState()
    {
        m_hudUI.OpenResult();
    }

    public void Stage_Init()
    {
        for (int i = 0; i < AssetMgr.Inst.m_AssStages.Count; i++)
        {
            AssetStage kAss = AssetMgr.Inst.m_AssStages[i]; // 스테이지 하나 불러와서
            if (kAss.m_StageId == CurStageID) // 현재 스테이지냐
            {
                GameObject stage = Instantiate((Resources.Load(kAss.m_StagePath)as GameObject), StageParent);// 스테이지에 맞는 맵 생성
                stage.name = "Stage";
                Transform[] childList = StageParent.GetComponentsInChildren<Transform>(true);
                if (childList != null)
                {
                    for (int j = 1; j < childList.Length; j++)
                    {
                        if (childList[j] != transform)
                        {
                            if (childList[j].tag == "Turret")
                            {
                                m_gameUI.Add_Turret_List(childList[j].GetComponent<Turret>());
                            }
                            if (childList[j].name == "Bg_Map")
                            {
                                // 카메라에 (카메라가 나가지 못하도록 하는) 배경 맵 넣어주기 
                                GameMgr.Inst.camControll.SetBg_Sprite(childList[j].GetComponent<SpriteRenderer>());
                                continue;
                            }
                        }
                    }
                }
                break;
            }
        }
    }

    void OnEnter_ActState()
    {
        m_gameUI.DontShoot();
        GameMgr.Inst.camControll.Shake();
        m_gameUI.ball.Initialize();
    }
    public void CheatOn(bool isOn)// 치트 On / Off
    {
        IsCheatOn = isOn;
    }

    public bool IsCheat() // 치트가 켜져있는지에 대한 반환 값
    {
        return IsCheatOn;
    }

    public void Pause()
    {
        IsPause = !IsPause;
        m_hudUI.escDlg.gameObject.SetActive(IsPause);

        Time.timeScale = IsPause ? 0 : 1;
    }

    public void OpenLoading()
    {
        LoadingObj.SetActive(true);
    }
}
