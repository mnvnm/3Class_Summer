using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialMsgBox : MonoBehaviour
{
    [SerializeField] Button YesBtn = null;
    [SerializeField] Button NoBtn = null;

    [SerializeField] ExplainDlg Explain_Dlg = null;

    private void Start()
    {
        Close();
        YesBtn.onClick.AddListener(Yes_Im_First);
        NoBtn.onClick.AddListener(No_Im_Not_First);
    }
    public void Yes_Im_First() // 나 처음이야
    {
        AudioManager.Inst.PlaySFX("ClickSound");
        Look_Video();
        Close();
    }

    public void No_Im_Not_First() // 나 처음 아니야
    {
        AudioManager.Inst.PlaySFX("ClickSound");
        PlayerPrefs.SetInt("isFirst_", 1);
        SceneManager.LoadScene("StageScene");
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    void Look_Video()
    {
        Explain_Dlg.Open();
    }
}
