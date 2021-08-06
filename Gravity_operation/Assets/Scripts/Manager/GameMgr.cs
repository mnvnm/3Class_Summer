using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : Singleton2<GameMgr>
{
    private static GameMgr _Inst = new GameMgr();

    private GameScene m_gameScene;
    private LobbyScene m_lobbyScene;
    public GameScene m_GameScene { get { return m_gameScene; } }
    public LobbyScene m_LobbyScene { get { return m_lobbyScene; } }
    private CameraController m_CameraController;
    public CameraController camControll { get { return m_CameraController; } }
    public bool IsInstalled { get; set; }
    public int IsFirst = 0;

    public GameInfo gameInfo = new GameInfo();

    public void SetGameScene(GameScene scene)
    {
        m_gameScene = scene;
    }
    public void SetLobbyScene(LobbyScene scene)
    {
        m_lobbyScene = scene;
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
}
