using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class ItemUI : MonoBehaviour
{
    [SerializeField] Text m_txtItemCount;
    // Start is called before the first frame update
    void Start()
    {
    //    if(!PhotonNetwork.IsMasterClient)
    //    {
    //        this.gameObject.SetActive(false);
    //    }
    }

    // Update is called once per frame
    void Update()
    {
        m_txtItemCount.text = string.Format("남은 재료 개수 : {0}",GameMgr.Inst.gameScene.m_ItemMng.GetItemMaxCount() - GameMgr.Inst.gameScene.m_ItemMng.GetItemCount());
    }
}
