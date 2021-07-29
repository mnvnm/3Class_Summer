using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditItemCollider : MonoBehaviour
{
    [HideInInspector] public bool IsEditTrue = true;

    private void Start()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Ground")
        {
            Debug.Log("에딧 아이템이 땅과 충돌했다구?");
            IsEditTrue = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground")
        {
            IsEditTrue = true;
        }
    }
}
