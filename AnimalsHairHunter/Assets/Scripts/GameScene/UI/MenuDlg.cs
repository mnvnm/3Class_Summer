using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuDlg : MonoBehaviour
{
    public Button m_BtnShop;
    public Button m_BtnBoss;
    public Button m_BtnX;

    public CanvasGroup m_BossPanel;
    public CanvasGroup m_ShopPanel;

    public bool B_Check;

    public Animator m_ani;
    void Start()
    {
        B_Check = false;
        //m_ani.SetBool("bSelc", B_Check);

        m_BtnShop.onClick.AddListener(OnClickShop);
        m_BtnBoss.onClick.AddListener(OnClickBoss);
        m_BtnX.onClick.AddListener(OnClickX);
    }

    public void Initialize()
    {
        m_BtnX.gameObject.SetActive(false);
        m_BtnShop.interactable = true;
        m_BtnBoss.interactable = true;
        VisibleGameObj(true);
    }
    public void VisibleGameObj(bool Bis)
    {
        B_Check = false;
        m_ani.SetBool("bSelc", B_Check);
        m_BtnShop.gameObject.SetActive(Bis);
        m_BtnBoss.gameObject.SetActive(Bis);
        m_BossPanel.gameObject.SetActive(Bis);
        m_ShopPanel.gameObject.SetActive(Bis);
        m_BtnX.gameObject.SetActive(false);
    }

    public void OnClickShop()
    {
        m_BtnX.gameObject.SetActive(true);
        if (!B_Check)
        {
            B_Check = true;
            m_ani.SetBool("bSelc", B_Check);
        }
        StartCoroutine(ShopFadeIn());
    }
    public void OnClickBoss()
    {
        m_BtnX.gameObject.SetActive(true);
        if (!B_Check)
        {
            B_Check = true;
            m_ani.SetBool("bSelc", B_Check);
        }
        StartCoroutine(BossFadeIn());
    }
    public void OnClickX()
    {
        m_BtnX.gameObject.SetActive(false);
        if (B_Check)
        {
            B_Check = false;
            m_ani.SetBool("bSelc", B_Check);
        }
        m_BtnShop.interactable = true;
        m_BtnBoss.interactable = true;
    }

    IEnumerator BossFadeIn()
    {
        m_BossPanel.gameObject.SetActive(true);
        m_ShopPanel.gameObject.SetActive(false);
        m_BtnBoss.interactable = false;
        m_BtnShop.interactable = true;

        m_BossPanel.alpha = 0;

        while (m_BossPanel.alpha < 1)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            m_BossPanel.alpha += Time.deltaTime * 4;
        }
    }
    IEnumerator ShopFadeIn()
    {
        m_BossPanel.gameObject.SetActive(false);
        m_ShopPanel.gameObject.SetActive(true);
        m_BtnShop.interactable = false;
        m_BtnBoss.interactable = true;

        m_ShopPanel.alpha = 0;

        while (m_ShopPanel.alpha < 1)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            m_ShopPanel.alpha += Time.deltaTime * 4;
        }
    }
}
