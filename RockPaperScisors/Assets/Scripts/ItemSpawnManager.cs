using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using System.IO;

public class ItemSpawnManager : MonoBehaviour
{
    [SerializeField] GameObject m_ItemPrefabs;
    [SerializeField] GameObject m_ItemContent;
    [SerializeField] List<GameObject> m_ItemSpawnList = new List<GameObject>();
    private List<int> _IndexList = new List<int>();
    private List<int> IDList = new List<int>();
    private int ItemMaxCount = 5;
    private int ItemCloneMaxCount = 0;
    public int GottedItemCount = 0;//지금까지 모은 아이템 갯수

    PhotonView PV;
    // Start is called before the first frame update
    void Start()
    {
        PV = gameObject.GetComponent<PhotonView>();
        _IndexList.Add(100);
        CreateItem();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void CreateItem()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            while (ItemCloneMaxCount < ItemMaxCount)
            {
                for (int i = 0; i < m_ItemSpawnList.Count; i++)
                {
                    int index = Random.Range(0, m_ItemSpawnList.Count);

                    if (_IndexList.Contains(index) == false)
                    {
                        _IndexList.Add(index);
                        Debug.Log("생성 아이템 " + ItemCloneMaxCount);
                        ItemCloneMaxCount++;
                        GameObject ItemObj = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "KeyPrefabs"), m_ItemSpawnList[index].transform.position, Quaternion.identity);
                        ItemObj.name += ItemCloneMaxCount;
                        ItemObj.GetComponent<Key>().ID = ItemCloneMaxCount;
                        IDList.Add(ItemCloneMaxCount);
                        break;
                    }
                }
            }
            Invoke("Callback_SetID", 1f);
        }
    }

    void Callback_SetID()
    {
        object[] param = new object[ItemMaxCount];
        for(int i = 0; i < param.Length;i++)
        {
            param[i] = IDList[i];
        }
        PV.RPC("RPC_SetKeyID",RpcTarget.Others, param);
    }
    [PunRPC]
    void RPC_SetKeyID(object[] param)
    {
        for (int i = 0; i < GameMgr.Inst.gameScene.m_GameUI.m_ListKey.Count; i++)
        {
            GameMgr.Inst.gameScene.m_GameUI.m_ListKey[i].ID = (int)param[i];
            Debug.Log(GameMgr.Inst.gameScene.m_GameUI.m_ListKey[i].ID);
        }
    }

    public int GetItemCount()
    {
        return GottedItemCount;
    }
    public int GetItemMaxCount()
    {
        return ItemMaxCount;
    }

}
