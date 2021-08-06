using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/*
 * Confact : 010-2604-4271
 * Email : chlwlsdud3690@gmail.com
 * 
 * Create by Mr.Choi
 * 2021 - 06 - 17 / ~ / CC BY 2.0 / ♥≥∇≤♥
 * 
 * 
 */
public class EditDlg : MonoBehaviour
{
    [SerializeField] Button PlayBtn = null; // 시작 버튼
    [SerializeField] Button ClearBtn = null; // 오브젝트 클리어 버튼
    [SerializeField] Button DeleteBtn = null; // 클릭 오브젝트 삭제 버튼
    [SerializeField] Text CostTxt = null;

    //━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━//
    //          ▼ 아이템 버튼들 ▼
    [SerializeField] Button Item_Gr1 = null;
    [SerializeField] Button Item_Gr2 = null;
    [SerializeField] Button Item_Gr3 = null;
    [SerializeField] Button Item_Gr4 = null;
    //━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━//
    //          ▼ 아이템 복사 프리팹들 ▼
    [SerializeField] GameObject EditObj_Gr1 = null; 
    [SerializeField] GameObject EditObj_Gr2 = null;
    [SerializeField] GameObject EditObj_Gr3 = null;
    [SerializeField] GameObject EditObj_Gr4 = null;
    //━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━//
    [SerializeField] LayerMask Item_LayerMask;
    [SerializeField] LayerMask Bg_LayerMask;
    [SerializeField] LayerMask Btn_LayerMask;

    private Vector2 MousePos;// 마우스 포지션 받을 값
    public Transform EditObjContent = null; // 에딧창에서 클릭한 아이템 저장소
    public Transform ObjContent = null; // 인스펙터창 어디에 아이템을 넣을지
    public Transform TrashContent = null; // 우클릭하여 취소하고 싶은 오브젝트를 처리 하는 곳 (주기 마다 삭제 처리)

    private GameObject EditItem = null; // 에딧 아이템을 가질 임시 변수
    private GameObject ClickItem = null;
    [SerializeField] string ClickItem_name; // 배치된 아이템 클릭할때
    private int ItemIndex = 0; // 아이템의 인덱스 (지금 현재 에딧 하기 위해 아이템을 들고 있는가?  또는 아이템의 번호를 받기 위함)

    bool IsReplace = false;

    [SerializeField] List<GameObject> EditList = new List<GameObject>();// 생성한 오브젝트 리스트
    List<GameObject> ClickObj_list = new List<GameObject>(); // 클릭된놈 리스트로 가져오기
    List<GameObject> Gr4_List = new List<GameObject>(); // 아이템4번 리스트
    bool IsGr4_Visible = true;// 아이템 4번 보이게 할 변수;

    public int tempStageCost = 0;

    [SerializeField] Button InfoBtn = null;
    [SerializeField] Info infoDlg = null; 
    void Start()
    {
        PlayBtn.onClick.AddListener(OnClicked_Play);
        ClearBtn.onClick.AddListener(OnClicked_Clear);
        DeleteBtn.onClick.AddListener(OnClicked_Delete);
        Item_Gr1.onClick.AddListener(Gr1_Item);
        Item_Gr2.onClick.AddListener(Gr2_Item);
        Item_Gr3.onClick.AddListener(Gr3_Item);
        Item_Gr4.onClick.AddListener(Gr4_Item);

        InfoBtn.onClick.AddListener(Open_Info);
    }
    void Update()
    {
        if (GameMgr.Inst.m_GameScene.btlFSM.IsEditState() == true) // 에딧 스테이트 상태 때만 작용
        {
            MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // 아이템 생성 및 취소
            if (EditItem != null) // 마우스로 뭔가를 생성하려 한다!
            {
                EditItem.GetComponent<Item>().VisibleCollider();
                EditItem.transform.position = MousePos;
                //Edit();// 생성 이나 배치 같은거 할때의 함수
                //----------------------------------
                Scale_Edit(); // 스케일 키우거나 줄이는 함수
            }
            Ctrl_Z(); // 되돌리기 함수
            //if (ClickItem_name != "" && EditItem == null)
            //{
            //    ClickItem = GameObject.Find(ClickItem_name);
            //
            //    switch (ClickItem.tag)
            //    {
            //        case "Gravity_1":
            //            Gr1_Item();
            //            Destroy(ClickItem);
            //            EditList.Remove(ClickItem);
            //            break;
            //        case "Gravity_2":
            //            Gr2_Item();
            //            Destroy(ClickItem);
            //            EditList.Remove(ClickItem);
            //            break;
            //        case "Gravity_3":
            //            Gr3_Item();
            //            Destroy(ClickItem);
            //            EditList.Remove(ClickItem);
            //            break;
            //        case "Gravity_4":
            //            Gr4_Item();
            //            Destroy(ClickItem);
            //            EditList.Remove(ClickItem);
            //            break;
            //    }
            //
            //}
            Click_Interactable(); // 버튼 활성화 / 비활성화
            Saving_EditItem(); // 아이템 단축키 함수

            if (Input.GetMouseButtonDown(0))
                Click_EditedObj(); // 마우스 레이캐스트 오브젝트 클릭 함수

            if (ClickItem != null)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    ClickItem.GetComponent<Item>().VisibleCollider();
                    IsReplace = !IsReplace;
                }

                if (IsReplace == true)
                {
                    ClickItem.transform.position = MousePos;
                    if (Input.GetMouseButtonDown(0) && ClickItem.GetComponentInChildren<EditItemCollider>().IsEditTrue)
                    {
                        IsReplace = false;
                        ClickItem.GetComponent<Item>().InVisibleCollider();
                    }
                    else if (Input.GetMouseButtonDown(1))
                    {
                        GameObject item = Instantiate(ClickItem, TrashContent.position, new Quaternion(0, 0, 0, 0), TrashContent);
                        item.transform.position = new Vector2(TrashContent.position.x, TrashContent.position.y);
                        ClickItem.transform.position = new Vector2(TrashContent.position.x, TrashContent.position.y);
                        ClickItem = null;

                        switch (ClickItem.tag)
                        {
                            case "Gravity_1":
                                GameMgr.Inst.gameInfo.StageCost += 5;
                                break;
                            case "Gravity_2":
                                GameMgr.Inst.gameInfo.StageCost += 15;
                                break;
                            case "Gravity_3":
                                GameMgr.Inst.gameInfo.StageCost += 10;
                                break;
                            case "Gravity_4":
                                GameMgr.Inst.gameInfo.StageCost += 20;
                                break;
                        }

                        EditList.Remove(ClickItem);
                    }
                }
                Scale_Click();
            }
        }
        else if (GameMgr.Inst.m_GameScene.btlFSM.IsGameState() == true)
        {
            ClearBtn.interactable = false;
            DeleteBtn.interactable = false;
            Item_Gr1.interactable = false;
            Item_Gr2.interactable = false;
            Item_Gr3.interactable = false;
            Item_Gr4.interactable = false;

            if (Input.GetKeyDown(KeyCode.R))//다시 시작하기
            {
                GameMgr.Inst.m_GameScene.btlFSM.SetActState();
            }

            if(Input.GetKeyDown(KeyCode.E))
            {
                IsGr4_Visible = !IsGr4_Visible;
                visible_Gr4(IsGr4_Visible);
            }
        }

        //쓰레기통 위치를 카메라 위에 고정하여 "절대" 안보이게함
        TrashContent.transform.position = new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.y + 100);
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        Cheat();

        ///
    }
    void Scale_Edit()// _ 스케일 키우거나 줄이기 _
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (EditItem.transform.localScale.x > 1.0f)
                EditItem.transform.localScale -= new Vector3(0.25f, 0.25f, 0);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            if (EditItem.transform.localScale.x < 2.0f)
                EditItem.transform.localScale += new Vector3(0.25f, 0.25f, 0);
        }
    }
    void Scale_Click()// _ 스케일 키우거나 줄이기 _
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (ClickItem.transform.localScale.x > 0.5f)
                ClickItem.transform.localScale -= new Vector3(0.25f, 0.25f, 0);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            if (ClickItem.transform.localScale.x < 2.0f)
                ClickItem.transform.localScale += new Vector3(0.25f, 0.25f, 0);
        }
    }
    public void EditLeft()// 에딧
    {
        if (GameMgr.Inst.m_GameScene.btlFSM.IsEditState() != true) return; // 에딧 스테이트 상태 때만 작용
        if (EditItem == null) return;

        if (Input.GetMouseButton(0) && EditItem.GetComponentInChildren<EditItemCollider>().IsEditTrue == true)//생성
        {
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit2d = Physics2D.Raycast(touchPos, Camera.main.transform.forward, 1000, Bg_LayerMask);

            if (hit2d.collider.tag == "Bg")
            {
                GameObject item = Instantiate(EditItem, MousePos, new Quaternion(0, 0, 0, 0), ObjContent);
                item.GetComponent<Item>().InVisibleCollider();
                switch (ItemIndex) // 생성 오브젝트에 해당한 아이템 인덱스 카운트 증감
                {
                    case 1:
                        GameMgr.Inst.gameInfo.StageCost -= 5;
                        break;
                    case 2:
                        GameMgr.Inst.gameInfo.StageCost -= 15;
                        break;
                    case 3:
                        GameMgr.Inst.gameInfo.StageCost -= 10;
                        break;
                    case 4:
                        Gr4_List.Add(item);
                        GameMgr.Inst.gameInfo.StageCost -= 20;
                        break;
                }

                int rand = Random.Range(0, 1000);
                item.name = string.Format("{0}{1}_{2}", "Item", rand, ItemIndex); // 아이템 이름을 하나만 있게끔 이름 변환
                EditItem = null;
                ItemIndex = 0;
                EditList.Add(item);

                EditContent_Clear();
            }
            else
                Debug.Log(hit2d.collider.name);
        }
        // _ 취소 _
        if (Input.GetMouseButton(1)) // 우클릭 누르면 에딧 취소
        {
            GameObject item = Instantiate(EditItem, TrashContent.position, new Quaternion(0, 0, 0, 0), TrashContent);
            EditItem = null;
            ItemIndex = 0;

            EditContent_Clear();
            TrashContent_Clear();
        }
    }
    void Ctrl_Z() // 되돌리기
    {
        //컨트롤 + Z 스크립트
        if (EditList.Count > 0 && Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Z))
        {
            switch (EditList[EditList.Count - 1].tag)
            {
                case "Gravity_1":
                    GameMgr.Inst.gameInfo.StageCost += 5;
                    break;
                case "Gravity_2":
                    GameMgr.Inst.gameInfo.StageCost += 15;
                    break;
                case "Gravity_3":
                    GameMgr.Inst.gameInfo.StageCost += 10;
                    break;
                case "Gravity_4":
                    GameMgr.Inst.gameInfo.StageCost += 20;
                    break;
            }
            EditList[EditList.Count - 1].transform.position = new Vector2(TrashContent.position.x, TrashContent.position.y);
            Destroy(EditList[EditList.Count - 1],0.1f);
            EditList.Remove(EditList[EditList.Count - 1]);
        }
    }

    void Click_Interactable()// 버튼 활성화 / 비활성화
    {
        // 0 개 이거나 그 이하라면 못누르게, 혹은 0 보다 크다면 누를 수 있게(생성 가능하게)
        // 혹은 에딧 상태가 아니라면 누를수 없게 에딧 상태면 누를 수 있게
        if (GameMgr.Inst.gameInfo.StageCost >= 5)
            Item_Gr1.interactable = true;
        else
            Item_Gr1.interactable = false;
        if (GameMgr.Inst.gameInfo.StageCost >= 15)
            Item_Gr2.interactable = true;
        else
            Item_Gr2.interactable = false;
        if (GameMgr.Inst.gameInfo.StageCost >= 10)
            Item_Gr3.interactable = true;
        else
            Item_Gr3.interactable = false;
        if (GameMgr.Inst.gameInfo.StageCost >= 20)
            Item_Gr4.interactable = true;
        else
            Item_Gr4.interactable = false;
        ////////////////
        if (ClickItem != null && ClickItem_name != "") // 딜리트 버튼 On / Off
            DeleteBtn.interactable = true;
        else if (ClickItem_name == "" && ClickItem == null)
            DeleteBtn.interactable = false;
        /////////////////
        if (EditList.Count == 0 || EditList == null) // 클리어 버튼 On / Off
            ClearBtn.interactable = false;
        else
            ClearBtn.interactable = true;
    }
    void Saving_EditItem()//단축키
    {
        //에딧 단축키
        if (Input.GetKeyDown(KeyCode.Alpha1)) // 1번 아이템
        {
            EditContent_Clear();
            Gr1_Item();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) // 2번 아이템
        {
            EditContent_Clear();
            Gr2_Item();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) // 3번 아이템
        {
            EditContent_Clear();
            Gr3_Item();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4)) // 4번 아이템
        {
            EditContent_Clear();
            Gr4_Item();
        }
    }
    void Cheat() // 치트 관련 함수
    {
        //아이템 카운트 세는 텍스트
        if (GameMgr.Inst.m_GameScene.IsCheat())//치트가 켜져있다면
        {
            //------------------------------------------
            GameMgr.Inst.gameInfo.StageCost = 99999;
            // 쭉 생성할 수 있게끔
            CostTxt.text = "남은돈 : ∞"; // 그냥 무한정 생성이 가능하다는걸 보여주는 무한대 텍스트
        }
        else // 치트키 Off 이면 현재 아이템 카운트 보여주기
        {
            CostTxt.text = string.Format("남은돈 : {0}￦", GameMgr.Inst.gameInfo.StageCost);
        }
        ////////////////////////////////////////////////////////////////////////////

        //_____________________★__C H E A T __★____________________//

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)) // 치트 On
        {
            GameMgr.Inst.m_GameScene.CheatOn(true);
        }

        if (GameMgr.Inst.m_GameScene.IsCheat() == true && Input.GetKeyDown(KeyCode.L)) // 치트 Off
        {
            GameMgr.Inst.m_GameScene.CheatOn(false);
            Cheat_Off();
        }
        /////////////////////////////////////////////////////////////////
    }
    public void Initialize() // 게임 초기화 함수
    {
        OnClicked_Clear();

        tempStageCost = GameMgr.Inst.gameInfo.StageCost;
    }
    void OnClicked_Play() // 플레이 버튼 누를때
    {
        GameMgr.Inst.m_GameScene.btlFSM.SetWaveState();
        PlayBtn.interactable = false;
    }

    public void Restart() // 공이 죽어서 재시작 할때
    {
        ClickItem_name = "";
        PlayBtn.interactable = true;
        DeleteBtn.interactable = false;
        visible_Gr4(true);
    }

    void OnClicked_Clear() // 클리어 버튼 누를때 
    {
        ObjContent_Clear();
        EditContent_Clear();
        TrashContent_Clear();
        EditList.Clear();
        ClickObj_list.Clear();
        Gr4_List.Clear();
    }

    void Gr1_Item()
    {
        if (GameMgr.Inst.gameInfo.StageCost < 5) return;
        if (EditItem != null)
        {
            Destroy(EditItem);
        }
        GameObject item = Instantiate(EditObj_Gr1, EditObjContent);
        EditItem = item;
        ItemIndex = 1;
    }
    void Gr2_Item()
    {
        if (GameMgr.Inst.gameInfo.StageCost < 15) return;
        if (EditItem != null)
        {
            Destroy(EditItem);
        }
        GameObject item = Instantiate(EditObj_Gr2, EditObjContent);
        EditItem = item;
        ItemIndex = 2;
    }
    void Gr3_Item()
    {
        if (GameMgr.Inst.gameInfo.StageCost < 10) return;
        if (EditItem != null)
        {
            Destroy(EditItem);
        }
        GameObject item = Instantiate(EditObj_Gr3, EditObjContent);
        EditItem = item;
        ItemIndex = 3;
    }
    void Gr4_Item()
    {
        if (GameMgr.Inst.gameInfo.StageCost < 20) return;

        if (EditItem != null)
        {
            Destroy(EditItem);
        }
        GameObject item = Instantiate(EditObj_Gr4, EditObjContent);
        EditItem = item;
        ItemIndex = 4;
    }

    public int GetItemIndex() // 현재 가지고 있는 인덱스 아이템 / 0 = 없음
    {
        return ItemIndex;
    }

    void EditContent_Clear() // 지금 마우스에 있는 오브젝트 모두 삭제
    {
        Transform[] childList = EditObjContent.GetComponentsInChildren<Transform>(true);
        if (childList != null)
        {
            for (int i = 1; i < childList.Length; i++)
            {
                if (childList[i] != transform)
                    Destroy(childList[i].gameObject);
            }
        }

        ClickItem_name = "";
    }
    void TrashContent_Clear() // 설치 취소한 오브젝트 모두 삭제
    {
        Transform[] childList = TrashContent.GetComponentsInChildren<Transform>(true);
        if (childList != null)
        {
            for (int i = 1; i < childList.Length; i++)
            {
                if (childList[i] != transform)
                    Destroy(childList[i].gameObject);
            }
        }

        ClickItem_name = "";
    }

    void ObjContent_Clear() // 설치된 오브젝트 모두 삭제
    {
        Transform[] childList = ObjContent.GetComponentsInChildren<Transform>(true);
        if (childList != null)
        {
            for (int i = 1; i < childList.Length; i++)
            {
                if (childList[i] != transform)
                {
                    GameObject go = childList[i].gameObject;
                    switch (go.tag)
                    {
                        case "Gravity_1":
                            GameMgr.Inst.gameInfo.StageCost+=5;
                            break;
                        case "Gravity_2":
                            GameMgr.Inst.gameInfo.StageCost += 15;
                            break;
                        case "Gravity_3":
                            GameMgr.Inst.gameInfo.StageCost += 10;
                            break;
                        case "Gravity_4":
                            GameMgr.Inst.gameInfo.StageCost += 20;
                            Gr4_List.Remove(go);
                            break;
                    }
                    Destroy(go);
                }
            }
        }

        ClickItem_name = "";
    }

    void Click_EditedObj() // 설치 되어 있는 오브젝트
    {
        Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(touchPos, Camera.main.transform.forward, 1000, Item_LayerMask);

        if (hit.collider != null)
        {
            ClickItem = null;
            ClickItem_name = hit.collider.gameObject.name;
            ClickItem = hit.collider.gameObject;
        }
        else
            ClickItem_name = "";
    }

    void OnClicked_Delete() // 설치되어 있는 오브젝트중 클릭한놈 삭제 할때
    {
        if (ClickItem != null)
        {
            switch (ClickItem.tag)
            {
                case "Gravity_1":
                    GameMgr.Inst.gameInfo.StageCost += 5;
                    break;
                case "Gravity_2":
                    GameMgr.Inst.gameInfo.StageCost += 15;
                    break;
                case "Gravity_3":
                    GameMgr.Inst.gameInfo.StageCost += 10;
                    break;
                case "Gravity_4":
                    GameMgr.Inst.gameInfo.StageCost += 20;
                    Gr4_List.Remove(ClickItem);
                    break;
            }
            EditList.Remove(ClickItem);
            ClickItem.transform.position = new Vector2(TrashContent.position.x, TrashContent.position.y);
            Destroy(ClickItem, 0.1f);
            ClickItem = null;
            ClickItem_name = "";
        }
        else
            Debug.Log("클릭된놈 존재 X");
    }

    void Cheat_Off()
    {
        GameMgr.Inst.gameInfo.StageCost = tempStageCost;

        Transform[] childList = ObjContent.GetComponentsInChildren<Transform>(true);
        if (childList != null)
        {
            for (int i = 1; i < childList.Length; i++)
            {
                if (childList[i] != transform)
                {
                    switch (childList[i].tag)
                    {
                        case "Gravity_1":
                            GameMgr.Inst.gameInfo.StageCost -= 5;
                            break;
                        case "Gravity_2":
                            GameMgr.Inst.gameInfo.StageCost -= 15;
                            break; 
                        case "Gravity_3":
                            GameMgr.Inst.gameInfo.StageCost -= 10;
                            break;
                        case "Gravity_4":
                            GameMgr.Inst.gameInfo.StageCost -= 20;
                            break;
                    }
                }
            }
        }
    }

    void visible_Gr4(bool visi)
    {
        if (Gr4_List != null)
        {
            for (int i = 0; i < Gr4_List.Count; i++)
            {
                Gr4_List[i].SetActive(visi);
            }
        }
    }

    public void Open_Info()
    {
        infoDlg.Open();
    }
}
