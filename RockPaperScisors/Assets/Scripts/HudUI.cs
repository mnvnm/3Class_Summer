using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class HudUI : MonoBehaviour
{
    public PlayerUI m_PlayerUI;
    public ItemUI m_ItemUI;
    public void Initialize()
    {
        m_PlayerUI.Initialize();
    }
    void Update()
    {
        OnUpdate();
    }

    void OnUpdate()
    {
    }
}
