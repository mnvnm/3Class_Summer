using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeverGageUI : MonoBehaviour
{
    public Image m_ImgFeverGage;
    public Image m_animFeverTime;

     private GameInfo kGameInfo;

    public bool bIsFever;

    public void Awake()
    {
    }
    public void Start()
    {
        kGameInfo = GameMgr.Inst.m_gameInfo;
    }

    public void Initialize()
    {
        bIsFever = false;
        ShowFeverGage();
    }
    public void ShowFeverGage()
    {
        if (m_ImgFeverGage.fillAmount >= 0)
            m_ImgFeverGage.fillAmount = kGameInfo.m_FeverCurGage / kGameInfo.m_FeverMaxGage; 
        else
        {
            m_ImgFeverGage.fillAmount = 0;
        }
    }

    private void Update()
    {
        if(kGameInfo.m_FeverCurGage > 0 && GameMgr.Inst.m_GameScene.BtlFSM.IsGameState())
        {
            if(kGameInfo.m_FeverTickCount > 0.1f)
            {
                m_animFeverTime.gameObject.SetActive(true);
                kGameInfo.m_FeverCurGage -= Time.deltaTime * 10;
            }
            else
            {
                m_animFeverTime.gameObject.SetActive(false);
                kGameInfo.m_FeverCurGage -= Time.deltaTime * 4;
            }
        }

        if (kGameInfo.m_FeverCurGage >= 100)
        {
            if (!bIsFever)
            {
                Debug.Log("BGM재생");
                AudioManager.Inst.PlayBGM("Fever10Second", MusicTransition.CrossFade);
                kGameInfo.m_FeverCurGage = 100;
                kGameInfo.m_FeverTickCount = 10;
                bIsFever = true;
            }
        }
        if (kGameInfo.m_FeverCurGage <= 0)
        {
            if (bIsFever)
            {
                Debug.Log("BGM원상복구");
                AudioManager.Inst.PlayBGM("GameBGM", MusicTransition.CrossFade);
                bIsFever = false;
            }
        }
    }
}
