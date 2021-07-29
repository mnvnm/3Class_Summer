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
        GameMgr.Inst.m_GameScene.Restart();
        anim.SetBool("Open", false);
    }

    void Okay()// 확인 버튼 누를때
    {
        PlayerPrefs.SetInt(string.Format("Stage_{0}", GameMgr.Inst.m_GameScene.CurStageID),1);// 스테이지 클리어 여부 결정
        GameMgr.Inst.m_GameScene.OpenLoading();
        SceneManager.LoadScene("LobbyScene"); //씬 이동
    }

    public void OpenResult()
    {
        anim.SetBool("Open",true);
    }
}
