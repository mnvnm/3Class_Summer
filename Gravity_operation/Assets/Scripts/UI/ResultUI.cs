using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultUI : MonoBehaviour
{
    private Animator anim = null;
    [SerializeField] Button RestartBtn = null;
    [SerializeField] Button OkayBtn = null;

    [SerializeField] Text KeepTimeTxt = null;
    [SerializeField] Text CostTxt = null;
    void Start()
    {
        RestartBtn.onClick.AddListener(Restart);
        OkayBtn.onClick.AddListener(Okay);
        anim = GetComponent<Animator>();
    }
    void Update()
    {

    }

    void Restart()//다시 시작 할때
    {
        GameMgr.Inst.gameInfo.Stage_KeepTime = 0;
        GameMgr.Inst.m_GameScene.Restart();
        anim.SetBool("Open", false);
    }

    void Okay()// 확인 버튼 누를때
    {
        PlayerPrefs.SetInt(string.Format("Stage_{0}", GameMgr.Inst.m_GameScene.CurStageID),1);// 스테이지 클리어 여부 결정
        GameMgr.Inst.gameInfo.Stage_KeepTime = 0;
        GameMgr.Inst.m_GameScene.OpenLoading();
        SceneManager.LoadScene("LobbyScene"); //씬 이동
    }

    public void OpenResult()
    {
        KeepTimeTxt.text = string.Format("Time : {0:00}:{0:01}", (int)(GameMgr.Inst.gameInfo.Stage_KeepTime / 60), (int)(GameMgr.Inst.gameInfo.Stage_KeepTime % 60));
        CostTxt.text = string.Format("Cost : {0}", GameMgr.Inst.gameInfo.StageUsingMoney);

        anim.SetBool("Open",true);
    }
}
