using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartDlg : MonoBehaviour
{
    [SerializeField] Text StageNameTxt;
    [SerializeField] Text CurScoreTxt;
    [SerializeField] Text CurTimeTxt;

    public MenuDlg menuDlg;

    public void Initialize()
    {
        SetStageName();
    }

    public void Update()
    {
        SetCurScoreText();
        SetCurTimeText();

    }
    public void SetStageName()
    {
        StageNameTxt.text = GameMgr.Inst.m_gameInfo.StageName;
    }

    public void SetCurTimeText()
    {
        if(menuDlg.optionDlg.bIsTimeLimitOnOff == true)
        {
            CurTimeTxt.text = string.Format("{0}분 {1}초", (int)(GameMgr.Inst.m_gameInfo.StageLimitTime / 60),
                                                       (int)(GameMgr.Inst.m_gameInfo.StageLimitTime % 60));

            GameMgr.Inst.m_gameInfo.StageLimitTime -= Time.deltaTime;
        }
        if (menuDlg.optionDlg.bIsTimeLimitOnOff == false)
        {
            CurTimeTxt.text = "-";
        }
    }

    public void SetCurScoreText()
    {
        CurScoreTxt.text = GameMgr.Inst.m_gameInfo.StageCurScore.ToString();
    }
}
