using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class ExplainDlg : MonoBehaviour
{
    [SerializeField] Button NextBtn;
    [SerializeField] Button BackBtn;
    [SerializeField] Button PlayBtn;
    int PageNum = 0;
    public int MaxPage = 5;

    [SerializeField] List<VideoClip> VideoClip_List = new List<VideoClip>();
    [SerializeField] VideoPlayer VideoObj;
    // Start is called before the first frame update
    void Start()
    {
        NextBtn.onClick.AddListener(Next);
        BackBtn.onClick.AddListener(Back);
        PlayBtn.onClick.AddListener(Play);
        VideoObj.isLooping = true;
        PlayBtn.gameObject.SetActive(false);
        Close();
    }

    // Update is called once per frame
    void Update()
    {
        VideoObj.clip = VideoClip_List[PageNum];
        if (PageNum == 0)
            BackBtn.interactable = false;
        else if(PageNum > 0)
            BackBtn.interactable = true;

        if(PageNum > MaxPage - 1)
        {
            PlayerPrefs.SetInt("isFirst_", 1);
            GameMgr.Inst.IsFirst = PlayerPrefs.GetInt("isFirst_");
            PlayBtn.gameObject.SetActive(true);
            NextBtn.interactable = false;
        }
        if (PageNum < MaxPage)
            NextBtn.interactable = true;

    }

    public void Open()
    {
        this.gameObject.SetActive(true);
    }
    public void Close()
    {
        this.gameObject.SetActive(false);
    }

    void Next()
    {
        AudioManager.Inst.PlaySFX("ClickSound");
        if (PageNum < MaxPage)
            PageNum++;
    }
    void Back()
    {
        AudioManager.Inst.PlaySFX("ClickSound");
        if (PageNum > 0)
            PageNum--;
    }
    public void Play()
    {
        AudioManager.Inst.PlaySFX("ClickSound");
        GameMgr.Inst.m_LobbyScene.OpenLoading();
        SceneManager.LoadScene("StageScene");
    }
}
