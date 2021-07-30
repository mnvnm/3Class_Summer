using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using System.IO;

public class RoomManager : Singleton3<RoomManager>
{
    private static RoomManager Ins = new RoomManager();
    public List<Player> m_PlayerList = new List<Player>();
    public List<Player> m_MasterPlayerList = new List<Player>();
    public string MurderActorNumber;
    public int MurderIndex_;

    public Player Killer;
    private void Awake()
    {
        if (Ins)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Ins = this;
    }
    public override void OnEnable()
    {
        base.OnEnable();
            SceneManager.sceneLoaded += OnSceneLoaded;
    }
    public override void OnDisable()
    {
        base.OnEnable();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMod)
    {
        if (scene.buildIndex == 1)
        {
            Player[] player = PhotonNetwork.PlayerList;
            m_MasterPlayerList.Clear();
            m_PlayerList.Clear();
            for (int i = 0; i < player.Length; i++)
            {
                m_PlayerList.Add(player[i]);
            }

            if (PhotonNetwork.IsMasterClient)
            {
                for (int i = 0; i < player.Length; i++)
                {
                    m_MasterPlayerList.Add(player[i]);
                }
                MurderIndex_ = Random.Range(0, m_MasterPlayerList.Count);
                MurderActorNumber = m_MasterPlayerList[MurderIndex_].UserId;
                m_MasterPlayerList[MurderIndex_].SetWhosMurder(true);
            }
            //게임씬에 들어오면
            Debug.Log("들어옴 플레이어 생성됨");
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerManager"), Vector3.zero, Quaternion.identity);
        }
    }
}
