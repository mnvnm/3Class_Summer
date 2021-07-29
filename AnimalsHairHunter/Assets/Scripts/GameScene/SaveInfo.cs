using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveInfo
{
    public class SStage
    {
        public int m_Stage = 0;

        public SStage(int stage)
        {
            m_Stage = stage;
        }
        public SStage() { }
    }

    //---------------------------------------------------
    public const string DSAVENAME = "SaveDataFile.data";

    public int m_LastStage = 1;          // Last 스테이지

    public double m_Hair = 0;//클릭당 얻을 털의 양               //저장
    public double m_Gold = 0;//현재 가지고 있는 재화의 양               //저장

    public int m_UpgradeGetHair_LV = 1;//GetHair업그레이드 레벨               //저장
    public int m_UpgradeCriticalProb_LV = 1;//CriticalProb 업그레이드 레벨               //저장
    public int m_UpgradeCriticalGetHair_LV = 1;//CriticalGetHair 업그레이드 레벨               //저장
    public int m_UpgradeFeverGage_LV = 1;//클릭당 얻을 피버게이지 업그레이드 레벨               //저장
    public int m_UpgradeAutoGold_LV = 1;//오토골드 업그레이드 레벨               //저장
    public int m_UpgradeAutoGoldTick_LV = 1;//오토골드 얻을 시간 업그레이드 레벨               //저장

    //----------------------------------------------------    

    public void Initialize()
    {
        LoadFile();
    }

    public bool SaveFile()
    {
        FileStream fs = new FileStream(DSAVENAME, FileMode.Create, FileAccess.Write);
        if (fs == null) return false;

        BinaryWriter bw = new BinaryWriter(fs);

        SaveData(bw);

        bw.Close();
        fs.Close();

        return true;
    }

    public bool LoadFile()
    {
        try
        {
            FileStream fs = new FileStream(DSAVENAME, FileMode.Open, FileAccess.Read);
            if (fs == null) return false;

            BinaryReader br = new BinaryReader(fs);

            LoadData(br);

            br.Close();
            fs.Close();

            return true;
        }
        catch (Exception e)
        {
            Debug.Log("Error SaveInfo.LoadFile() - " + e.ToString());
        }
        return false;
    }

    public void SaveData(BinaryWriter bw)
    {
        bw.Write(m_LastStage);
        bw.Write(m_Hair);
        bw.Write(m_Gold);
        bw.Write(m_UpgradeGetHair_LV);
        bw.Write(m_UpgradeCriticalProb_LV);
        bw.Write(m_UpgradeCriticalGetHair_LV);
        bw.Write(m_UpgradeFeverGage_LV);
        bw.Write(m_UpgradeAutoGold_LV);
        bw.Write(m_UpgradeAutoGoldTick_LV);
    }

    public void LoadData(BinaryReader br)
    {
        m_LastStage = br.ReadInt32();
        m_Hair = br.ReadDouble();
        m_Gold = br.ReadDouble();
        m_UpgradeGetHair_LV = br.ReadInt32();
        m_UpgradeCriticalProb_LV = br.ReadInt32();
        m_UpgradeCriticalGetHair_LV = br.ReadInt32();
        m_UpgradeFeverGage_LV = br.ReadInt32();
        m_UpgradeAutoGold_LV = br.ReadInt32();
        m_UpgradeAutoGoldTick_LV = br.ReadInt32();
    }


    public void SetStage(int nStage)
    {
        m_LastStage = nStage;
    }
}
