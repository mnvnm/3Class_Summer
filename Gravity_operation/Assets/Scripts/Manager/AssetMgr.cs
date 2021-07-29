using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _STR
{
    public const string DTABLE_STAGE = "TableData/Stage";
}
public class CAsset
{
    public int m_id = 0;                // id
}

// 스테이지 정보 파일
public class AssetStage : CAsset
{
    public int m_StageId = 0;                // Stage
    public int m_StageCost = 99999;
    public string m_StagePath = "";
}
public class AssetMgr
{
    static AssetMgr instance = null;
    public static AssetMgr Inst
    {
        get
        {
            if (instance == null)
                instance = new AssetMgr();

            return instance;
        }
    }

    private AssetMgr()
    {
        IsInstalled = false;
    }

    //----------------------------------------------------------
    public bool IsInstalled { get; set; }
    public List<AssetStage> m_AssStages = new List<AssetStage>();

    public void Initialize()
    {
        Initialzie_Stage(_STR.DTABLE_STAGE);
        IsInstalled = true;
    }

    public void Initialzie_Stage(string pathName)
    {
        List<string[]> kDatas = CSVParser.Load(pathName);
        if (kDatas == null)
            return;

        for (int i = 1; i < kDatas.Count; i++)
        {
            string[] aStr = kDatas[i];
            AssetStage kStage = new AssetStage();
            int index = 0;

            kStage.m_id = int.Parse(aStr[index++]);
            kStage.m_StageId = int.Parse(aStr[index++]);
            kStage.m_StageCost = int.Parse(aStr[index++]);
            kStage.m_StagePath = aStr[index++];

            m_AssStages.Add(kStage);
        }
        kDatas.Clear();
    }
}
