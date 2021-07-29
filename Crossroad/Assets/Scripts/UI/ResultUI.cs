using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultUI : MonoBehaviour
{
    [SerializeField] GameObject Prefabs_ScoreImg;//스코어 점수 프리팹
    [SerializeField] Image SuccessCongImg; // 점수에 따른 Perfect, Excelent, Good 등 성공시 이미지
    [SerializeField] Button RestartBtn; // 재시작 버튼
    [SerializeField] Button BackToMenuBtn; // 돌아가기 버튼
    [SerializeField] Transform Contents_ScoreRect; // 스코어 점수 프리팹 갖다 놓기위한 트랜스폼
    [SerializeField] Sprite[] sprlist; // 0 ~ 9 까지의 스프라이트
    [SerializeField] GameObject Prefabs_Particle; // 성공시 튀어나올 파티클 (버블, 폭죽);
    // Start is called before the first frame update
    void Start()
    {
        RestartBtn.onClick.AddListener(Restart);
        BackToMenuBtn.onClick.AddListener(BackToMenu);
    }

    public void Initialize()
    {
        GameMgr.Inst.m_gameInfo.StageCurScore = 0;
        Close();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Restart()
    {
        GameMgr.Inst.m_gameInfo.StageCurScore = 0;
        SceneManager.LoadScene("GameScene");
    }

    void BackToMenu()
    {
        if (AudioManager.Inst.IsMusicPlaying)
        {
            AudioManager.Inst.IsMusicOn = false;
        }
        PlayerPrefs.SetInt(string.Format("Success_{0}", GameMgr.Inst.m_gameInfo.StageNum), 1);
        Invoke("SetMenuScene",0.25f);
    }

    public void OpenResultUI()
    {
        if (GameMgr.Inst.m_gameInfo.StageCurScore >= GameMgr.Inst.m_gameInfo.StageScore)
        {
            GameMgr.Inst.m_gameInfo.StageScore = GameMgr.Inst.m_gameInfo.StageCurScore;

            PlayerPrefs.SetInt(string.Format("Score_{0}", GameMgr.Inst.m_gameInfo.StageNum), GameMgr.Inst.m_gameInfo.StageScore);
        }
        this.gameObject.SetActive(true);
    }
    public void Open()
    {
        GameObject go = Instantiate(Prefabs_Particle);
        SuccessCongImg.gameObject.SetActive(true);
        if (GameMgr.Inst.m_gameInfo.StageNum < 6)//초급이면
        {
            if(GameMgr.Inst.m_gameInfo.StageCurScore >= 100)
            {
                SuccessCongImg.sprite = Resources.Load("Sprite/Perfect",typeof(Sprite)) as Sprite;
                AudioManager.Inst.PlaySFX("sfx_endgood");
            }
            else if (GameMgr.Inst.m_gameInfo.StageCurScore >= 70)
            {
                SuccessCongImg.sprite = Resources.Load("Sprite/Exelient", typeof(Sprite)) as Sprite;
                AudioManager.Inst.PlaySFX("sfx_endbad");
            }
            else
            {
                SuccessCongImg.sprite = Resources.Load("Sprite/Good", typeof(Sprite)) as Sprite;
                AudioManager.Inst.PlaySFX("sfx_endbad");
            }
        }
        else if (GameMgr.Inst.m_gameInfo.StageNum < 11)//중급이면
        {
            if (GameMgr.Inst.m_gameInfo.StageCurScore >= 150)
            {
                SuccessCongImg.sprite = Resources.Load("Sprite/Perfect", typeof(Sprite)) as Sprite;
                AudioManager.Inst.PlaySFX("sfx_endgood");
            }
            else if (GameMgr.Inst.m_gameInfo.StageCurScore >= 100)
            {
                SuccessCongImg.sprite = Resources.Load("Sprite/Exelient", typeof(Sprite)) as Sprite;
                AudioManager.Inst.PlaySFX("sfx_endbad");
            }
            else
            {
                SuccessCongImg.sprite = Resources.Load("Sprite/Good", typeof(Sprite)) as Sprite;
                AudioManager.Inst.PlaySFX("sfx_endbad");
            }
        }
        else if (GameMgr.Inst.m_gameInfo.StageNum < 16)//고급이면
        {
            if (GameMgr.Inst.m_gameInfo.StageCurScore >= 200)
            {
                SuccessCongImg.sprite = Resources.Load("Sprite/Perfect", typeof(Sprite)) as Sprite;
                AudioManager.Inst.PlaySFX("sfx_endgood");
            }
            else if (GameMgr.Inst.m_gameInfo.StageCurScore >= 150)
            {
                SuccessCongImg.sprite = Resources.Load("Sprite/Exelient", typeof(Sprite)) as Sprite;
                AudioManager.Inst.PlaySFX("sfx_endbad");
            }
            else
            {
                SuccessCongImg.sprite = Resources.Load("Sprite/Good", typeof(Sprite)) as Sprite;
                AudioManager.Inst.PlaySFX("sfx_endbad");
            }
        }
        SuccessCongImg.GetComponent<Animator>().Play("Boom");

        CreateStageScore();
        Invoke("InvisibleImg", 1.7f);
        Invoke("OpenResultUI",1.8f);
    }
    void InvisibleImg()
    {
        SuccessCongImg.gameObject.SetActive(false);
    }
    public void Close()
    {
        this.gameObject.SetActive(false);
    }

    public void CreateStageScore()
    {
        int score = GameMgr.Inst.m_gameInfo.StageCurScore;
        do
        {
            int score_idx = score % 10;
            GameObject go = Instantiate(Prefabs_ScoreImg, Contents_ScoreRect);
            go.gameObject.GetComponent<Image>().sprite = sprlist[score_idx];
            go.gameObject.GetComponent<Image>().SetNativeSize();
            go.gameObject.transform.localScale = new Vector3(-1, 1, 1);
            score /= 10;
        } while (score > 0);
    }

    public void SetMenuScene()
    {
        GameMgr.Inst.m_GameScene.fade.FadeIn("MenuScene");
    }
}
