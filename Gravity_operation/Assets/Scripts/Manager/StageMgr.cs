using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMgr : Singleton2<StageMgr>
{
    private StageScene stageScene;
    public StageScene m_StageScene { get { return stageScene; } }
    public int StageIndex = 1;
    public void SetStageScene(StageScene scene)
    {
        stageScene = scene;
    }
    public void Initailze()
    {
        Application.runInBackground = true;
    }
}
