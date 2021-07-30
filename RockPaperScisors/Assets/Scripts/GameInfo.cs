using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo : MonoBehaviour
{
    //public int m_nStage;
    //public bool m_IsSucces;
    //public float m_fDurationTime;
    //
    //public AssetStage m_AssetStage;
    //public List<ItemInfo> m_ListItemInfo = new List<ItemInfo>();
    //public void Init()
    //{
    //    SaveInfo kSaveInfo = GameMgr.Inst.m_saveInfo;
    //    m_nStage = kSaveInfo.m_LastStage;
    //
    //    Intialize_Stage(m_nStage);
    //    Intialize_Item();
    //}
    //public void OnUpdate(float delta)
    //{
    //    m_fDurationTime += delta;
    //}
    //public void Intialize_Stage(int nStage)
    //{
    //    m_AssetStage = AssetMgr.Inst.GetAssetStage(nStage);
    //    m_ActorInfo.Intialize(m_AssetStage.m_PlayerHP);
    //    m_fDurationTime = 0;
    //}
    //public float StageKeepTime { get { return m_AssetStage.m_StageKeepTime; } }
    //public float ItemAppearDelay { get { return m_AssetStage.m_ItemAppearDelay; } }
    //public float ItemKeepTime { get { return m_AssetStage.m_ItemKeepTime; } }
    //public float TurretFireDelay { get { return m_AssetStage.m_FireDelayTime; } }
    //public float BulletSpeed { get { return m_AssetStage.m_BulletSpeed; } }
    //public int BulletDamage { get { return m_AssetStage.m_BulletAttack; } }
    //public int TurretCount { get { return m_AssetStage.m_TurretCount; } }
    //
    //
    //public void Intialize_Item()
    //{
    //    for (int i = 0; i < AssetMgr.Inst.m_AssItems.Count; i++)
    //    {
    //        AssetItem kAss = AssetMgr.Inst.m_AssItems[i];
    //        ItemInfo kInfo = new ItemInfo();
    //        kInfo.Intialize(kAss.m_nType, kAss.m_fValue);
    //        m_ListItemInfo.Add(kInfo);
    //    }
    //}
    //
    //public void AddDamage()
    //{
    //    m_ActorInfo.AddDamage(this.BulletDamage);
    //}
    //public bool IsPlayerDie()
    //{
    //    return m_ActorInfo.IsDie();
    //}
    //
    //public bool IsSuccess()
    //{
    //    return m_IsSucces;
    //}
    //
    //public ItemInfo ActionItem(int nItemId)
    //{
    //    ItemInfo kInfo = GetItemInfo(nItemId);
    //    if (kInfo.m_ItemType == (int)ItemInfo.Type.eHealing)
    //    {
    //        m_ActorInfo.m_PlayerHP += (int)kInfo.m_ItemValue;
    //    }
    //    if (kInfo.m_ItemType == (int)ItemInfo.Type.eExplose)
    //    {
    //
    //    }
    //
    //    return kInfo;
    //}
    //
    //public ItemInfo GetItemInfo(int id)
    //{
    //    if (id > 0 && id <= m_ListItemInfo.Count)
    //    {
    //        return m_ListItemInfo[id - 1];
    //    }
    //    return null;
    //}
    //
    //public int CaluclateScore()
    //{
    //    int curHp = m_ActorInfo.m_PlayerHP;
    //    int maxHp = m_ActorInfo.CalculateMaxHP();
    //    int nScore = (int)((float)(maxHp / curHp) * Config.DMAX_SCORE);
    //
    //    return nScore;
    //}
}
