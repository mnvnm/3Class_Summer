using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : Singleton<GameMgr>
{
    public GameInfo m_gameInfo = new GameInfo();
    public SaveInfo m_saveInfo = new SaveInfo();

    private GameScene m_gameScene;
    public GameScene m_GameScene { get { return m_gameScene; } }
    public void SetGameScene(GameScene Scene)    {
        m_gameScene = Scene;
    }

    public bool IsInstalled { get; set; }

    public void Initialize()
    {
        IsInstalled = true;
    }

    public void Game_Initialize()
    {
        m_gameInfo.Init();
    }
}
