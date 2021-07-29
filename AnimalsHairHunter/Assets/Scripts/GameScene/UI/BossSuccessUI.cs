using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossSuccessUI : MonoBehaviour
{
    public Animator m_AnimPanalUp;
    public Button m_btnOk;
    public Text m_txtKeepTime;
    public Text m_txtClickCount;
    public Text m_txtReward;
    void Start()
    {
        m_btnOk.onClick.AddListener(SetReadyState);
    }

    public void Initialize()
    {

    }
    void Update()
    {
        if(GameMgr.Inst.m_GameScene.BtlFSM.IsResultState())
        {
            m_txtKeepTime.text = string.Format("남은 시간 : {0:0.00}초",GameMgr.Inst.m_GameScene.m_HudUI.m_BossStateDlg.m_BossKeepTime);
            m_txtClickCount.text = string.Format("클릭 횟수 : {0}번",GameMgr.Inst.m_gameInfo.m_ClickCount);
            GameMgr.Inst.m_gameInfo.m_Reward = (GameMgr.Inst.m_gameInfo.m_ClickCount * 1000) + ((int)GameMgr.Inst.m_GameScene.m_HudUI.m_BossStateDlg.m_BossKeepTime * 100);
            m_txtReward.text = string.Format("보상 : " + GameMgr.Inst.m_gameInfo.m_txtReward);
        }
    }

    void SetReadyState()
    {
        CloseUI();
        GameMgr.Inst.m_GameScene.BtlFSM.SetReadyState();
    }

    public void CloseUI()
    {
        GameMgr.Inst.m_GameScene.m_HudUI.m_Fade.FadeIn();
        GameMgr.Inst.m_gameInfo.m_Hair += GameMgr.Inst.m_gameInfo.m_Reward;
        GameMgr.Inst.m_gameInfo.ToSave();
        gameObject.SetActive(false);
    }
    public void OpenUI()
    {
        gameObject.SetActive(true);
        m_AnimPanalUp.SetBool("IsResult", GameMgr.Inst.m_GameScene.BtlFSM.IsResultState());
    }
}
