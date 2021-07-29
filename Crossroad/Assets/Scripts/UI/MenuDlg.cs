using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuDlg : MonoBehaviour
{
    [SerializeField] Button OpenBtn;
    [SerializeField] Button CloseBtn;
    [SerializeField] Button OptionBtn;
    [SerializeField] Button ShareBtn;
    [SerializeField] Button HelperBtn;
    [SerializeField] Button CreditBtn;
    [SerializeField] Button ExitBtn;

    //[SerializeField] Text Rand_BibleTxt;
    //[SerializeField] List<string> BibleTextList = new List<string>();

    [SerializeField] Animator Anim;
    public OptionDlg optionDlg;
    public HelperDlg helperDlg;
    public CreditDlg creditDlg;
    [SerializeField] ExitDlg exitDlg;

    void Start()
    {
        CloseBtn.gameObject.SetActive(false);
        OpenBtn.onClick.AddListener(Open);
        CloseBtn.onClick.AddListener(Close);
        OptionBtn.onClick.AddListener(Option);
        ShareBtn.onClick.AddListener(Share);
        HelperBtn.onClick.AddListener(Helper);
        CreditBtn.onClick.AddListener(Credit);
        ExitBtn.onClick.AddListener(Game_Exit);

        optionDlg.Initialize();
    }
    public void Option()
    {
        optionDlg.Open();
    }
    public void Helper()
    {
        helperDlg.Open();
    }
    public void Credit()
    {
        creditDlg.Open();
    }
    public void Game_Exit()
    {
        exitDlg.Open();
    }

    public void Open()//Menu 버튼 누를때 들어옴 // 열기 
    {
        //int listIdx = Random.Range(0,BibleTextList.Count); //리스트에 있는것중 하나를 쓰기
        //Rand_BibleTxt.text = BibleTextList[listIdx];

        Anim.Play("Open");
        CloseBtn.gameObject.SetActive(true);
    }

    public void Close()//Menu 버튼 누를때 들어옴 // 닫기
    {
        CloseBtn.gameObject.SetActive(false);
        Anim.Play("Close");
    }

    void Update()
    {
        
    }

    public void OpenURL(string url)
    {
        Application.OpenURL(url);
    }

    public void Share()
    {
        StartCoroutine(TakeScreenshotAndShare());
    }

    private IEnumerator TakeScreenshotAndShare()
    {
        yield return new WaitForEndOfFrame();

        new NativeShare()
            .SetSubject("성경 십자말 게임 다운로드 공유").SetUrl("https://www.google.com/")
            .SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
            .Share();
    }
}
