using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudUI : MonoBehaviour
{
    public FeverGageUI m_FeverGageUI;
    public MenuDlg m_MenuDlg;
    public HudClickDlg m_HudClickDlg;
    public SettingDlg m_SettingDlg;
    public SoundPanelDlg m_SoundPanelDlg;
    public BossSuccessUI m_BossResultUI;
    public BossFailedUI m_BossFailedUI;
    public BossStateDlg m_BossStateDlg;
    public FadeInOut m_Fade;

    public void Initialize()
    {
        m_SoundPanelDlg.Initialize();
        m_SettingDlg.Initialize();
        m_MenuDlg.Initialize();
        m_FeverGageUI.Initialize();
        m_BossResultUI.Initialize();
        m_BossStateDlg.Initialize();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3))
        {
            m_BossStateDlg.m_BossKeepTime = 1;
        }
        m_FeverGageUI.ShowFeverGage();
    }
}
