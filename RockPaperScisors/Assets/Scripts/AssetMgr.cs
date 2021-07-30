using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _STR
{
    public const string DTABLE_BASEPATH = "Assets/Resources/";

    public const string DTABLE_STAGE = "TableData/stage";
    public const string DTABLE_ITEM = "TableData/item";
}


public class CAsset
{
    public int m_id = 0;                // id
}

// 스테이지 정보 파일
public class AssetStage : CAsset
{
    //public int m_id = 0;                // Stage
    public float m_FireDelayTime = 0;   // 총알 발사 지연시간    
    public float m_BulletSpeed = 0;     // 총알 속도
    public float m_StageKeepTime = 0;   // 스테이지 유지시간    
    public int m_PlayerHP = 0;          // 플레이어 체력
    public int m_BulletAttack = 0;      // 총알 공격력(데미지)
    public float m_ItemAppearDelay = 0; // 아이템 생성 지연 시간 또는 간격.
    public float m_ItemKeepTime = 0;    // 아이템 화면 출력 유지 시간    
    public int m_TurretCount = 0;       // 터렛 갯수
}


public class AssetItem : CAsset
{
    public int m_nType = 1;
    public string m_sPrefabName = "";
    public float m_fValue;
    public string m_sDesc = "";
}


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
    public List<AssetItem> m_AssItems = new List<AssetItem>();

    public void Initialize()
    {
        Initialzie_Stage(_STR.DTABLE_STAGE);
        Initialize_Item(_STR.DTABLE_ITEM);
        //Initialzie_Stage(_STR.DTABLE_BASEPATH + _STR.DTABLE_STAGE);
        IsInstalled = true;
    }

    public int GetAsstItemCount() {
        return m_AssItems.Count;
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
            kStage.m_FireDelayTime = float.Parse(aStr[index++]);
            kStage.m_BulletSpeed = float.Parse(aStr[index++]);
            kStage.m_StageKeepTime = int.Parse(aStr[index++]);
            kStage.m_PlayerHP = int.Parse(aStr[index++]);
            kStage.m_BulletAttack = int.Parse(aStr[index++]);
            kStage.m_ItemAppearDelay = float.Parse(aStr[index++]);
            kStage.m_ItemKeepTime = float.Parse(aStr[index++]);
            kStage.m_TurretCount = int.Parse(aStr[index++]);

            m_AssStages.Add(kStage);
        }
        kDatas.Clear();
    }

    public void Initialize_Item( string pathName )
    {
        List<string[]> kDatas = CSVParser.Load(pathName);
        if (kDatas == null)
            return;

        for (int i = 1; i < kDatas.Count; i++)
        {
            string[] aStr = kDatas[i];
            AssetItem kItem = new AssetItem();

            int index = 0;

            kItem.m_id = int.Parse(aStr[index++]);
            kItem.m_nType = int.Parse(aStr[index++]);
            kItem.m_sPrefabName = aStr[index++];
            kItem.m_fValue = float.Parse(aStr[index++]);
            kItem.m_sDesc = aStr[index++];

            m_AssItems.Add(kItem);
        }
        kDatas.Clear();
    }



    //------------------------------------------------------------------
    public AssetStage GetAssetStage(int iStage )
    {
        if( iStage > 0 && iStage <= m_AssStages.Count){
            return m_AssStages[iStage - 1];
        }
        return null;
    }

    //------------------------------------------------------------------
    public AssetItem GetAssetItem(int id)
    {
        if (id > 0 && id <= m_AssItems.Count)
        {
            return m_AssItems[id - 1];
        }
        return null;
    }

}
