using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] List<Transform> MoveList = new List<Transform>();
    private bool IsMoved = true;
    private bool bAction = true;
    private int movingCount = 0;
    private Turret m_Turret;
    [SerializeField] float m_Speed = 5;
    [SerializeField] ResultUI resultUI;

    [SerializeField] Transform StartPos;
    void Start()
    {
        m_Turret = GameObject.FindWithTag("Turret").GetComponent<Turret>();
        movingCount = 0;
        Initialize();
    }

    void Initialize()
    {
        bAction = true;
        movingCount = 0;
        m_Speed = 5;
        this.gameObject.transform.position = StartPos.position;
        resultUI.Close();
        m_Turret.Initialize();

        this.transform.LookAt(MoveList[movingCount]);
    }                    

    void Update()
    {
        Move();
    }

    void Move()
    {
        if (bAction)
        {
            if (Input.GetMouseButton(0))
                IsMoved = false;
            else
                IsMoved = true;

            if (IsMoved)
            {
                transform.Translate(this.transform.forward * Time.deltaTime * m_Speed, Space.World);
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                m_Speed = m_Speed + 2f;
            }
            if(Input.GetKeyUp(KeyCode.LeftShift))
            {
                m_Speed = m_Speed - 2f;
            }
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            Initialize();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        //if(collision.gameObject.tag == "Bullet")
        //{
        //    IsMoved = false;
        //    bAction = false;
        //    m_Turret.m_bAction = false;
        //    movingCount = 0;
        //    resultUI.Open();
        //}
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Trans")
        {
            movingCount++;
            if (movingCount > 3)
            {
                m_Turret.MaxAttackTime -= 0.125f;
                m_Speed += 1.25f;
                movingCount = 0;
            }
            this.transform.LookAt(MoveList[movingCount]);
        }

        if(other.gameObject.tag == "Attack")
        {
            m_Turret.StartAttack();
            other.gameObject.GetComponent<AttackTrigger>().Init();
        }
    }

    public void Die()
    {
        IsMoved = false;
        bAction = false;
        movingCount = 0;
        resultUI.Open();
    }
}
