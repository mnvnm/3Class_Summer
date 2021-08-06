using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageItem : MonoBehaviour
{
    public void OnClick_This(int index)
    {
        StageMgr.Inst.StageIndex = index;
        AudioManager.Inst.PlaySFX("ClickSound");
    }
}
