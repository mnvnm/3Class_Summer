using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameUI : MonoBehaviourPunCallbacks
{
    public List<PlayerController> m_listPC = new List<PlayerController>();
    public List<Key> m_ListKey = new List<Key>();

    public void Initialize()
    {
        for(int i = 0; i < m_listPC.Count;i++)
        {
            m_listPC[i].Initialize();
        }
    }
    void Update()
    {
    }
}
