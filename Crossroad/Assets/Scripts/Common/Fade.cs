using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour
{
    public void FadeIn(string name)
    {
        StartCoroutine("ImgFadeIn", name);
    }

    public void FadeOut()
    {
        StartCoroutine(ImgFadeOut());
    }

    IEnumerator ImgFadeIn(string name)
    {
        this.gameObject.SetActive(true);
        Color kColor = this.gameObject.GetComponent<Image>().color;
        kColor = new Color(0, 0, 0, 0);
        this.gameObject.GetComponent<Image>().color = kColor;
        while (kColor.a < 1)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            kColor.a += Time.deltaTime * 4;
            this.gameObject.GetComponent<Image>().color = kColor;
        }

        SceneManager.LoadScene(name);
    }
    IEnumerator ImgFadeOut()
    {
        this.gameObject.GetComponent<Image>().gameObject.SetActive(true);
        Color kColor = this.gameObject.GetComponent<Image>().color;
        kColor = new Color(0, 0, 0, 1);
        this.gameObject.GetComponent<Image>().color = kColor;
        while (kColor.a > 0)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            kColor.a -= Time.deltaTime * 4;
            this.gameObject.GetComponent<Image>().color = kColor;
        }
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }
}
