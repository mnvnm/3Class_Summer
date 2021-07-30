using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class MurderUI : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject m_txtMurderCoolDownTime;

    public void Initialize()
    {
    }
    void Start()
    {
    }
    void Update()
    {

    }
    public void KillCoolDownTimetxt(string cool)
    {
        m_txtMurderCoolDownTime.GetComponent<Text>().text = string.Format("Cool : {0}", (int)float.Parse(cool));
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
