using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyState : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Initialize()
    {
        Show();
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
    }
    public void Close()
    {
        this.gameObject.SetActive(false);
    }
}
