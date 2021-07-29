using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] EditItemCollider coll = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void VisibleCollider()
    {
        coll.gameObject.SetActive(true);
    }

    public void InVisibleCollider()
    {
        coll.gameObject.SetActive(false);
    }
}
