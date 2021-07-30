using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : Singleton<GameMgr>
{
    private static GameMgr _Inst = new GameMgr();

    private GameScene m_gameScene;
    public GameInfo m_gameInfo = new GameInfo();
    public SaveInfo m_saveInfo = new SaveInfo();
    public Launcher m_Net = new Launcher();
    string IsMyUserID = "";

    //public RoomManager m_RoomMNG = new RoomManager();

    public GameScene gameScene { get { return m_gameScene; } }
    public bool IsInstalled { get; set; }
    public GameMgr()
    {

    }

    public void SetGameScene(GameScene scene)
    {
        m_gameScene = scene;
    }
    public void Initialize()
    {
        Time.timeScale = 1;
        IsInstalled = true;
        Application.runInBackground = true;
    }
    public void InitStart()
    {
    }

    public void OnUpdate(float dTime)
    {
    }

    public void LoadFile()
    {
        //m_saveInfo.LoadFile();
    }
    public void SaveFile()
    {
        //m_saveInfo.SaveFile();
    }

    public string GetID()
    {
        return IsMyUserID;
    }

    public void SetID(string ID)
    {
        IsMyUserID = ID;
    }
}
