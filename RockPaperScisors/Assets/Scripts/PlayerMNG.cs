using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using System.IO;

public class PlayerMNG : MonoBehaviour
{
    PhotonView PV;
    // Start is called before the first frame update
    GameObject playerController;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }
    void Start()
    {
        if(PV.IsMine)
        {
            CreateController();
        }
    }

    void CreateController()
    {
        playerController = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerPrefabs"), Vector3.zero, Quaternion.identity,0,new object[] { PV.ViewID });
        playerController.name = PhotonNetwork.NickName;
  
    }

    public void Die()
    {
        Debug.Log(playerController.name + " killed");
        PhotonNetwork.Destroy(playerController);
        CreateController();
    }
}
