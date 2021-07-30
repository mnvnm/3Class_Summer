using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Flashlight : MonoBehaviour
{
    [SerializeField] Light m_Flashlight = null;
    public bool b_LightEnabled;
    PhotonView PV;
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponentInParent<PhotonView>();

        if(PV.IsMine)
        {
            b_LightEnabled = false;
            m_Flashlight.enabled = b_LightEnabled;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                b_LightEnabled = !b_LightEnabled;
                PV.GetComponentInParent<PlayerController>().OnAndOffLight();
            }
            m_Flashlight.enabled = b_LightEnabled;
            PV.GetComponentInParent<PlayerController>().IsLight = b_LightEnabled;
        }
        else
        {
            m_Flashlight.enabled = PV.GetComponentInParent<PlayerController>().IsLight;
        }
    }
}
