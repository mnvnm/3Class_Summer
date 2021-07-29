using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenuDlg : MonoBehaviour
{
    public Button m_BtnStart;
    public Button m_BtnOption;
    public Button m_BtnExit;

    [SerializeField] Image m_FadePannel;

    [SerializeField] Image m_MouseImg = null;
    [SerializeField] Canvas m_RootCanvas = null;

    void Start()
    {
        Cursor.visible = false;
        m_BtnStart.onClick.AddListener(OnClickBtnStart);
        m_BtnExit.onClick.AddListener(OnClickBtnExit);
    }

    private void Update()
    {
        Vector3 vPos = Input.mousePosition;
        Camera kCamera = m_RootCanvas.worldCamera;

        Vector3 vWorld;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(
                  m_RootCanvas.transform as RectTransform, vPos, kCamera, out vWorld);

        m_MouseImg.transform.position = vWorld;

        if(Input.GetMouseButtonDown(0))
        {
            Click();
        }

        //m_MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //m_Scissors.transform.position = new Vector3(m_MousePos.x, m_MousePos.y, 0);
    }
    public void Click()//클릭 할때 이벤트 함수(클릭마다 들어옴)
    {
        Sprite kSprite = Resources.Load("Image/UI/Scissors_Close", typeof(Sprite)) as Sprite;
        m_MouseImg.sprite = kSprite;

        Invoke("OpenScissors", 0.05f);
    }

    void OpenScissors()
    {
        Sprite kSprite = Resources.Load("Image/UI/Scissors_Open", typeof(Sprite)) as Sprite;
        m_MouseImg.sprite = kSprite;
    }

    public void OnClickBtnStart()
    {
        if (!m_FadePannel.gameObject.activeSelf)
            StartCoroutine(Enum_ChangeScene_Game());
    }

    public void OnClickBtnExit()
    {
        ExitGame();
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }
    IEnumerator Enum_ChangeScene_Game()
    {
        m_FadePannel.gameObject.SetActive(true);

        Color kColor = m_FadePannel.GetComponent<Image>().color;
        kColor.a = 0;

        while (kColor.a < 1)
        {
            m_FadePannel.GetComponent<Image>().color = kColor;
            yield return new WaitForSeconds(Time.deltaTime);
            kColor.a += Time.deltaTime * 4;
        }

        SceneManager.LoadScene("GameScene");
        yield return null;
    }
}
