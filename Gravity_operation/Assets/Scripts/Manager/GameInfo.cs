using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo
{
    public int StageCost;

    public float Stage_KeepTime = 0;
    public int StageUsingMoney = 0;
    public void Initialize()
    {
        for (int i = 0; i < AssetMgr.Inst.m_AssStages.Count; i++)
        {
            AssetStage kAss = AssetMgr.Inst.m_AssStages[i]; // 스테이지 하나 불러와서
            if (kAss.m_StageId == GameMgr.Inst.m_GameScene.CurStageID)
            {
                StageCost = kAss.m_StageCost;
                break;
            }
        }
    }
    public void OnUpdate()
    {
        for (int i = 0; i < AssetMgr.Inst.m_AssStages.Count; i++)
        {
            AssetStage kAss = AssetMgr.Inst.m_AssStages[i]; // 스테이지 하나 불러와서
            if (kAss.m_StageId == GameMgr.Inst.m_GameScene.CurStageID)
            {
                StageUsingMoney = kAss.m_StageCost - StageCost;
                break;
            }
        }
    }
}
