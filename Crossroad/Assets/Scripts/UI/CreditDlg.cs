using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditDlg : MonoBehaviour
{
    [SerializeField] Button CloseBtn;
    // Start is called before the first frame update
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
    // Update is called once per frame
    void Update()
    {
        
    }
}
