using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudClickDlg : MonoBehaviour
{
    public Text m_txtHair;
    public Text m_txtGold;
    private Vector2 MousePos;
    [SerializeField] GameObject m_PrefClickParticle;
    [SerializeField] SpriteRenderer m_Scissors;

    public GameObject ImgHairBg;
    public GameObject ImgGoldBg;

    public GameObject CriticalObj;
    public GameObject AutoGoldObj;

    void Start()
    {
        Cursor.visible = false;
    }

    public void Initialize()
    {
        m_Scissors.sprite = Resources.Load("Image/UI/Scissors_Open", typeof(Sprite)) as Sprite;
    }

    public void Click()//클릭 할때 이벤트 함수(클릭마다 들어옴)
    {
        GameMgr.Inst.m_GameScene.m_GameUI.Click();
        var Obj = Instantiate(m_PrefClickParticle, MousePos, new Quaternion(0, 0, 0, 0), null);
        Sprite kSprite = Resources.Load("Image/UI/Scissors_Close", typeof(Sprite))as Sprite;
        m_Scissors.sprite = kSprite;

        Invoke("OpenScissors",0.05f);
    }
    void Update()
    {
        m_txtHair.text = string.Format( GameMgr.Inst.m_gameInfo.m_txtHair);
        m_txtGold.text = string.Format( GameMgr.Inst.m_gameInfo.m_txtGold);
        MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        m_Scissors.transform.position = new Vector3(MousePos.x,MousePos.y,-2);

        Cursor.visible = false;

        if (Input.GetMouseButtonDown(0))
        {
            RayClick();
        }
    }

    public void RayClick()
    {
            RaycastHit2D hit = Physics2D.Raycast(MousePos, Vector2.zero, 100);

        if (hit.collider == null) return;

        if (hit.collider.tag == "Click" && (GameMgr.Inst.m_GameScene.m_HudUI.m_MenuDlg.B_Check == false &&
            GameMgr.Inst.m_GameScene.m_HudUI.m_SettingDlg.B_Check == false) && !GameMgr.Inst.m_GameScene.BtlFSM.IsWaveState())
        {
            AudioManager.Inst.PlaySFX("가위");
            Click();
        }
    }

    void OpenScissors()
    {
        Sprite kSprite = Resources.Load("Image/UI/Scissors_Open", typeof(Sprite)) as Sprite;
        m_Scissors.sprite = kSprite;
    }

    public void SpawnCritical()
    {
        GameObject go = Instantiate(CriticalObj, m_Scissors.transform.position, Quaternion.identity, null);
        var Obj = Instantiate(m_PrefClickParticle, MousePos, new Quaternion(0, 0, 0, 0), null);
    }
    public void SpawnAutoGoldText()
    {
        GameObject go = Instantiate(AutoGoldObj, AutoGoldObj.transform.position, Quaternion.identity, null);
    }

    
    public void InvisibleAll()
    {
        ImgHairBg.SetActive(false);
        ImgGoldBg.SetActive(false);
        m_txtHair.gameObject.SetActive(false);
        m_txtGold.gameObject.SetActive(false);
    }
    public void VisibleAll()
    {
        ImgHairBg.SetActive(true);
        ImgGoldBg.SetActive(true);
        m_txtHair.gameObject.SetActive(true);
        m_txtGold.gameObject.SetActive(true);
    }
}
