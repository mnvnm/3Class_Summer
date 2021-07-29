using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_S : MonoBehaviour
{
    [SerializeField] Turret m_Turret;
    [SerializeField] Player m_Player;

    public float amount;

    void Start()
    {
    }

    void Update()
    {
        transform.LookAt(m_Turret.transform); // 적 바라봄

        // 터렛 ↔ 플레이어 간의 단위벡터 거리를 구함
        Vector3 pos = (m_Player.transform.position - m_Turret.transform.position).normalized;
        transform.position = m_Player.transform.position + new Vector3(pos.x * amount, 2, pos.z * amount); 
        // 플레이어 + 더 떨어질 거리 : amount
    }
}
