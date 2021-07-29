using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    [SerializeField] List<Transform> triggerTrans = new List<Transform>();
    public void Init()
    {
        int index = Random.Range(0,triggerTrans.Count);
        this.transform.position = triggerTrans[index].position;
    }
}
