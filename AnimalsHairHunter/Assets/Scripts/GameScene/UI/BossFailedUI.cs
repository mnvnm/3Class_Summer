using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossFailedUI : MonoBehaviour
{
    public Animator m_AnimPanalUp;
    public Button m_btnOk;
    void Start()
    {
        m_btnOk.onClick.AddListener(SetReadyState);
    }

    public void Initialize()
    {

    }
    void Update()
    {
    }

    void SetReadyState()
    {
        CloseUI();
        GameMgr.Inst.m_GameScene.BtlFSM.SetReadyState();
    }

    public void CloseUI()
    {
        GameMgr.Inst.m_GameScene.m_HudUI.m_Fade.FadeIn();
        GameMgr.Inst.m_gameInfo.ToSave();
        gameObject.SetActive(false);
    }
    public void OpenUI()
    {
        gameObject.SetActive(true);
        m_AnimPanalUp.SetBool("IsResult", GameMgr.Inst.m_GameScene.BtlFSM.IsResultState());
    }
}
