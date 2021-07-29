using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rigd = null;
    float LifeTime = 0;

    void Start()
    {
        rigd = this.gameObject.GetComponent<Rigidbody2D>();
        Initialize();
    }
    public void Initialize()
    {
        rigd.velocity = new Vector2(-7, 0) * this.transform.right;
    }
    void Update()
    {
        LifeTime += Time.deltaTime;

        if(LifeTime >= 15.0f)
        {
            Die();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Ground":
                Die();
                break;

            case "Player":
                GameMgr.Inst.m_GameScene.btlFSM.SetActState();
                Die();
                break;
        }
    }

    void Die()
    {
        Destroy(this.gameObject);
    }
}
