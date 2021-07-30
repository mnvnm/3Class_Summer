using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public int ID;

    private void Start()
    {
        GameMgr.Inst.gameScene.m_GameUI.m_ListKey.Add(this);
    }
}

