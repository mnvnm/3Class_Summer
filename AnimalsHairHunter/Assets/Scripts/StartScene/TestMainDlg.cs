using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TestMainDlg : MonoBehaviour
{
    public Button m_BtnStart;
    public Button m_BtnOption;

    public OptionTestDlg m_OptionDlg = null;

    AudioSource m_AudioBgm = null;


    void Awake()
    {
        LocalSave.Inst().Load();
        m_AudioBgm = GetComponent<AudioSource>();
    }
    public void Start()
    {
        m_BtnOption.onClick.AddListener(OnClickedOption);
    }

    public void OnClickedOption()
    {
        m_OptionDlg.Show();
    }
}
