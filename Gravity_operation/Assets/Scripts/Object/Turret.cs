using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] GameObject BulletPrefabs = null;
    [SerializeField] Transform BulletPosition = null;
    Coroutine shootCo = null;

    void Start()
    {
        
    }

    void Update()
    {
    }

    void CreateBullet()
    {
        var bull = Instantiate(BulletPrefabs, BulletPosition);
    }
    public void Shoot()
    {
        shootCo = StartCoroutine(ShootCoroutine());
    }
    public void DontShoot()
    {
        StopCoroutine(shootCo);
    }

    IEnumerator ShootCoroutine()
    {
        while(GameMgr.Inst.m_GameScene.btlFSM.IsGameState())
        {
            CreateBullet();
            yield return new WaitForSeconds(1.5f);
        }
    }
}
