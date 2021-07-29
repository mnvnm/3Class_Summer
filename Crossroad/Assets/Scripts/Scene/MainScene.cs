using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainScene : MonoBehaviour
{
    [SerializeField] Button Btn_Start;
    [SerializeField] Fade fade;
    [SerializeField] Animator anim;
    [SerializeField] Animator Titleanim;
    // Start is called before the first frame update
    void Start()
    {
        bool sound = true;
        if (PlayerPrefs.HasKey("IsSound"))
        {
            sound = PlayerPrefs.GetInt("IsSound") == 1 ? true : false;
        }
        AudioManager.Inst.IsMusicOn = sound;
        AudioManager.Inst.IsSoundOn = sound;
        AudioManager.Inst.PlayBGM("bg_main_2", MusicTransition.LinearFade);
        anim.Play("MainSceneAnim");
        Titleanim.Play("TitleAnim");
        fade.FadeOut();
        Btn_Start.onClick.AddListener(Btn_OnClick_Start);
    }

    void Btn_OnClick_Start()
    {
        fade.FadeIn("MenuScene");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
