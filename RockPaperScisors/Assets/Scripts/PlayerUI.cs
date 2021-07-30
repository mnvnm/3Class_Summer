using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerUI : MonoBehaviourPunCallbacks
{
    [SerializeField] Transform PlayerListContent;
    [SerializeField] GameObject m_PlayerListItemPrefab;

    public void Initialize()
    {
        for (int i = 0; i < RoomManager.Inst.m_PlayerList.Count; i++)
        {
            Instantiate(m_PlayerListItemPrefab, PlayerListContent).GetComponent<PlayerProfileItem>().SetUp(RoomManager.Inst.m_PlayerList[i]);
        }
    }
    void Start()
    {

    }
    void Update()
    {

    }
    public void CloseUI()
    {
        this.gameObject.SetActive(false);
    }
    public void OpenUI()
    {
        this.gameObject.SetActive(true);
    }
}
