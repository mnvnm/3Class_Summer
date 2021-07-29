using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelperDlg : MonoBehaviour
{
    [SerializeField] Button CloseBtn;
    void Start()
    {
        CloseBtn.onClick.AddListener(Close);
    }

    public void Open()
    {
        this.gameObject.SetActive(true);
    }
    public void Close()
    {
        this.gameObject.SetActive(false);
    }

    void Update()
    {
        
    }
}
