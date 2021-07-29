using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossDlg : MonoBehaviour
{
    public Button m_BtnStage1_Boss;
    void Start()
    {
       
        m_BtnStage1_Boss.onClick.AddListener(OnClicked_Stage_Boss);
    }

    public void Initialize()
    {
    }

    public void OnClicked_Stage_Boss()
    {
        GameMgr.Inst.m_GameScene.m_HudUI.m_Fade.FadeIn();
        GameMgr.Inst.m_GameScene.BtlFSM.SetWaveState();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
