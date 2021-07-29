using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopDlg : MonoBehaviour
{
    public Button m_BtnGetHairUpgrade;
    public Button m_BtnCriticalUpgrade;
    public Button m_BtnCriticalProbUpgrade;
    public Button m_BtnFeverGageUpgrade;
    public Button m_BtnAutoGoldUpgrade;
    public Button m_BtnAutoGoldTickUpgrade;
    public Button m_BtnSell;

    public Text txtLv_UpgGetHair;
    public Text txtLv_UpgCritical;
    public Text txtLv_UpgCriticalProb;
    public Text txtLv_UpgFeverGage;
    public Text txtLv_UpgAutoGold;
    public Text txtLv_UpgAutoGoldTime;

    public Text txtCost_UpgGetHair;
    public Text txtCost_UpgCritical;
    public Text txtCost_UpgCriticalProb;
    public Text txtCost_UpgFeverGage;
    public Text txtCost_UpgAutoGold;
    public Text txtCost_UpgAutoGoldTime;


    void Start()
    {
        m_BtnGetHairUpgrade.onClick.AddListener(Upgrade_GetHair);
        m_BtnCriticalUpgrade.onClick.AddListener(Upgrade_Critcal);
        m_BtnCriticalProbUpgrade.onClick.AddListener(Upgrade_CritcalProb);
        m_BtnFeverGageUpgrade.onClick.AddListener(Upgrade_FeverGage);
        m_BtnAutoGoldUpgrade.onClick.AddListener(Upgrade_AuotoGold);
        m_BtnAutoGoldTickUpgrade.onClick.AddListener(Upgrade_AutoGoldTick);
        m_BtnSell.onClick.AddListener(Sell_Hair);
    }

    public void initilaize()
    {

    }

    void Upgrade_GetHair()
    {
        GameMgr.Inst.m_gameInfo.Upgrade_GetHair();
    }
    void Upgrade_Critcal()
    {
        GameMgr.Inst.m_gameInfo.Upgrade_CriticalGetHair();
    }
    void Upgrade_CritcalProb()
    {
        GameMgr.Inst.m_gameInfo.Upgrade_CriticalProb();
    }
    void Upgrade_FeverGage()
    {
        GameMgr.Inst.m_gameInfo.Upgrade_FeverGage();
    }

    void Upgrade_AuotoGold()
    {
        GameMgr.Inst.m_gameInfo.Upgrade_AutoGold();
    }
    void Upgrade_AutoGoldTick()
    {
        GameMgr.Inst.m_gameInfo.Upgrade_AutoGoldTick();
    }
    void Sell_Hair()
    {
        GameMgr.Inst.m_gameInfo.Sell_Hair();
    }
    private void Update()
    {
        txtLv_UpgGetHair.text = string.Format("Lv {0:00}", GameMgr.Inst.m_gameInfo.m_UpgradeGetHair_LV);
        txtLv_UpgCritical.text = string.Format("Lv {0:00}", GameMgr.Inst.m_gameInfo.m_UpgradeCriticalGetHair_LV);
        txtLv_UpgCriticalProb.text = string.Format("Lv {0:00}", GameMgr.Inst.m_gameInfo.m_UpgradeCriticalProb_LV);
        txtLv_UpgFeverGage.text = string.Format("Lv {0:00}", GameMgr.Inst.m_gameInfo.m_UpgradeFeverGage_LV);
        txtLv_UpgAutoGold.text = string.Format("Lv {0:00}", GameMgr.Inst.m_gameInfo.m_UpgradeAutoGold_LV);
        txtLv_UpgAutoGoldTime.text = string.Format("Lv {0:00}", GameMgr.Inst.m_gameInfo.m_UpgradeAutoGoldTick_LV);

        txtCost_UpgGetHair.text = string.Format("Cost " + GameMgr.Inst.m_gameInfo.m_txtCost_GetHair);
        txtCost_UpgCritical.text = string.Format("Cost " + GameMgr.Inst.m_gameInfo.m_txtCost_Critical);
        txtCost_UpgCriticalProb.text = string.Format("Cost " + GameMgr.Inst.m_gameInfo.m_txtCost_Prob);
        txtCost_UpgFeverGage.text = string.Format("Cost " + GameMgr.Inst.m_gameInfo.m_txtCost_Fever);
        txtCost_UpgAutoGold.text = string.Format("Cost " + GameMgr.Inst.m_gameInfo.m_txtCost_AutoGold);
        txtCost_UpgAutoGoldTime.text = string.Format("Cost " + GameMgr.Inst.m_gameInfo.m_txtCost_AutoGoldTick);
    }
}
