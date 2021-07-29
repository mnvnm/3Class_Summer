using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageScene : MonoBehaviour
{
    [SerializeField] Button StartBtn = null;
    [SerializeField] Button BackBtn = null;
    [SerializeField] GameObject Loading = null;
    [SerializeField] Dictionary<int, bool> dictStage = new Dictionary<int,bool>();
    [SerializeField] List<GameObject> SuccessImg_List = new List<GameObject>();
    [SerializeField] List<Button> StageItem_List = new List<Button>();
    // Start is called before the first frame update
    private void Awake()
    {
        StageMgr.Inst.SetStageScene(this);
        StageMgr.Inst.Initailze();
    }
    void Start()
    {
        Loading.SetActive(false);
        StartBtn.onClick.AddListener(PlayOnGameScene);
        BackBtn.onClick.AddListener(Back);
        Initialize();
    }
    void Update()
    {
        for(int i = 0;i< StageItem_List.Count;i++)
        {
            StageItem_List[i].interactable = true;
        }
        StageItem_List[StageMgr.Inst.StageIndex - 1].interactable = false;

        if(Input.GetKey(KeyCode.A) && Input.GetKeyDown(KeyCode.D))
        {
            StageMgr.Inst.StageIndex = 0;
            PlayOnGameScene();
        }
    }

    public void Initialize()
    {
        for (int i = 0; i < 10; i++)
        {
            dictStage.Add(i, false);
        }

        for(int i = 0; i < dictStage.Count;i++)
        {
            int a = PlayerPrefs.GetInt(string.Format("Stage_{0}", i + 1));
            if (a == 1)
            {
                SuccessImg_List[i].SetActive(true);
                dictStage[i] = true;
            }

            else
            {
                SuccessImg_List[i].SetActive(false);
                dictStage[i] = false;
            }
        }
    }

    void PlayOnGameScene()
    {
        OpenLoading();
        SceneManager.LoadScene("GameScene");
    }

    void Back()
    {
        OpenLoading();
        SceneManager.LoadScene("LobbyScene");
    }

    void OpenLoading()
    {
        Loading.SetActive(true);
    }
}
