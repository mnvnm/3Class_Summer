using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Esc_Dlg : MonoBehaviour
{
    [SerializeField] Button ResumeBtn = null;
    [SerializeField] Button OptionBtn = null;
    [SerializeField] Button ExitBtn = null;
    // Start is called before the first frame update
    void Start()
    {
        ResumeBtn.onClick.AddListener(Resume);
        OptionBtn.onClick.AddListener(Option);
        ExitBtn.onClick.AddListener(Exit);
        Close();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Open()
    {
        this.gameObject.SetActive(true);
    }
    public void Close()
    {
        this.gameObject.SetActive(false);
    }

    void Resume()
    {
        GameMgr.Inst.m_GameScene.Pause();
    }
    void Option()
    {

    }

    void Exit()
    {
        GameMgr.Inst.m_GameScene.OpenLoading();
        SceneManager.LoadScene("LobbyScene");
    }
}
