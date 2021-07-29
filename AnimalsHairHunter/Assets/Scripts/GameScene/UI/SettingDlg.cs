using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingDlg : MonoBehaviour
{
    public bool B_Check;
    public Button m_BtnSet;
    public Button m_BtnBack;
    public Button m_BtnSound;

    public CanvasGroup m_SetWindowFade;

    [SerializeField] GameObject m_SettingPanel;
    [SerializeField] GameObject m_SoundPanel;
    [SerializeField] Image m_FadePanel;

    void Start()
    {
        m_BtnSet.onClick.AddListener(OnClickedSet);
        m_BtnBack.onClick.AddListener(OnClickedBack);
        m_BtnSound.onClick.AddListener(OnClickedSound);
        Close();
    }

    public void Initialize()
    {
    }

    void Update()
    {
        
    }

    public void OnClickedSet()
    {
        Show();
        m_BtnSet.gameObject.SetActive(false);
        m_SettingPanel.SetActive(true);

        if(m_SetWindowFade.alpha <= 0)
            StartCoroutine(FadeINSetPanel());
    }
    public void OnClickedBack()
    {
        m_BtnSet.gameObject.SetActive(true);

        if (m_SetWindowFade.alpha >= 1)
            StartCoroutine(FadeOutSetPanel());
    }

    public void OnClickedSound()
    {
        m_SoundPanel.SetActive(true);
    }

    IEnumerator FadeINSetPanel()
    {
        while(m_SetWindowFade.alpha < 1)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            m_SetWindowFade.alpha += Time.deltaTime * 8;
        }
        m_BtnSound.interactable = true;
    }
    IEnumerator FadeOutSetPanel()
    {
        while(m_SetWindowFade.alpha > 0)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            m_SetWindowFade.alpha -= Time.deltaTime * 8;
        }
        Close();
        m_SettingPanel.SetActive(false);
        m_BtnSound.interactable = false;
    }

    IEnumerator FadeInPanel()
    {
        Image[] allChildren = GetComponentsInChildren<Image>();
        Color kColor = new Color(1, 1, 1, 0);

        while(kColor.a < 1)
        {
            foreach (Image child in allChildren)
                 child.GetComponent<Image>().color = kColor;

            yield return new WaitForSeconds(Time.deltaTime);

            for (int i = 0; i < m_SettingPanel.transform.childCount; i++)
                kColor.a += Time.deltaTime * 1;
        }
    }

    // 아 ㅋㅋ setActive 활성화 안된얘들은 코루틴 작동 안한다니깐
    IEnumerator FadeInPlay()
    {
        Color FadeColor = m_FadePanel.color;
        FadeColor.a = 0;
    
        while (FadeColor.a < 1)
        {
            m_FadePanel.material.SetFloat("_Blur", FadeColor.a * 10);
            yield return new WaitForSeconds(Time.deltaTime);
            FadeColor.a += Time.deltaTime * 4;
        }
    }

    IEnumerator FadeOutPlay()
    {
        Color FadeColor = m_FadePanel.color;
        FadeColor.a = 1;

        while (FadeColor.a > 0)
        {
            m_FadePanel.material.SetFloat("_Blur", FadeColor.a * 10);
            yield return new WaitForSeconds(Time.deltaTime);
            FadeColor.a -= Time.deltaTime * 4;
        }

        m_SettingPanel.SetActive(false);
    }

    public void Show()
    {
        B_Check = true;
        this.gameObject.SetActive(true);
    }
    public void Close()
    {
        this.gameObject.SetActive(false);
        B_Check = false;
    }
}
