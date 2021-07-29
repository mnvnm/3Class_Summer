using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    [SerializeField] GameObject HideAnswerImg;//자기 자신의 정답을 가릴 그림
    [SerializeField] string Answer = "";//정답 string
    public int Type;//0 이면 흰색 버튼타일 / 1이면 검정색 클릭 안되는 타일/ 2이면 분홍색 타일
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
