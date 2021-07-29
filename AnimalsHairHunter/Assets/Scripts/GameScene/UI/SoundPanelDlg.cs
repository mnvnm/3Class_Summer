using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundPanelDlg : MonoBehaviour
{
    public Slider m_BGMSlider;
    public Slider m_SFXSlider;

    public Text m_BGMValue;
    public Text m_SFXValue;

    public AudioSource m_Audio;
    public float m_BGMSlide;
    public float m_SFXSlide;
    void Start()
    {
    }
    public void Initialize()
    {
        SoundSave();
        m_Audio.Play();
    }

    void Update()
    {
        SoundControl();
    }

    public void SoundControl()
    {
        AudioManager.Inst.MusicVolume = m_BGMSlider.value;
        m_BGMSlide = m_BGMSlider.value;
        AudioManager.Inst.SoundVolume = m_SFXSlider.value;
        m_SFXSlide = m_SFXSlider.value;

        PlayerPrefs.SetFloat("m_BGMSlide", m_BGMSlide);
        PlayerPrefs.SetFloat("m_SFXSlide", m_SFXSlide);

        m_BGMValue.text = string.Format("배경음 : {0}", (int)(m_BGMSlider.value * 100));
        m_SFXValue.text = string.Format("효과음 : {0}", (int)(m_SFXSlider.value * 100));
    }

    public void SoundSave()
    {
        m_BGMSlide = PlayerPrefs.GetFloat("m_BGMSlide", 1f);
        m_SFXSlide = PlayerPrefs.GetFloat("m_SFXSlide", 1f);

        m_BGMSlider.value = m_BGMSlide;
        m_SFXSlider.value = m_SFXSlide;

        AudioManager.Inst.MusicVolume = m_BGMSlider.value;
        AudioManager.Inst.SoundVolume = m_SFXSlider.value;
    }
}
