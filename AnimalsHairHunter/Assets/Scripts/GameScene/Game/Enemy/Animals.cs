using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animals : MonoBehaviour//여기에선 동물들의 체력을 관리
{
    public double Hp = 0;
    public double MaxHp = 1000;
    public bool bIsAlive = true;
    public int Id = 0;
    public Animator m_DeadAnim;
    public Animation m_CreateAnim;

    SpriteRenderer m_SprRenderer;

    void Start()
    {
        m_SprRenderer = GetComponent<SpriteRenderer>();
    }

    public void Initialize()
    {
        MaxHp = GameMgr.Inst.m_gameInfo.m_GetHair * 10 * GameMgr.Inst.m_gameInfo.m_nStage;
        Hp = MaxHp;
        m_CreateAnim.Play();
    }

    public void Death()
    {
        bIsAlive = false;
        m_DeadAnim.SetBool("bIsDead", bIsAlive);
    }

    void Update()
    {
        if (this.gameObject.transform.position.x <= -12.0f)
            Destroy(this.gameObject);
    }
}
