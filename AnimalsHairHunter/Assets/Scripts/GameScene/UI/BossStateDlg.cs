using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossStateDlg : MonoBehaviour
{
    public Text m_txTimeCount;
    public Text m_txtBossHp;

    public float m_BossKeepTime;

    void Start()
    {
        
    }

    public void Initialize()
    {
        m_BossKeepTime = 61.0f;
        m_txTimeCount.gameObject.SetActive(false);
        m_txtBossHp.gameObject.SetActive(false);
    }
    void Update()
    {
        if (GameMgr.Inst.m_GameScene.BtlFSM.IsBossState())
        {
            Boss kBoss = GameMgr.Inst.m_GameScene.m_GameUI.m_Boss;
            int iStage = GameMgr.Inst.m_saveInfo.m_LastStage;
            AssetBoss kAssBoss = AssetMgr.Inst.GetAssetBoss(iStage);

            if (m_BossKeepTime > 0)
                m_BossKeepTime -= Time.deltaTime;

            else
            {
                GameMgr.Inst.m_GameScene.BtlFSM.SetResultState();
            }

            if (m_BossKeepTime <= 60)
            {
                m_txTimeCount.gameObject.SetActive(true);
                m_txtBossHp.gameObject.SetActive(true);
                m_txtBossHp.text = string.Format("보스 체력 : {0:0.00}%", (kBoss.HP / kAssBoss.m_BossHP) * 100);
                m_txTimeCount.text = string.Format("남은 시간 : {0:0.00}초", m_BossKeepTime);
            }
        }
        else
        {
            m_txTimeCount.gameObject.SetActive(false);
            m_txtBossHp.gameObject.SetActive(false);
        }
    }
}
