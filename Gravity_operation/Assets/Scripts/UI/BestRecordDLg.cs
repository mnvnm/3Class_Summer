using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestRecordDLg : MonoBehaviour
{
    [SerializeField] Text CostTxt = null;
    [SerializeField] Text TimeTxt = null;

    float time = 0;
    int cost = 0;

    public void Initialize()
    {
        time = PlayerPrefs.GetFloat(string.Format("StageKeepTime_{0}", StageMgr.Inst.StageIndex));
        cost = PlayerPrefs.GetInt(string.Format("StageCost_{0}", StageMgr.Inst.StageIndex));
    }

    void Start()
    {
        
    }

    void Update()
    {
        time = PlayerPrefs.GetFloat(string.Format("StageKeepTime_{0}", StageMgr.Inst.StageIndex));
        cost = PlayerPrefs.GetInt(string.Format("StageCost_{0}", StageMgr.Inst.StageIndex));

        CostTxt.text = string.Format("cost : {0}", cost);
        TimeTxt.text = string.Format("time : {0:00}:{0:01}", (int)(time / 60),(int)(time % 60));
    }
}
