using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 여러개의 파티클을 순차적으로 출력하는 클래스
 *  - min max를 두어 출력까지의 지연시간이 랜덤이다.
 *  - auto Hide 기능을 넣는다. ( 유지 시간이 넘으면 강제로 숨기기 )
 * 
 */
public class FXSerialize : MonoBehaviour
{
    public const int DMAX_COUNT = 1000;
    [SerializeField] ParticleSystem[] m_Particles = null;   // 파티클 리스트
    [SerializeField] float m_KeepTime = 3.0f;               // 강제 파괴를 위한 최소 유지시간
    [SerializeField] float m_NextPlayDelayMin = 0.1f;       // 파티클의 화면 출력까지 간격 시간 min
    [SerializeField] float m_NextPlayDelayMax = 0.3f;       // 파티클의 화면 출력까지 간격 시간 max
    [SerializeField] bool m_AutoHide = false;               // 강제로 유지시간 이후에는 파티클 플레이 정지후 모두 숨기기

    float m_DurationTime = 0.0f;
    int m_nCount = DMAX_COUNT;



    void Start()
    {
        StartPlay();
    }

    public void Show(bool bShow)
    {
        if( gameObject != null )
            gameObject.SetActive(bShow);
    }

    public void ShowParticles(bool bShow)
    {
        for (int i = 0; i < m_Particles.Length; i++)
        {
            m_Particles[i].gameObject.SetActive(bShow);
        }
    }

    public bool IsPlayFX()
    {
        if (Time.time - m_DurationTime > m_KeepTime)
            return false;

        for (int i = 0; i < m_Particles.Length; i++)
        {
            if (m_Particles[i].IsAlive())
                return true;
        }
        return false;
    }

    //외부에서 강제로 플레이를 호출할때 사용.
    public void Play()
    {
        Stop();
        Show(true);

        StartPlay();
    }

    private void StartPlay()
    {
        m_DurationTime = Time.time;
        StartCoroutine("EnumFunc_Play");
        
        if (m_AutoHide)
        {
            Invoke("CBI_AutoHide", m_KeepTime);
        }
    }

    IEnumerator EnumFunc_Play()
    {
        float fDelay = m_NextPlayDelayMin;
        m_nCount = 0;
        while (m_nCount < m_Particles.Length)
        {
            ParticleSystem kParticle = m_Particles[m_nCount];
            kParticle.gameObject.SetActive(true);               //  Play On Awake 가 체크 되어있다는 전제 하에서 사용
            
            if(!kParticle.main.playOnAwake )
                kParticle.Play();

            m_nCount++;

            int min = (int)(m_NextPlayDelayMin * 100);
            int max = (int)(m_NextPlayDelayMax * 100);
            int value = Random.Range(min, max);
            fDelay = value * 0.01f;

            yield return new WaitForSeconds(fDelay);
        }
        
        yield return null;
    }

    void CBI_AutoHide()
    {
        if (!IsPlayFX())
        {
            Stop();
            Debug.Log("Stop Play...");
        }
    }

    public void Stop()
    {
        for (int i = 0; i < m_Particles.Length; i++)
        {
            m_Particles[i].Stop();
            m_Particles[i].gameObject.SetActive(false);
        }
        m_nCount = DMAX_COUNT;
        Show(false);
    }



}
