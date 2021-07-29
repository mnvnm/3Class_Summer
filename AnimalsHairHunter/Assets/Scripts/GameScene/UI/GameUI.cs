using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public GameObject m_AnimalPrefab;
    private Animals m_Animal;
    public GameObject m_BossObj;
   [HideInInspector] public Boss m_Boss;

    public SpriteRenderer m_Bg;

    public List<AssetAnimals> m_StageAnimals = new List<AssetAnimals>(); // 해당 스테이지에서만 나오는 몬스터의 리스트

    private void Start()
    {
        m_Boss = m_BossObj.GetComponent<Boss>();
    }
    public void Initialize()
    {
        m_StageAnimals.Clear();
           Sprite kSprite = Resources.Load(GameMgr.Inst.m_gameInfo.m_AssetStage.m_StagePath, typeof(Sprite)) as Sprite;
        m_Bg.sprite = kSprite;
        m_BossObj.gameObject.SetActive(false);
        m_Boss.Initialize();
        Sprite kBossSprite = Resources.Load(GameMgr.Inst.m_gameInfo.m_AssetBoss.m_BossPath, typeof(Sprite)) as Sprite;
        m_BossObj.GetComponent<SpriteRenderer>().sprite = kBossSprite;
        CreateAnimal();

    }
    private void Update()
    {
        if(GameMgr.Inst.m_GameScene.BtlFSM.IsBossState())
            BossUpdate();

        AnimalUpdate();

        if (Input.GetKey(KeyCode.F1))
        {
            Click();
        }
        if (Input.GetKey(KeyCode.Q))
        {
            GameMgr.Inst.m_gameInfo.m_Hair += 100;
        }
        if (Input.GetKey(KeyCode.W))
        {
            GameMgr.Inst.m_gameInfo.m_Gold += 100;
        }
    }
    void AnimalUpdate()
    {
        if (GameMgr.Inst.m_GameScene.BtlFSM.IsGameState())
        {
            if (IsAnimal())
            {
                if (m_Animal.Hp <= 0 && m_Animal.bIsAlive)
                {
                    Death();
                    return;
                }

                double Percent = m_Animal.Hp / m_Animal.MaxHp;
                if (Percent <= 0.6f)
                {
                    Sprite kSprite = Resources.Load(string.Format(m_StageAnimals[m_Animal.Id].m_AnimalsPath + "_wounded1"), typeof(Sprite)) as Sprite;
                    m_Animal.GetComponent<SpriteRenderer>().sprite = kSprite;
                }
                if (Percent <= 0.3f)
                {
                    Sprite kSprite = Resources.Load(string.Format(m_StageAnimals[m_Animal.Id].m_AnimalsPath + "_wounded2"), typeof(Sprite)) as Sprite;
                    m_Animal.GetComponent<SpriteRenderer>().sprite = kSprite;
                }
            }
            else
            {
                CreateAnimal();
            }
        }

        else if (GameMgr.Inst.m_GameScene.BtlFSM.IsWaveState())
        {
            if (IsAnimal())
                Death();
        }
    }

    void BossUpdate()
    {
        int iStage = GameMgr.Inst.m_saveInfo.m_LastStage;
        AssetBoss kAssBoss = AssetMgr.Inst.GetAssetBoss(iStage);
        double Percent = m_Boss.HP / kAssBoss.m_BossHP;
        if(Percent <= 0.6f)
        {
            Sprite kSprite = Resources.Load(string.Format(kAssBoss.m_BossPath + "_wounded1"), typeof(Sprite)) as Sprite;
            m_Boss.GetComponent<SpriteRenderer>().sprite = kSprite;
        }
        if(Percent <= 0.3f)
        {
            Sprite kSprite = Resources.Load(string.Format(kAssBoss.m_BossPath + "_wounded2"), typeof(Sprite)) as Sprite;
            m_Boss.GetComponent<SpriteRenderer>().sprite = kSprite;
        }
        if (m_Boss.HP <= 0)
        {
            m_Boss.Death();
        }
    }

    void Death()
    {
        Sprite kSprite = Resources.Load(string.Format(m_StageAnimals[m_Animal.Id].m_AnimalsPath + "_wounded3"), typeof(Sprite)) as Sprite;
        m_Animal.GetComponent<SpriteRenderer>().sprite = kSprite;
        m_Animal.Death();
        m_Animal = null;
    }
    //동물 관련
    public bool IsAnimal()
    {
        return m_Animal != null ? true : false;
    }

    public void CreateAnimal()
    {
        if(GameMgr.Inst.m_GameScene.BtlFSM.IsGameState() )
        {
            if (IsAnimal()) return;

            GameObject go = Instantiate(m_AnimalPrefab, m_AnimalPrefab.GetComponentInParent<Transform>());
            go.gameObject.SetActive(true);
            Animals kAnimal = go.GetComponent<Animals>();

            for (int i = 0; i < AssetMgr.Inst.m_AssetAnimals.Count; i++)
            {
                AssetAnimals kAsset = AssetMgr.Inst.GetAssetAnimal(i + 1);
                if (kAsset.m_StageId == GameMgr.Inst.m_saveInfo.m_LastStage)
                {
                    m_StageAnimals.Add(kAsset);
                }
            }

            int nRandom = Random.Range(0, m_StageAnimals.Count);
            kAnimal.MaxHp = m_StageAnimals[nRandom].m_AnimalsHp;
            kAnimal.name = m_StageAnimals[nRandom].m_AnimalsName;
            kAnimal.Id = nRandom;
            kAnimal.GetComponent<SpriteRenderer>().sprite = Resources.Load(m_StageAnimals[nRandom].m_AnimalsPath, typeof(Sprite)) as Sprite;

            m_Animal = kAnimal;
            m_Animal.Initialize();
        }
    }
    public void Click()//클릭 할때 이벤트 함수(클릭마다 들어옴)
    {
        GameInfo kGameInfo = GameMgr.Inst.m_gameInfo;
        if (IsAnimal() && GameMgr.Inst.m_GameScene.BtlFSM.IsGameState() && m_Animal.Hp > 0)
        {
            if (kGameInfo.m_FeverTickCount <= 0)
                kGameInfo.m_FeverCurGage += 1 * kGameInfo.m_UpgradeFeverGage_LV * 1.125f;
    
            //치명타 확률과 처리
            float a = 0;
            a = Random.Range(0.0f, 101.0f);

            if (a <= kGameInfo.CriticalProb())
            {
                kGameInfo.m_Hair += kGameInfo.GetHair() * (int)kGameInfo.Critical();
                m_Animal.Hp -= kGameInfo.GetHair() * kGameInfo.Critical();

                GameMgr.Inst.m_GameScene.m_HudUI.m_HudClickDlg.SpawnCritical();

                Debug.Log(a + " 크리티컬 터짐");
            }
            else
            {
                kGameInfo.m_Hair += kGameInfo.GetHair();
                m_Animal.Hp -= kGameInfo.GetHair();
                return;
            }
            if (kGameInfo.m_FeverTickCount > 0)
            {
                //피버 타임때 클릭시
                kGameInfo.m_Hair += kGameInfo.GetHair() * kGameInfo.m_FeverGetHairUp;
                m_Animal.Hp -= kGameInfo.GetHair() * kGameInfo.m_FeverGetHairUp;
            }
            else
            {
                kGameInfo.m_Hair += kGameInfo.GetHair();
                m_Animal.Hp -= kGameInfo.GetHair();
                return;
            }
        }
        else if(GameMgr.Inst.m_GameScene.BtlFSM.IsBossState() && GameMgr.Inst.m_GameScene.m_HudUI.m_BossStateDlg.m_BossKeepTime <= 60)
        {
            if (m_Boss.HP > 0 && GameMgr.Inst.m_GameScene.BtlFSM.IsBossState())
            {
                //치명타 확률과 처리
                float a = 0;
                a = Random.Range(0.0f, 100.0f);

                if (a <= kGameInfo.CriticalProb() )
                {
                    m_Boss.HP -= kGameInfo.GetHair() * kGameInfo.Critical();

                    GameMgr.Inst.m_GameScene.m_HudUI.m_HudClickDlg.SpawnCritical();
                    Debug.Log(a + " 크리티컬 터짐");
                }
                else
                {
                    m_Boss.HP -= kGameInfo.GetHair();
                }

                kGameInfo.m_ClickCount += 1;
                Debug.Log(m_Boss.HP);
            }
        }
    }

    public void BossInit()
    {
        m_Boss.Initialize();
        m_BossObj.GetComponent<SpriteRenderer>().sprite = m_Boss.m_BossSprite;
        m_BossObj.SetActive(false);
    }
}
