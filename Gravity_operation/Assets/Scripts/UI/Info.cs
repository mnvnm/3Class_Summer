using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Info : MonoBehaviour
{
    [SerializeField] Button CloseBtn = null;
    public void Open()
    {
        this.gameObject.SetActive(true);
    }
    public void Close()
    {
        this.gameObject.SetActive(false);
    }
    void Start()
    {
        Close();
        CloseBtn.onClick.AddListener(Close);
    }
    void Update()
    {
        
    }
}
