using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _Game_InputField : MonoBehaviour
{
    [SerializeField] _Game_TileMNG tileMNG;
    [SerializeField] InputField input_answer;
    [SerializeField] Animator QuizOK;
    void Start()
    {
        
    }

    public void Open() // ▣ 정답입력 버튼을 누를시
    {
        this.gameObject.SetActive(true);
        input_answer.Select();
    }

    public void EndInput() // ▣ 텍스트 입력이 끝난 후 엔터를 누르면 이 함수 들어옴
    {
        int id = tileMNG.curQuizIndex;
        bool b = false;

        for(int i = 0; i < AssetMgr.Inst.m_AssetQuiz.Count; i++)
        {
            AssetQuiz kAss = AssetMgr.Inst.m_AssetQuiz[i];
            if(kAss.m_id == id && kAss.answer.Equals(input_answer.text))
            {
                b = true;
                QuizOK.GetComponent<Image>().sprite = Resources.Load("Sprite/correct", typeof(Sprite)) as Sprite;
                tileMNG.dictAnswer[id] = true; // 해당 퀴즈 번호 정답처리
                tileMNG.PlusScore(id);
                tileMNG.OpenQuizAnswer(id);
                break;
            }
            else
            {
                QuizOK.GetComponent<Image>().sprite = Resources.Load("Sprite/wrong", typeof(Sprite)) as Sprite;
            }
        }
        AudioManager.Inst.PlaySFX(b ? "sfx_correct" : "sfx_wrong");
        QuizOK.Play("Quiz_OK");
        Close();
        // ▲

    }

    public void Close()
    {
        input_answer.text = "";
        this.gameObject.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            this.gameObject.SetActive(false);
        }
    }
}
