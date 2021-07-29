using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public double HP;
    public Sprite m_BossSprite;
    SpriteRenderer m_SpriteRenderer;
    public bool bIsSuccess;
    void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void Initialize()
    {
        int iStage = GameMgr.Inst.m_saveInfo.m_LastStage;
        AssetBoss kAssBoss = AssetMgr.Inst.GetAssetBoss(iStage);
        HP = kAssBoss.m_BossHP;
    }

    public void Death()
    {
        int iStage = GameMgr.Inst.m_saveInfo.m_LastStage;
        AssetBoss kAssBoss = AssetMgr.Inst.GetAssetBoss(iStage);

        HP = 0;
        m_BossSprite = Resources.Load(kAssBoss.m_BossPath + "wounded3",typeof (Sprite)) as Sprite;
        m_SpriteRenderer.sprite = m_BossSprite;
        GameMgr.Inst.m_gameInfo.m_IsSuccess = true;
        GameMgr.Inst.m_saveInfo.SetStage(GameMgr.Inst.m_gameInfo.m_nStage + 1);
        GameMgr.Inst.m_gameInfo.m_nStage = GameMgr.Inst.m_saveInfo.m_LastStage;
        GameMgr.Inst.m_GameScene.BtlFSM.SetResultState();

    }
    void Update()
    {
    }
}
