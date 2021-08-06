using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyScene : MonoBehaviour
{
    [SerializeField] Button StartBtn = null;
    [SerializeField] Button ExitBtn = null;
    [SerializeField] Button EditorBtn = null;
    [SerializeField] GameObject Loading = null;
    [SerializeField] TutorialMsgBox MsgBox = null;
    // Start is called before the first frame update
    void Start()
    {
        GameMgr.Inst.SetLobbyScene(this);
        GameMgr.Inst.IsFirst = PlayerPrefs.GetInt("isFirst_");
        StartBtn.onClick.AddListener(PlayOnGameScene);
        EditorBtn.onClick.AddListener(PlayOnEditorScene);
        ExitBtn.onClick.AddListener(Exit);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void PlayOnGameScene()
    {
        AudioManager.Inst.PlaySFX("ClickSound");
        if (GameMgr.Inst.IsFirst == 0)
            MsgBox.Open();
        else
        {
            OpenLoading();
            SceneManager.LoadScene("StageScene");
        }

    }

    void PlayOnEditorScene()
    {
        OpenLoading();
        AudioManager.Inst.PlaySFX("ClickSound");
        SceneManager.LoadScene("EditorScene");
    }

    void Exit()
    {
        AudioManager.Inst.PlaySFX("ClickSound");
        Application.Quit();
    }

    public void OpenLoading()
    {
        Loading.SetActive(true);
    }
}
