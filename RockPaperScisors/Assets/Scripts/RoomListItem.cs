using Photon.Realtime;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoomListItem : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    public RoomInfo Info;
    public void SetUp(RoomInfo info)
    {
        Info = info;
        text.text = info.Name;
    }

    public void OnClick()
    {
        GameMgr.Inst.m_Net.JoinRoom(Info);
    }
}
