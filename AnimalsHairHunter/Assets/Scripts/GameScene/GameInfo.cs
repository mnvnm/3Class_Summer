using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo
{
    public int m_nStage = 1;//현재 스테이지               //저장
    public bool m_IsSuccess;//보스 스테이지 돌파에 성공 했는가?

    public AssetStage m_AssetStage;
    public AssetBoss m_AssetBoss;

    public string m_StrValue = " ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    public string m_txtHair;
    public string m_txtGold;
    public string m_txtReward;
    public string m_txtAutoGold;
    public string m_txtCost_GetHair;
    public string m_txtCost_Critical;
    public string m_txtCost_Prob;
    public string m_txtCost_Fever;
    public string m_txtCost_AutoGold;
    public string m_txtCost_AutoGoldTick;

    public ActorInfo m_ActorInfo = new ActorInfo();

    public const double c_GetHair = 2;//클릭당 얻을 털의 양   const
    public double m_Hair = 0;//현재 가지고 있는 털의 양               //저장
    public double m_GetHair = 2;//클릭당 얻을 털의 양
    public double m_Gold = 0;//현재 가지고 있는 재화의 양               //저장
    public double c_AutoGold = 1;//일정시간이 지나면 얻을 자동 털 획득량    const
    public double m_AutoGold = 1;//일정시간이 지나면 얻을 자동 털 획득량 

    public float c_AutoGoldTick = 10;//위에 말한 일정시간(변하지 않음)   const
    public float m_AutoGoldTick = 10;//위에 말한 일정시간(변하지 않음)
    public float m_AutoGoldTickCount = 0;//일정시간 채워줄 시간 변수
    public float m_CriticalGetHair = 2;//크리티컬 때의 기본 털 얻는 양(변하지 않음)
    public float m_CriticalUp = 0.25f;//크리티컬 업그레이드 할때의 기본 업그레이드 양(변하지 않음)
    public float m_CriticalGetHairProb = 1;//크리티컬 때의 털 얻는 양
    public int m_FeverMaxGage = 100;//피버게이지 맥스값(변하지 않음)
    public float m_FeverCurGage = 0;//피버게이지 현재값
    public float m_FeverTickCount = 0;//피버 타임 지속시간(변하지 않음)
    public int m_FeverGetHairUp = 2;//피버 타임때 얻을 GetHair 증가량(변하지 않음)      

    public int m_UpgradeGetHair_LV = 1;//GetHair업그레이드 레벨               //저장
    public int m_UpgradeCriticalProb_LV = 1;//CriticalProb 업그레이드 레벨               //저장
    public int m_UpgradeCriticalGetHair_LV = 1;//CriticalGetHair 업그레이드 레벨               //저장
    public int m_UpgradeFeverGage_LV = 1;//클릭당 얻을 피버게이지 업그레이드 레벨               //저장
    public int m_UpgradeAutoGold_LV = 1;//오토골드 업그레이드 레벨               //저장
    public int m_UpgradeAutoGoldTick_LV = 1;//오토골드 얻을 시간 업그레이드 레벨               //저장

    public double m_UpgradeGetHair_Cost = 5;//GetHair업그레이드 코스트               
    public double m_UpgradeCriticalProb_Cost = 20;//CriticalProb 업그레이드 코스트               
    public double m_UpgradeCriticalGetHair_Cost = 10;//CriticalGetHair 업그레이드 코스트               
    public double m_UpgradeFeverGage_Cost = 500;//클릭당 얻을 피버게이지 업그레이드 코스트               
    public double m_UpgradeAutoGold_Cost = 5;//오토골드 업그레이드 코스트               
    public double m_UpgradeAutoGoldTick_Cost = 10;//오토골드 얻을 시간 업그레이드 코스트               

    //Const 상수 기존값
    public const double c_UpgGetHair_Cost = 5;//GetHair업그레이드    const
    public const double c_UpgCriticalProb_Cost = 20;//CriticalProb 업그레이드    const
    public const double c_UpgCriticalGetHair_Cost = 10;//CriticalGetHair 업그레이드     const
    public const double c_UpgFeverGage_Cost = 500;//클릭당 얻을 피버게이지 업그레이드     const
    public const double c_UpgAutoGold_Cost = 5;//오토골드 업그레이드 코스트    const
    public const double c_UpgAutoGoldTick_Cost = 10;//오토골드 얻을 시간 업그레이드    const

    public bool bIsFever;
    //
    public int m_ClickCount;
    public double m_Reward;
    //____________________________________________________________________________//

    //public List<ItemInfo> m_ListItemInfo = new List<ItemInfo>();
    public void Init()
    {
        m_ClickCount = 0;
        SaveInfo kSaveInfo = GameMgr.Inst.m_saveInfo;

        m_nStage = kSaveInfo.m_LastStage;
        m_Hair = kSaveInfo.m_Hair;
        m_Gold = kSaveInfo.m_Gold;

        m_UpgradeGetHair_LV = kSaveInfo.m_UpgradeGetHair_LV;
        m_UpgradeCriticalProb_LV = kSaveInfo.m_UpgradeCriticalProb_LV;
        m_UpgradeCriticalGetHair_LV = kSaveInfo.m_UpgradeCriticalGetHair_LV;
        m_UpgradeFeverGage_LV = kSaveInfo.m_UpgradeFeverGage_LV;
        m_UpgradeAutoGold_LV = kSaveInfo.m_UpgradeAutoGold_LV;
        m_UpgradeAutoGoldTick_LV = kSaveInfo.m_UpgradeAutoGoldTick_LV;

        m_UpgradeCriticalProb_Cost = m_UpgradeCriticalProb_LV * c_UpgCriticalProb_Cost + c_UpgCriticalProb_Cost;
        m_UpgradeCriticalGetHair_Cost = c_UpgCriticalGetHair_Cost * m_UpgradeCriticalGetHair_LV + (m_UpgradeCriticalGetHair_Cost / 8);
        m_UpgradeFeverGage_Cost = (int)(m_UpgradeFeverGage_LV * c_UpgFeverGage_Cost + c_UpgFeverGage_Cost);
        m_UpgradeGetHair_Cost = m_UpgradeGetHair_LV * c_GetHair + (m_UpgradeGetHair_Cost / 8);
        m_UpgradeAutoGold_Cost = m_UpgradeAutoGold_LV * c_UpgAutoGold_Cost + (m_UpgradeAutoGold_Cost / 8);
        m_UpgradeAutoGoldTick_Cost = m_UpgradeAutoGoldTick_LV * m_UpgradeAutoGoldTick_Cost + ((c_UpgGetHair_Cost * m_UpgradeAutoGoldTick_LV) / 8);

        Initialize_Stage(m_nStage);
        Initialize_Boss(m_nStage);
        //ntialize_Item();
    }
    public void Initialize_Stage(int nStage)
    {
        m_AssetStage = AssetMgr.Inst.GetAssetStage(nStage);
        //m_ActorInfo.Intialize(m_AssetStage.m_PlayerHP);
    }
    public void Initialize_Boss(int nStage)
    {
        m_AssetBoss = AssetMgr.Inst.GetAssetBoss(nStage);
    }

    public bool IsSuccess()
    {
        return m_IsSuccess;
    }
    //변수들 반환

    //업그레이드의 의한 값들 (공식들)-----
    public double Critical()
    {
        return m_CriticalGetHair + (m_CriticalUp * (m_UpgradeCriticalGetHair_LV - 1));
    }

    public double CriticalProb()
    {
        m_CriticalGetHairProb = m_UpgradeCriticalProb_LV;
        return m_CriticalGetHairProb;
    }

    public double GetHair()
    {
        m_GetHair = m_UpgradeGetHair_LV * c_GetHair;
        return m_GetHair;
    }
    public double AutoGold()
    {
        m_AutoGold = m_UpgradeAutoGold_LV * c_AutoGold * 2;
        return m_AutoGold;
    }

    public float AutoGoldTick()
    {
        m_AutoGoldTick = c_AutoGoldTick - m_UpgradeAutoGoldTick_LV;
        return m_AutoGoldTick;
    }
    //__________________________________//

    //업그레이드 하는 함수들
    public void Upgrade_CriticalProb()//크리티컬 확률 업그레이드 함수
    {
        if (m_Gold >= m_UpgradeCriticalProb_Cost && m_UpgradeCriticalProb_LV < 20)
        {
            AudioManager.Inst.PlayOneShot("업그레이드");
            UpgradeGold(ref m_Gold, m_UpgradeCriticalProb_Cost, ref m_UpgradeCriticalProb_LV);
            m_UpgradeCriticalProb_Cost = m_UpgradeCriticalProb_LV * c_UpgCriticalProb_Cost + c_UpgCriticalProb_Cost;
        }
    }
    public void Upgrade_CriticalGetHair()//크리티컬 GetHair 업그레이드 함수
    {
        if (m_Gold >= m_UpgradeCriticalGetHair_Cost)
        {
            AudioManager.Inst.PlayOneShot("업그레이드");
            UpgradeGold(ref m_Gold, m_UpgradeCriticalGetHair_Cost, ref m_UpgradeCriticalGetHair_LV);
            m_UpgradeCriticalGetHair_Cost = c_UpgCriticalGetHair_Cost * m_UpgradeCriticalGetHair_LV + (m_UpgradeCriticalGetHair_Cost / 8);
        }
    }
    public void Upgrade_FeverGage()
    {
        if (m_Gold >= m_UpgradeFeverGage_Cost && m_UpgradeFeverGage_LV < 5)
        {
            AudioManager.Inst.PlayOneShot("업그레이드");
            UpgradeGold(ref m_Gold, m_UpgradeFeverGage_Cost, ref m_UpgradeFeverGage_LV);
            m_UpgradeFeverGage_Cost = (int)(m_UpgradeFeverGage_LV * c_UpgFeverGage_Cost + c_UpgFeverGage_Cost);
        }
    }
    public void Upgrade_GetHair()
    {
        if (m_Gold >= m_UpgradeGetHair_Cost)
        {
            AudioManager.Inst.PlayOneShot("업그레이드");
            UpgradeGold(ref m_Gold, m_UpgradeGetHair_Cost, ref m_UpgradeGetHair_LV);
            m_UpgradeGetHair_Cost = m_UpgradeGetHair_LV * c_GetHair + (m_UpgradeGetHair_Cost / 8);
        }
    }
    public void Upgrade_AutoGold()
    {
        if (m_Gold >= m_UpgradeAutoGold_Cost)
        {
            AudioManager.Inst.PlayOneShot("업그레이드");
            UpgradeGold(ref m_Gold, m_UpgradeAutoGold_Cost, ref m_UpgradeAutoGold_LV);
            m_UpgradeAutoGold_Cost = m_UpgradeAutoGold_LV * c_UpgAutoGold_Cost + (m_UpgradeAutoGold_Cost / 8);
        }
    }
    public void Upgrade_AutoGoldTick ()
    {
        if (m_Gold >= m_UpgradeAutoGoldTick_Cost && m_UpgradeAutoGoldTick_LV < 9)
        {
            AudioManager.Inst.PlayOneShot("업그레이드");
            UpgradeGold(ref m_Gold, m_UpgradeAutoGoldTick_Cost, ref m_UpgradeAutoGoldTick_LV);
            m_UpgradeAutoGoldTick_Cost = m_UpgradeAutoGoldTick_LV * m_UpgradeAutoGoldTick_Cost + ((c_UpgGetHair_Cost * m_UpgradeAutoGoldTick_LV) / 8);
        }
    }
    //업데이트 관련
    public void OnUpdate()
    {
        if(GameMgr.Inst.m_GameScene.BtlFSM.IsGameState())
        {
            AutoGoldUpdate();
            FeverUpdate();
        }

        SwapValueString(m_Hair, ref m_txtHair);
        SwapValueString(m_Gold, ref m_txtGold);
        SwapValueString(m_Reward, ref m_txtReward);
        SwapValueString(m_AutoGold, ref m_txtAutoGold);
        SwapValueString(m_UpgradeGetHair_Cost, ref m_txtCost_GetHair);
        SwapValueString(m_UpgradeCriticalGetHair_Cost, ref m_txtCost_Critical);
        SwapValueString(m_UpgradeCriticalProb_Cost, ref m_txtCost_Prob);
        SwapValueString(m_UpgradeFeverGage_Cost, ref m_txtCost_Fever);
        SwapValueString(m_UpgradeAutoGold_Cost, ref m_txtCost_AutoGold);
        SwapValueString(m_UpgradeAutoGoldTick_Cost, ref m_txtCost_AutoGoldTick);

        if (Input.GetKey(KeyCode.F2))
            m_UpgradeGetHair_LV += 100;
    }

    void AutoGoldUpdate()
    {
        m_AutoGoldTickCount += Time.deltaTime;
        
        if (AutoGoldTick() < m_AutoGoldTickCount)
        {
            m_AutoGoldTickCount = 0;
            m_Gold += AutoGold();
            GameMgr.Inst.m_GameScene.m_HudUI.m_HudClickDlg.SpawnAutoGoldText();
        }
    }

    void FeverUpdate()
    {
        if (m_FeverTickCount > 0)
        {
            bIsFever = false;
            m_FeverTickCount -= Time.deltaTime;
        }
    }
    public void SwapValueString(double value, ref string result)
    {
        //value 는 치환 할 실질적 변수 ex) m_Hair, m_Gold 등
        //value 를 / 1000 씩 while 문 돌려서 나눔 나눈 값을 배열의 인덱스 값 즉 -> _array[value/ 1000] 어떤 알파벳까지 왔는지 검사.
        //value 를 while문 돌려서 / 1000 씩 하고 나머지를 스트링에 집어 넣음. ex) 676.86 <-
        int countAlphabet = 0;
        double RemainderValue = value;

        while (RemainderValue > 1000)
        {
            RemainderValue /= 1000;
            countAlphabet++;
        }

        result = string.Format("{0:0.00}", RemainderValue) + m_StrValue[countAlphabet];
    }

    // ex) 구매했을때 돈을 10000만큼 깍게 하고싶어
    // _index : 0 // _value : 10000
    // _index : 1 // _valeu : 10

    // ex) 구매했을때 A를 100만큼 깍게 하고싶어
    // _index : 1 // _value : 100

    // ex) 구매했을때 B를 256만큼 깍게 하고싶어
    // _index : 2 // _value : 256
    public void UpgradeGold(ref double gold, double cost, ref int Lv)
    {
        // _index -> 0
        // _value -> 10000

        if (gold >= cost)
        {
            Debug.Log("업그레이드 성공");
            gold -= cost;
            Lv++;
            return;
        }
        else
        {
            Debug.Log("업그레이드 실패!!");
        }
    }
    public void Sell_Hair()//털을 판매할때 들어오는 함수
    {
        if (m_Hair >= 10)
        {
            AudioManager.Inst.PlayOneShot("골드");
            m_Gold += (int)(m_Hair / 10);
            m_Hair = m_Hair % 10;
        }
    }

    public void ToSave()
    {
        SaveInfo kSaveInfo = GameMgr.Inst.m_saveInfo;

        kSaveInfo.m_LastStage = m_nStage;
        kSaveInfo.m_Hair = m_Hair;
        kSaveInfo.m_Gold = m_Gold;

        kSaveInfo.m_UpgradeGetHair_LV = m_UpgradeGetHair_LV;
        kSaveInfo.m_UpgradeCriticalProb_LV = m_UpgradeCriticalProb_LV;
        kSaveInfo.m_UpgradeCriticalGetHair_LV = m_UpgradeCriticalGetHair_LV;
        kSaveInfo.m_UpgradeFeverGage_LV = m_UpgradeFeverGage_LV;
        kSaveInfo.m_UpgradeAutoGold_LV = m_UpgradeAutoGold_LV;
        kSaveInfo.m_UpgradeAutoGoldTick_LV = m_UpgradeAutoGoldTick_LV;

        GameMgr.Inst.m_saveInfo.SaveFile();
    }
}
