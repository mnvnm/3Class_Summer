using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : Singleton2<GameMgr>
{
    private static GameMgr _Inst = new GameMgr();

    private GameScene m_gameScene;
    public GameScene m_GameScene { get { return m_gameScene; } }
    private CameraController m_CameraController;
    public CameraController camControll { get { return m_CameraController; } }
    public bool IsInstalled { get; set; }

    public GameInfo gameInfo = new GameInfo();

    public void SetGameScene(GameScene scene)
    {
        m_gameScene = scene;
    }

    public void SetCamControll(CameraController cam)
    {
        m_CameraController = cam;
    }
    public void Initialize()
    {
        IsInstalled = true;
        Application.runInBackground = true;
    }
    public void InitStart()
    {
    }
}
