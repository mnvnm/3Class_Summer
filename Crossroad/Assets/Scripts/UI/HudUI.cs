using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudUI : MonoBehaviour
{
    [SerializeField] ScrollViewDlg ScrollViewUI;
    public Fade fade;
    public void Initialize()
    {
        ScrollViewUI.Initialize();
    }
    void Start()
    {
        fade.FadeOut();
    }

    void Update()
    {
        
    }
}
