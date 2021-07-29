using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _STR
{
    public const string DTABLE_QUIZ = "TableData/Quiz";
    public const string DTABLE_QUIZDATA = "TableData/QuizData";
}

public class CAsset
{
    public int m_id = 0;                // id
}

public class AssetQuiz : CAsset
{
    public int m_StageId = 0;
    public int startpos = 0;
    public int dir = 0;
    public string answer = "";
    public string text = "";
}
public class AssetQuizData : CAsset
{
    public string title1 = "";
    public string title2 = "";
    public string title3 = "";
    public string hint1 = "";
    public string hint2 = "";
    public string hint3 = "";
    public string explain = "";
}

public class AssetMgr 
{
    static AssetMgr instance = null;
    public static AssetMgr Inst
    {
        get
        {
            if (instance == null)
                instance = new AssetMgr();
            return instance;
        }
    }

    private AssetMgr()
    {
        IsInstalled = false;
    }

    //----------------------------------------------------------
    public bool IsInstalled { get; set; }
    public List<AssetQuiz> m_AssetQuiz = new List<AssetQuiz>();
    public List<AssetQuizData> m_AssetQuizData = new List<AssetQuizData>();

    public void Initialize()
    {
        Initialzie_Quiz(_STR.DTABLE_QUIZ);
        Initialzie_QuizData(_STR.DTABLE_QUIZDATA);
        IsInstalled = true;
    }

    public void Initialzie_Quiz(string pathName)
    {
        List<string[]> kDatas = CSVParser.Load(pathName);
        if (kDatas == null)
            return;

        for (int i = 1; i < kDatas.Count; i++)
        {
            string[] aStr = kDatas[i];
            AssetQuiz kAss = new AssetQuiz();
            int index = 0;

            kAss.m_StageId = int.Parse(aStr[index++]);
            kAss.m_id = int.Parse(aStr[index++]);
            kAss.startpos = int.Parse(aStr[index++]);
            kAss.dir = int.Parse(aStr[index++]);
            kAss.answer = aStr[index++];
            kAss.text = aStr[index++];

            m_AssetQuiz.Add(kAss);
        }
        kDatas.Clear();
    }

    public void Initialzie_QuizData(string pathName)
    {
        List<string[]> kDatas = CSVParser.Load(pathName);
        if (kDatas == null)
            return;

        for (int i = 1; i < kDatas.Count; i++)
        {
            string[] aStr = kDatas[i];
            AssetQuizData kAss = new AssetQuizData();
            int index = 0;

            kAss.m_id = int.Parse(aStr[index++]);
            kAss.title1 = aStr[index++];
            kAss.title2 = aStr[index++];
            kAss.title3 = aStr[index++];
            kAss.hint1 = aStr[index++];
            kAss.hint2 = aStr[index++];
            kAss.hint3 = aStr[index++];
            kAss.explain = aStr[index++];

            m_AssetQuizData.Add(kAss);
        }
        kDatas.Clear();
    }
}
