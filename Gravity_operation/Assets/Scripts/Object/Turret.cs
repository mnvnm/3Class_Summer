using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] GameObject BulletPrefabs = null;
    [SerializeField] Transform BulletPosition = null;
    public bool IsShoot = true;

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
        IsShoot = true;
        StartCoroutine(ShootCoroutine());
    }
    public void DontShoot()
    {
        IsShoot = false;
    }

    IEnumerator ShootCoroutine()
    {
        while(GameMgr.Inst.m_GameScene.btlFSM.IsGameState() && IsShoot == true)
        {
            CreateBullet();
            yield return new WaitForSeconds(1.5f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Gravity_2")
        {
            IsShoot = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Gravity_2")
        {
            IsShoot = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Gravity_2")
        {
            IsShoot = true;
        }
    }
}
