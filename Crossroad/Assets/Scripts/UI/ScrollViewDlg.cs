using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewDlg : MonoBehaviour
{
    [SerializeField] Button EasyBtn;
    [SerializeField] Button NormalBtn;
    [SerializeField] Button HardBtn;
    [SerializeField] Button InfoBtn;

    [SerializeField] GameObject Easy_SeleBtn;
    [SerializeField] GameObject Normal_SeleBtn;
    [SerializeField] GameObject Hard_SeleBtn;
    [SerializeField] GameObject Info_SeleBtn;

    [SerializeField] ScrollRect Easy_Scroll;
    [SerializeField] ScrollRect Normal_Scroll;
    [SerializeField] ScrollRect Hard_Scroll;
    [SerializeField] ScrollRect Info_Scroll;
    public void Initialize()
    {
        if(PlayerPrefs.HasKey("StageLevel"))
        {
            int level = PlayerPrefs.GetInt("StageLevel");
            switch (level)
            {
                case 1:
                    SetEasy();
                    break;
                case 2:
                    SetNormal();
                    break;
                case 3:
                    SetHard();
                    break;
            }
        }
        else
        {
            SetEasy();
        }
    }
    void Start()
    {
        Initialize();
        EasyBtn.onClick.AddListener(SetEasy);
        NormalBtn.onClick.AddListener(SetNormal);
        HardBtn.onClick.AddListener(SetHard);
        InfoBtn.onClick.AddListener(Set_Info);


    }

    // Update is called once per frame
    void Update()
    {

    }

    void SetEasy()
    {
        Easy_SeleBtn.SetActive(true);
        Easy_Scroll.gameObject.SetActive(true);

        Normal_SeleBtn.SetActive(false);
        Hard_SeleBtn.SetActive(false);
        Info_SeleBtn.SetActive(false);

        Normal_Scroll.gameObject.SetActive(false);
        Hard_Scroll.gameObject.SetActive(false);
        Info_Scroll.gameObject.SetActive(false);
        PlayerPrefs.SetInt("StageLevel",1);
    }//초급 난이도 스크롤뷰 나타내기
    void SetNormal()
    {
        Normal_SeleBtn.SetActive(true);
        Normal_Scroll.gameObject.SetActive(true);

        Easy_SeleBtn.SetActive(false);
        Hard_SeleBtn.SetActive(false);
        Info_SeleBtn.SetActive(false);

        Easy_Scroll.gameObject.SetActive(false);
        Hard_Scroll.gameObject.SetActive(false);
        Info_Scroll.gameObject.SetActive(false);
        PlayerPrefs.SetInt("StageLevel", 2);
    }//중급 난이도 스크롤뷰 나타내기
    void SetHard()
    {
        Hard_Scroll.gameObject.SetActive(true);
        Hard_SeleBtn.gameObject.SetActive(true);

        Easy_SeleBtn.SetActive(false);
        Normal_SeleBtn.SetActive(false);
        Info_SeleBtn.SetActive(false);

        Easy_Scroll.gameObject.SetActive(false);
        Normal_Scroll.gameObject.SetActive(false);
        Info_Scroll.gameObject.SetActive(false);
        PlayerPrefs.SetInt("StageLevel", 3);
    }//고급 난이도 스크롤뷰 나타내기

    void Set_Info()
    {
        Info_SeleBtn.SetActive(true);
        Info_Scroll.gameObject.SetActive(true);

        Easy_SeleBtn.SetActive(false);
        Normal_SeleBtn.SetActive(false);
        Hard_SeleBtn.gameObject.SetActive(false);
        Easy_Scroll.gameObject.SetActive(false);
        Normal_Scroll.gameObject.SetActive(false);
        Hard_Scroll.gameObject.SetActive(false);
    }//일러두기 스크롤뷰 나타내기
}
