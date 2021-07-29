using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fan : MonoBehaviour
{
    AreaEffector2D Effector_Area;// 바람을 불게하는 이펙터
    void Start()
    {
        Effector_Area = this.GetComponent<AreaEffector2D>();

        Effector_Area.forceAngle = 180; // 시작부터 왼쪽 방향임을 정해줌 
        Effector_Area.forceMagnitude = 60; // 힘의 크기
        Effector_Area.forceVariation = 90; // 힘의 크기 변화량
        
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Gravity_1": // 중력 2배 중력장
                break;
            case "Gravity_2": // 무중력 중력장
                Effector_Area.forceMagnitude = 0;
                break;
            case "Gravity_3": // 역방향 중력장
                Effector_Area.forceMagnitude = -Effector_Area.forceMagnitude;//바람의 방향을 반대로 함 
                                                                             // - <- 마이너스를 붙힘으로써 밀어내는게 아닌
                                                                             //     빨아들이는 흡입기로 바꿈
                break;
            case "Gravity_4": // 블랙홀 중력장
                break;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Ground")
        {
            switch (collision.gameObject.tag)
            {
                case "Gravity_1": // 중력 2배 중력장
                    Effector_Area.forceMagnitude = 60 * 2;//바람의 세기를 2배로 만듬
                    break;
                case "Gravity_2": // 무중력 중력장
                    Effector_Area.forceMagnitude = 0;
                    break;
                case "Gravity_3": // 역방향 중력장
                    break;
                default:
                    break;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Gravity_1": // 중력 2배 중력장
                Effector_Area.forceMagnitude = 60; // 힘의 크기
                Effector_Area.forceVariation = 90; // 힘의 크기 변화량
                break;
            case "Gravity_2": // 무중력 중력장
                Effector_Area.forceMagnitude = 60;
                break;
            case "Gravity_3": // 역방향 중력장
                Effector_Area.forceMagnitude *= -1;
                break;
            case "Gravity_4": // 블랙홀 중력장
                break;
        }
    }
}
