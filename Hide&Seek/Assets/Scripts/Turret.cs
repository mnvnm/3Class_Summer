using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public bool m_bAction = false;
    public bool m_bAttack = false;
    private Player m_Player;
    public float AttackTime;
    public float MaxAttackTime;
    [SerializeField] Light AttackLight;
    private MeshRenderer mesh;
    [SerializeField] GameObject Exclamation_mark;
    public void Initialize()
    {
        m_bAction = true;
        m_bAttack = false;
        AttackTime = 0;
        mesh.material = Resources.Load("Materials/TurretIdle") as Material;
        MaxAttackTime = 3.0f;
        m_Player = GameObject.FindWithTag("Player").GetComponent<Player>();
        AttackLight.enabled = false;
        Exclamation_mark.SetActive(false);
    }
    void Start()
    {
        mesh = this.GetComponent<MeshRenderer>();
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_bAction)
        {
            Raycas_For_Target(); // 레이 캐스트를 업데이트에서 불러 계속 호출시킴
            if (m_bAttack == true)
            {
                if (AttackTime >= MaxAttackTime)
                {
                    mesh.material = Resources.Load("Materials/TurretIdle") as Material;
                    AttackLight.enabled = false;
                    m_bAttack = false;
                    AttackTime = 0;
                    Exclamation_mark.SetActive(false);
                }

                if (AttackTime < MaxAttackTime)
                    AttackTime += Time.deltaTime;
            }
            if (m_Player != null)
                this.transform.LookAt(m_Player.transform);
        }
    }

    public void StartAttack() // 공격 시작 함수
    {
        Exclamation_mark.SetActive(true);
        AttackTime = 0;
        m_bAttack = true;
        mesh.material = Resources.Load("Materials/TurretAttackReady") as Material;
    }

    void Raycas_For_Target() // 플레이어를 공격하기 위한 함수
    {
        RaycastHit hit;
        if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, 20.0f))
        {
            if (AttackTime >= MaxAttackTime / 2)
            {
                AttackLight.enabled = true;
                mesh.material = Resources.Load("Materials/TurretAttack") as Material;
                if (hit.transform.gameObject.tag == "Player" && m_bAttack == true)
                {
                    m_bAction = false;
                    m_Player.Die();
                }
            }
        }

        else
        {
            return;
        }
    }
}
