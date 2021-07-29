using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMgr : MonoBehaviour
{
    public bool m_isBGM = false;
    [HideInInspector] public AudioSource m_AudioSource = null;

    [SerializeField] AudioClip Eff_sScissors;
    [SerializeField] AudioClip Eff_sClickBtn;
    [SerializeField] AudioClip Eff_sClickHairToGold;

    private void Start()
    {
        if (m_AudioSource == null)
            m_AudioSource = GetComponent<AudioSource>();

        Initialize_Bgm();
        Initialize_SFX();
    }

    private void Initialize_Bgm()
    {
        if (m_isBGM)
        {
            if (LocalSave.Inst().IsUseBGM && (m_AudioSource.loop || m_AudioSource.playOnAwake))
                m_AudioSource.Play();
            else
                m_AudioSource.Stop();
        }
    }
    private void Initialize_SFX()
    {
        if (!m_isBGM)
        {
            if (LocalSave.Inst().IsUseSFX && (m_AudioSource.loop || m_AudioSource.playOnAwake))
                m_AudioSource.PlayOneShot(m_AudioSource.clip);
            else
                m_AudioSource.Stop();
        }
    }

    public void Play()
    {
        if (m_isBGM && LocalSave.Inst().IsUseBGM)
            m_AudioSource.Play();

        if (!m_isBGM && LocalSave.Inst().IsUseSFX)
            m_AudioSource.PlayOneShot(m_AudioSource.clip);
    }

    public void Stop()
    {
        m_AudioSource.Stop();
    }
}
