using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    public Image m_imgFade;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeIn()
    {
        StartCoroutine(ImgFadeIn());
    }

    public void FadeOut()
    {
        StartCoroutine(ImgFadeOut());
    }

    IEnumerator ImgFadeIn()
    {
        m_imgFade.gameObject.SetActive(true);
           Color kColor = m_imgFade.color;
        kColor = new Color(0,0,0,0);
        m_imgFade.color = kColor;
        while (kColor.a < 1)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            kColor.a += Time.deltaTime * 4;
            m_imgFade.color = kColor;
        }
    }
    IEnumerator ImgFadeOut()
    {
        m_imgFade.gameObject.SetActive(true);
        Color kColor = m_imgFade.color;
        kColor = new Color(0, 0, 0, 1);
        m_imgFade.color = kColor;
        while (kColor.a > 0)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            kColor.a -= Time.deltaTime * 4;
            m_imgFade.color = kColor;
        }
    }
}
