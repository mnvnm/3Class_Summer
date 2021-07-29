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
    int IsFirst = 0;
    // Start is called before the first frame update
    void Start()
    {
        IsFirst = PlayerPrefs.GetInt("isFirst_");
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
        if(IsFirst == 0)
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
        SceneManager.LoadScene("EditorScene");
    }

    void Exit()
    {
        Application.Quit();
    }

    void OpenLoading()
    {
        Loading.SetActive(true);
    }
}
