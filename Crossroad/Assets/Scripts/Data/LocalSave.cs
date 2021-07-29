using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalSave 
{
    private static LocalSave _Instance = null;

    public static LocalSave Inst()
    {
        if (_Instance == null)
            _Instance = new LocalSave();

        return _Instance;
    }

    public bool IsUseBGM { get; set; }
    public bool IsUseSFX { get; set; }

    private LocalSave()
    {
        IsUseBGM = true;
        IsUseSFX = true;
    }

    public void Load()
    {
        int value = PlayerPrefs.GetInt("Bgm", 1);
        IsUseBGM = (value == 1);

        int value2 = PlayerPrefs.GetInt("SFX", 1);
        IsUseSFX = (value2 == 1);
    }

    public void Save()
    {
        int value = IsUseSFX ? 1 : 0;
        PlayerPrefs.SetInt("Bgm", value);

        int value2 = IsUseSFX ? 1 : 0;
        PlayerPrefs.SetInt("SFX", value2);
    }
}
