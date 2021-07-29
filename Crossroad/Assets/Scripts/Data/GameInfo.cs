using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo//여기서 스테이지의 점수, 스테이지의 이름 등을 
{                    //관리및 가지고 있을 데이터 정보 모음집
    public int StageMaxScore; // 최고점수
    public float StageLimitTime; // 제한시간
    public int StageScore; // 점수
    public int StageCurScore; // 다시시작 했을시 임시 저장 점수
    public int StageNum; // 어디 스테이지인지
    public int StageID; // 어디 스테이지인지
    public string StageName; // 스테이지 이름
    public Dictionary<int, bool> bIsSuccessList = new Dictionary<int, bool>();
    public void Init()
    {
        StageCurScore = 0;
        for (int i = 0; i < 15;i++)
        {
            bIsSuccessList.Add(i, false);
        }

        StageCurScore = 0;
        SaveInfo kSaveInfo = GameMgr.Inst.m_saveInfo;
    }

    //업데이트 관련
    public void OnUpdate()
    {

    }
    public void Reset() // 스테이지 다시하기 버튼 눌렀을때 오는곳
    {
        StageCurScore = 0;
        StageName = "";
    }
    public void ToSave()
    {
        SaveInfo kSaveInfo = GameMgr.Inst.m_saveInfo;


        kSaveInfo.SaveFile();
    }
}
