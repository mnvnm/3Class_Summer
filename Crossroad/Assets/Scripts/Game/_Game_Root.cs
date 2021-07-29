using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _Game_Root : MonoBehaviour
{
    [SerializeField] _Game_TileMNG tileMgr;
    public Fade fade;
    public ResultUI resultUI;
    public GameStartDlg gameStartDlg;

    private void Awake()
    {
        GameMgr.Inst.SetGameScene(this);
        GameMgr.Inst.Initialize();
    }
    void Start()
    {
    }

    public void Initialize()
    {
        AssetMgr.Inst.Initialize();
        tileMgr.Initialize();
        gameStartDlg.Initialize();
        resultUI.Initialize();
        gameStartDlg.menuDlg.optionDlg.Initialize();
    }
    // Update is called once per frame
    void Update()
    {
        
    }


    //public void Reset_Game()
    //{
    //    tileMgr.Initialize();
    //    gameStartDlg.Initialize();
    //    resultUI.Initialize();
    //}
}
