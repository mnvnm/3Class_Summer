using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuManager : MonoBehaviour
{
    [SerializeField] Menu[] m_Menus;
    public static MenuManager Inst;
    private void Awake()
    {
        Inst = this;
    }
    public void Initialize()
    {
    }
    void Update()
    {
        OnUpdate();
    }

    void OnUpdate()
    {
    }
    public void OpenMenu(string menuName)
    {
        for (int i = 0; i < m_Menus.Length; i++)
        {
            if (m_Menus[i].m_Menuname == menuName)
            {
                m_Menus[i].Open();
            }
            else if (m_Menus[i].IsOpen)
            {
                CloseMenu(m_Menus[i]);
            }
        }
    }
    public void OpenMenu(Menu menu)
    {
        for (int i = 0; i < m_Menus.Length; i++)
        {
            if (m_Menus[i].IsOpen)
            {
                CloseMenu(m_Menus[i]);
            }
        }
        menu.Open();
    }
    public void CloseMenu(Menu menu)
    {
        menu.Close();
    }
}
