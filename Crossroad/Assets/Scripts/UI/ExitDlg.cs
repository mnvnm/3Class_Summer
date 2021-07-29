using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitDlg : MonoBehaviour
{
    [SerializeField] Button YesBtn;
    [SerializeField] Button NoBtn;
    public void Start()
    {
        YesBtn.onClick.AddListener(Exit);
        NoBtn.onClick.AddListener(Close);
    }
    public void Close()
    {
        this.gameObject.SetActive(false);
    }
    public void Open()
    {
        this.gameObject.SetActive(true);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
