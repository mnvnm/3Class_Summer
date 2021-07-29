using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveInfo
{
    //---------------------------------------------------
    public const string DSAVENAME = "SaveDataFile.data";
    //----------------------------------------------------    

    public void Initialize()
    {
        LoadFile();
    }

    public bool SaveFile()
    {
        FileStream fs = new FileStream(DSAVENAME, FileMode.Create, FileAccess.Write);
        if (fs == null) return false;

        BinaryWriter bw = new BinaryWriter(fs);

        SaveData(bw);

        bw.Close();
        fs.Close();

        return true;
    }

    public bool LoadFile()
    {
        try
        {
            FileStream fs = new FileStream(DSAVENAME, FileMode.Open, FileAccess.Read);
            if (fs == null) return false;

            BinaryReader br = new BinaryReader(fs);

            LoadData(br);

            br.Close();
            fs.Close();

            return true;
        }
        catch (Exception e)
        {
            Debug.Log("Error SaveInfo.LoadFile() - " + e.ToString());
        }
        return false;
    }

    public void SaveData(BinaryWriter bw)
    {
        //br.Write();
    }

    public void LoadData(BinaryReader br)
    {
        //br.Read
    }
}
