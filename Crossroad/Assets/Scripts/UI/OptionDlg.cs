using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionDlg : MonoBehaviour
{
    public bool bIsSoundOnOff = true; // 소리 끄기 켜기
    public bool bIsTimeLimitOnOff = true; // 시간 제한 끄기 켜기

    [SerializeField] Button IsSoundBtn;
    [SerializeField] Button IsTimeLimitBtn;

    [SerializeField] Image SoundOnOffImg; // 버튼 이미지
    [SerializeField] Image TimeLimitOnOffImg; // 버튼 이미지

    [SerializeField] Button CloseBtn;
    // Start is called before the first frame update
    void Start()
    {
        CloseBtn.onClick.AddListener(Close);
        IsSoundBtn.onClick.AddListener(Sound);
        IsTimeLimitBtn.onClick.AddListener(TimeLimit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Open()
    {
        this.gameObject.SetActive(true);
    }
    public void Close()
    {
        this.gameObject.SetActive(false);
    }
    public void Initialize()
    {
        bool sound = true;
        if (PlayerPrefs.HasKey("IsSound"))
        {
            sound = PlayerPrefs.GetInt("IsSound") == 1 ? true : false;
        }
        bIsSoundOnOff = sound == true ? true : false;
        bool timelimit = true;
        if (PlayerPrefs.HasKey("IsTimeLimt"))
        {
            timelimit = PlayerPrefs.GetInt("IsTimeLimt") == 1 ? true : false;
        }
        bIsTimeLimitOnOff = timelimit == true ? true : false;

        SetSprite(bIsSoundOnOff , bIsTimeLimitOnOff);

        AudioManager.Inst.IsMusicOn = bIsSoundOnOff;
        AudioManager.Inst.IsSoundOn = bIsSoundOnOff;


        if (bIsSoundOnOff == false)
            SoundOnOffImg.sprite = Resources.Load(("Sprite/Popup_Option_0"), typeof(Sprite)) as Sprite;
        if (bIsSoundOnOff == true)
            SoundOnOffImg.sprite = Resources.Load(("Sprite/Popup_Option_1"), typeof(Sprite)) as Sprite;
    }
    void Sound()
    {
        bIsSoundOnOff = !bIsSoundOnOff;
        if(bIsSoundOnOff == false)
            SoundOnOffImg.sprite = Resources.Load(("Sprite/Popup_Option_0"),typeof(Sprite)) as Sprite;
        if (bIsSoundOnOff == true)
            SoundOnOffImg.sprite = Resources.Load(("Sprite/Popup_Option_1"),typeof(Sprite)) as Sprite;

        PlayerPrefs.SetInt("IsSound", bIsSoundOnOff ? 1 : 0);

        AudioManager.Inst.IsMusicOn = bIsSoundOnOff;
        AudioManager.Inst.IsSoundOn = bIsSoundOnOff;
    }
    void TimeLimit()
    {
        bIsTimeLimitOnOff = !bIsTimeLimitOnOff;
        if (bIsTimeLimitOnOff == false)
            TimeLimitOnOffImg.sprite = Resources.Load(("Sprite/Popup_Option_0"), typeof(Sprite)) as Sprite;
        if (bIsTimeLimitOnOff == true)
            TimeLimitOnOffImg.sprite = Resources.Load(("Sprite/Popup_Option_1"), typeof(Sprite)) as Sprite;

        PlayerPrefs.SetInt("IsTimeLimt", bIsTimeLimitOnOff ? 1 : 0);
    }

    void SetSprite(bool IsSound, bool IsTime)
    {
        if (IsSound == false)
            SoundOnOffImg.sprite = Resources.Load(("Sprite/Popup_Option_0"), typeof(Sprite)) as Sprite;
        if (IsSound == true)
            SoundOnOffImg.sprite = Resources.Load(("Sprite/Popup_Option_1"), typeof(Sprite)) as Sprite;
        if (IsTime == false)
            TimeLimitOnOffImg.sprite = Resources.Load(("Sprite/Popup_Option_0"), typeof(Sprite)) as Sprite;
        if (IsTime == true)
            TimeLimitOnOffImg.sprite = Resources.Load(("Sprite/Popup_Option_1"), typeof(Sprite)) as Sprite;
    }
}
