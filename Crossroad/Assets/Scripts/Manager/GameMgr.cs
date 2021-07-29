using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : Singleton<GameMgr>
{
    public GameInfo m_gameInfo = new GameInfo();
    public SaveInfo m_saveInfo = new SaveInfo();
    public Fade FadeInOut = new Fade();

    private _Game_Root m_gameScene;
    private MenuScene m_menuScene;
    public _Game_Root m_GameScene { get { return m_gameScene; } }
    public MenuScene m_MenuScene { get { return m_menuScene; } }
    public void SetGameScene(_Game_Root Scene)    {
        if (m_gameScene == null)
            m_gameScene = Scene;
    }
    public void SetMenuScene(MenuScene Scene) {
        if (m_menuScene == null)
            m_menuScene = Scene;
    }
    public bool IsInstalled { get; set; }

    public void Initialize()
    {
        IsInstalled = true;
        m_GameScene.Initialize();
        //m_MenuScene.Initialize();
    }

    public void Game_Initialize()
    {
        m_saveInfo.Initialize();
        m_gameInfo.Init();
    }
}
