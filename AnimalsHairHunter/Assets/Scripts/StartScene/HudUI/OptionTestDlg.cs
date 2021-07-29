using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionTestDlg : MonoBehaviour
{
    [SerializeField] Toggle m_ToggleBgm;
    [SerializeField] Toggle m_ToggleSFX;

    [SerializeField] Button m_BtnOK;
    [SerializeField] Button m_BtnClose;

    [SerializeField] Text m_TxtBGM;
    [SerializeField] Text m_TxtSFX;

    public AudioSource m_BGM;


    private void Awake()
    {
        LocalSave.Inst().Load();
        PrintBGM(LocalSave.Inst().IsUseBGM);
        PrintSFX(LocalSave.Inst().IsUseSFX);
        m_ToggleBgm.isOn = LocalSave.Inst().IsUseBGM;
        m_ToggleSFX.isOn = LocalSave.Inst().IsUseSFX;
    }
    public void Start()
    {
        m_BtnOK.onClick.AddListener(OnClickOk);
        m_BtnClose.onClick.AddListener(OnClickClose);

        m_ToggleBgm.onValueChanged.AddListener((bool bOn) =>
        {
            OnValueChanged_BGM(bOn);
        });
        m_ToggleSFX.onValueChanged.AddListener((bool bOn) =>
        {
            OnValueChanged_SFX(bOn);
        });
    }

    public void OnClickOk()
    {
        LocalSave.Inst().IsUseBGM = m_ToggleBgm.isOn;
        LocalSave.Inst().IsUseSFX = m_ToggleSFX.isOn;
        LocalSave.Inst().Save();

        if (LocalSave.Inst().IsUseBGM)
            m_BGM.Play();
        else
            m_BGM.Stop();
    }

    public void OnClickClose()
    {
        LocalSave.Inst().Load();
        m_ToggleBgm.isOn = LocalSave.Inst().IsUseBGM;
        m_ToggleSFX.isOn = LocalSave.Inst().IsUseSFX;
        Close();
    }
    public void OnValueChanged_BGM(bool bOn)
    {
        PrintBGM(bOn);
    }
    public void OnValueChanged_SFX(bool bOn)
    {
        PrintSFX(bOn);
    }
    public void PrintBGM(bool bOn)
    {
        if (bOn) m_TxtBGM.text = "BGM ON";
        else m_TxtBGM.text = "BGM OFF";
    }
    public void PrintSFX(bool bOn)
    {
        if (bOn) m_TxtSFX.text = "SFX ON";
        else m_TxtSFX.text = "SFX OFF";
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
    }
    public void Close()
    {
        this.gameObject.SetActive(false);
    }
}
