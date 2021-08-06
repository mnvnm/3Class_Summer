using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudUI : MonoBehaviour
{
    public EditDlg editDlg = null; // 맵 에딧, 아이템 생성들을 관리하는 스크립트
    public ResultUI resultUI = null; // 결과창 보여주는 스크립트
    public Esc_Dlg escDlg = null; // Esc 눌렀을때 나오는 Dlg
    public ItemInfoMsgDlg MsgBox = null;// 아이템 정보 나오는 메세지 박스
    public Text Gr_scaleTxt = null; // 중력 스케일이 얼만큼인지
    public Text Gr_dirTxt = null; // 중력 방향이 어딘지
    public Text ItemInfoTxt = null; //아이템 정보 설명란
    void Start()
    {
        TextInVisible();
    }
    public void Initialize()// 초기화
    {
        editDlg.Initialize();
    }
    void Update()
    {
        Gr_scaleTxt.text = string.Format("중력 스케일 : {0}", Physics2D.gravity); // 중력 스케일이 얼만큼인지 보여주는 텍스트
        Gr_dirTxt.text = string.Format("중력방향 : {0}", GameMgr.Inst.m_GameScene.m_gameUI.ball.GravityDir); // 중력 방향이 어디인지 보여주는 텍스트
        switch (MsgBox.GetItemIndex())
        {
            case 0:
                ItemInfoTxt.text = "";
                break;
            case 1:
                ItemInfoTxt.text = "이 중력장은 속도 2배 중력장, \n공의 속도를 2배로 바꿔줍니다.";
                break;
            case 2:
                ItemInfoTxt.text = "이 중력장은 무중력 중력장, \n중력을 무중력으로 바꿔줍니다.\n 이 중력장은 한번 더 지나가야지 돌아옵니다.";
                break;
            case 3:
                ItemInfoTxt.text = "이 중력장은 역방향 중력장, \n중력의 위아래 방향을 바꿔줍니다. ";
                break;
            case 4:
                ItemInfoTxt.text = "이 중력장은 블랙홀 중력장, \n공의 속도를 2배로 바꿔줍니다.";
                break;
        }


        if(Input.GetKeyDown(KeyCode.F2))
        {
            TextInVisible();
        }
        if (Input.GetKeyDown(KeyCode.F1))
        {
            TextVisible();
        }
    }
    void TextInVisible()
    {
        Gr_scaleTxt.gameObject.SetActive(false);
        Gr_dirTxt.gameObject.SetActive(false);
    }

    void TextVisible()
    {
        Gr_scaleTxt.gameObject.SetActive(true);
        Gr_dirTxt.gameObject.SetActive(true);
    }
    public void OpenResult() //결과창 열기
    {
        resultUI.OpenResult();
    }
}
