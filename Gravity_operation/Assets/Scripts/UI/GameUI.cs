using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [HideInInspector] public Ball ball = null;
    public List<Turret> Turret_List = new List<Turret>();
    void Start()
    {
    }
    void Update()
    {
        
    }

    public void Initialize()// 초기화 함수
    {
        FindBall();
        ball.Initialize();// 공 초기화
    }

    void FindBall()
    {
        ball = GameObject.Find("Ball").GetComponent<Ball>();
        if(ball == null) // 재귀 함수로 공을 못찾으면 계속 찾음
        {
            FindBall();
        }
    }

    public void Add_Turret_List(Turret turret)
    {
        Turret_List.Add(turret);
    }

    public void Shoot()
    {
        for(int i = 0; i < Turret_List.Count;i++)
        {
            Turret_List[i].Shoot();
        }
    }

    public void DontShoot()
    {
        for (int i = 0; i < Turret_List.Count; i++)
        {
            Turret_List[i].DontShoot();
        }
    }
}
