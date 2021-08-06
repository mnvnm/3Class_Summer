using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfoMsgDlg : MonoBehaviour
{
    int ItemIndex = 0;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public int GetItemIndex()
    {
        return ItemIndex;
    }
    public void SetItemIndex(int index)
    {
        ItemIndex = index;
    }
}
