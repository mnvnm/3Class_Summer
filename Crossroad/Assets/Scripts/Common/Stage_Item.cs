using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Stage_Item : MonoBehaviour
{
    [SerializeField] string Stage_Name = "초급 - 1";// 스테이지가 어디인지
    [SerializeField] string Stage_GameName = "그리스도의 향기";//게임 스테이지 네임
    [SerializeField] string Stage_Paragraph = "고후 2장 15절";//몇장 몇절 말씀인지
    [SerializeField] string Stage_Timelimit = "4분 0초";//제한시간
    [SerializeField] Text NameTxt;
    [SerializeField] Text GameNameTxt;
    [SerializeField] Text ParagraphTxt;
    [SerializeField] Text TimeLimitTxt;
    [SerializeField] Text ScoreTxt;
    [SerializeField] Text MaxScoreTxt;
    [SerializeField] GameObject SuccessImg;
    public int MaxScore;//최대 점수
    public int Score;//점수             저장해야할 변수
    public bool bIsSuccess = false;//게임을 깨었는가? 에대한 변수            저장해야할 변수
    public float KeepTime = 240;//게임 제한시간 변수
    public int StageNumber;//어느 스테이지 인지
    public int StageID;//초급에 몇번째, 중급에 몇번째인지 등
    // Start is called before the first frame update
    void Start()
    {
        NameTxt.text = Stage_Name;
        GameNameTxt.text = Stage_GameName;
        ParagraphTxt.text = Stage_Paragraph;
        TimeLimitTxt.text = string.Format("{0}분 {1}초",(int)(KeepTime / 60),(int)KeepTime % 60);
        MaxScoreTxt.text = string.Format("만점 {0}", MaxScore);

        this.gameObject.GetComponent<Button>().onClick.AddListener(onClick_StageItem);
        SetBIsSuccess();

    }

    public void Initialize()
    {
    }
    void Update()
    {
        Score = PlayerPrefs.GetInt(string.Format("Score_{0}", StageNumber));
        ScoreTxt.text = Score.ToString();
    }

    public void SetBIsSuccess()//만약 성공했다면 이미지가 보이고, 점수 출력
    {
        int bIs = PlayerPrefs.GetInt(string.Format("Success_{0}", StageNumber));

        if (bIs == 1)
            bIsSuccess = true;
        else
            bIsSuccess = false;

        SuccessImg.SetActive(bIsSuccess);
    }

    public void onClick_StageItem()
    {
        GameMgr.Inst.m_gameInfo.StageLimitTime = KeepTime;
        GameMgr.Inst.m_gameInfo.StageMaxScore = MaxScore;
        GameMgr.Inst.m_gameInfo.StageNum = StageNumber;
        GameMgr.Inst.m_gameInfo.StageName = string.Format("{0}. " + Stage_GameName, StageID);
        GameMgr.Inst.m_MenuScene.FadeIn("GameScene");
    }
}
