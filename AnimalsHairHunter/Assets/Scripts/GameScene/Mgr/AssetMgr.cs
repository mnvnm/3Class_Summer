using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _STR
{
    public const string DTABLE_BASEPATH = "Assets/Resources/";

    public const string DTABLE_STAGE = "TableData/stage";
    public const string DTABLE_BOSS = "TableData/boss";
    public const string DTABLE_ANIAML = "TableData/animals";
}


public class CAsset
{
    public int m_id = 0;                // id
}

// 스테이지 정보 파일
public class AssetStage : CAsset
{
    public int m_StageId = 0;                // Stage
    public string m_StagePath = "";
}

public class AssetBoss : CAsset
{
    public int m_BossId = 0;                // Boss
    public string m_BossPath = "";
    public double m_BossHP = 0;
}
public class AssetAnimals : CAsset
{
    public int m_AnimalsId = 0;                // Animals
    public string m_AnimalsName = "";
    public string m_AnimalsPath = "";
    public double m_AnimalsHp = 1000;
    public int m_StageId = 1;
}



//public class AssetItem : CAsset
//{
//    public int m_nType = 1;
//    public string m_sPrefabName = "";
//    public float m_fValue;
//    public string m_sDesc = "";
//}


/*
 *  어셋 정보 관리자
 */
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
    public List<AssetBoss> m_AssetBosses = new List<AssetBoss>();
    public List<AssetAnimals> m_AssetAnimals = new List<AssetAnimals>();
    //public List<AssetItem> m_AssItems = new List<AssetItem>();

    public void Initialize()
    {
        Initialzie_Stage(_STR.DTABLE_STAGE);
        Initialzie_Boss(_STR.DTABLE_BOSS);
        Initialzie_Animals(_STR.DTABLE_ANIAML);
        IsInstalled = true;
    }

    public int GetAssetStageCount() {
        return m_AssStages.Count;
    }


    public void Initialzie_Stage( string pathName )
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
            kStage.m_StagePath = aStr[index++];

            m_AssStages.Add(kStage);
        }
        kDatas.Clear();
    }

    public void Initialzie_Boss(string pathName)
    {
        List<string[]> kDatas = CSVParser.Load(pathName);
        if (kDatas == null)
            return;

        for (int i = 1; i < kDatas.Count; i++)
        {
            string[] aStr = kDatas[i];
            AssetBoss kBoss = new AssetBoss();
            int index = 0;

            kBoss.m_id = int.Parse(aStr[index++]);
            kBoss.m_BossPath = aStr[index++];
            kBoss.m_BossHP = double.Parse(aStr[index++]);

            m_AssetBosses.Add(kBoss);
        }
        kDatas.Clear();
    }

    public void Initialzie_Animals(string pathName)
    {
        List<string[]> kDatas = CSVParser.Load(pathName);
        if (kDatas == null)
            return;

        for (int i = 1; i < kDatas.Count; i++)
        {
            string[] aStr = kDatas[i];
            AssetAnimals kAnimals = new AssetAnimals();
            int index = 0;

            kAnimals.m_id = int.Parse(aStr[index++]);
            kAnimals.m_AnimalsName = aStr[index++];
            kAnimals.m_AnimalsPath = aStr[index++];
            kAnimals.m_AnimalsHp = double.Parse(aStr[index++]);
            kAnimals.m_StageId = int.Parse(aStr[index++]);

            m_AssetAnimals.Add(kAnimals);
        }
        kDatas.Clear();
    }
    //public void Initialize_Item( string pathName )
    //{
    //    List<string[]> kDatas = CSVParser.Load(pathName);
    //    if (kDatas == null)
    //        return;
    //
    //    for (int i = 1; i < kDatas.Count; i++)
    //    {
    //        string[] aStr = kDatas[i];
    //        AssetItem kItem = new AssetItem();
    //
    //        int index = 0;
    //
    //        kItem.m_id = int.Parse(aStr[index++]);
    //        kItem.m_nType = int.Parse(aStr[index++]);
    //        kItem.m_sPrefabName = aStr[index++];
    //        kItem.m_fValue = float.Parse(aStr[index++]);
    //        kItem.m_sDesc = aStr[index++];
    //
    //        m_AssItems.Add(kItem);
    //    }
    //    kDatas.Clear();
    //}



    //------------------------------------------------------------------
    public AssetAnimals GetAssetAnimal(int iIndex)
    {
        if (iIndex > 0 && iIndex <= m_AssetAnimals.Count)
        {
            return m_AssetAnimals[iIndex - 1];
        }
        return null;
    }

    public AssetStage GetAssetStage(int iStage)
    {
        if (iStage > 0 && iStage <= m_AssStages.Count)
        {
            return m_AssStages[iStage - 1];
        }
        return null;
    }
    public AssetBoss GetAssetBoss(int iStage)
    {
        if (iStage > 0 && iStage <= m_AssetBosses.Count)
        {
            return m_AssetBosses[iStage - 1];
        }
        return null;
    }

    //------------------------------------------------------------------
    //public AssetItem GetAssetItem(int id)
    //{
    //    if (id > 0 && id <= m_AssItems.Count)
    //    {
    //        return m_AssItems[id - 1];
    //    }
    //    return null;
    //}

}
