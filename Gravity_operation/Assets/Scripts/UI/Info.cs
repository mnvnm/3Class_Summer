using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Info : MonoBehaviour
{
    [SerializeField] Button CloseBtn = null;
    [SerializeField] Button NextBtn = null;
    [SerializeField] Button BackBtn = null;

    [SerializeField] List<GameObject> PageList = new List<GameObject>();
    int PageNum = 0;
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
        NextBtn.onClick.AddListener(Next);
        BackBtn.onClick.AddListener(Back);
        CloseBtn.onClick.AddListener(Close);

        for (int i = 0; i < PageList.Count; i++)
        {
            if (i == PageNum)
                PageList[i].SetActive(true);
            else
                PageList[i].SetActive(false);
        }
    }

    void Next()
    {
        if (PageNum < 1)
            PageNum++;

        for(int i = 0; i < PageList.Count;i++)
        {
            if(i == PageNum)
                PageList[i].SetActive(true);
            else
                PageList[i].SetActive(false);
        }
    }
    void Back()
    {
        if (PageNum > 0)
            PageNum--;

        for (int i = 0; i < PageList.Count; i++)
        {
            if (i == PageNum)
                PageList[i].SetActive(true);
            else
                PageList[i].SetActive(false);
        }
    }
    void Update()
    {
        if (PageNum > 0)
        {
            BackBtn.gameObject.SetActive(true);
        }
        else
        {
            BackBtn.gameObject.SetActive(false);
        }
        if (PageNum < 1)
        {
            NextBtn.gameObject.SetActive(true);
        }
        else
        {
            NextBtn.gameObject.SetActive(false);
        }

    }
}
