using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    NavMeshAgent Agent;
    private GameObject m_Player;
    public Transform Target;
    public bool IsAction;
    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        IsAction = false;
    }

    void Sight()
    {
        //RaycastHit hit;
        //if (Physics.Raycast(transform.position,transform.forward,out hit, 10.0f))
        //{
        //    if(hit.transform.name =="Player")
        //    {
        //        transform.LookAt(hit.transform.position);
        //        Agent.SetDestination(hit.transform.position);
        //    }
        //}
        if (Target != null)
        {

            float Distance;
            Distance = Vector3.Distance(transform.position, Target.position);
            Vector3 dir;
            dir = Target.transform.position - this.transform.position;

            if (Distance < 7.0f)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 4);
                Agent.SetDestination(Target.position);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        m_Player = GameObject.FindWithTag("Player");
        if(m_Player != null)
            Target = m_Player.transform;

        if (Target != null)
        {

            if (Target.GetComponent<PlayerController>().isConnect)
            {
                IsAction = true;
            }
            if (IsAction)
            {
                Sight();
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Agent.SetDestination(Target.position);
                }
            }
        }
    }
}