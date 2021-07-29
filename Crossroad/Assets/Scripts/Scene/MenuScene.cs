using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScene : MonoBehaviour
{
    [SerializeField] GameUI gameUI;
    [SerializeField] HudUI hudUI;

    private void Awake()
    {
        GameMgr.Inst.SetMenuScene(this);
    }
    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
    }

    public void FadeIn(string path)
    {
        hudUI.fade.FadeIn(path);
    }
    public void FadeOut()
    {
        hudUI.fade.FadeOut();
    }

    void Update()
    {

    }
}
