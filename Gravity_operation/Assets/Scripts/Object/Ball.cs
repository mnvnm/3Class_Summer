using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    [SerializeField] Transform StartPos = null;// 시작 지점
    [SerializeField] GameObject TrailObjPrefabs = null;//꼬리 프리팹
    GameObject trailTemp;
    Rigidbody2D Rigd = null;
    int dir = 1; // 이 공의 방향 정해주는 변수 // 1 = 오른쪽 // -1 = 왼쪽
    [HideInInspector] public int GravityDir = 1; // 이 공의 "중력" 방향 정해주는 변수 // 1 = 아래 // -1 = 위
    [HideInInspector] public float GravityScale = 1; // 공의 중력 크기 조정 변수
    bool IsNoneGravity = false;
    float posX; // 포지션 X
    float posY; // 포지션 Y
    float veloX; // 힙 적용 값 X
    float veloY; // 힘 적용 값 Y
    float TimeToInBlackHole = 5.0f; // 블랙홀에 있을 타임 변수

    SpriteRenderer Map_Sprite; // 배경 화면
    float mapMinY; // 맵 마지막으로 벗어나면 사망
    float mapMaxY; 
    float mapMinX; 
    float mapMaxX; 

    Vector2 GravityVector; // 중력 스케일 조정 변수
    Vector2 TempVelocity;// 속도 임시 저장 변수
    void Start()
    {
        Rigd = GetComponent<Rigidbody2D>();// 리지드 바디 가져오고
    }

    public void Initialize()
    {
        Map_Sprite = GameObject.Find("Bg_Map").GetComponent<SpriteRenderer>();// 배경 그림 가져오고
        Physics2D.gravity = new Vector2(0, -9.81f);
        GravityScale = 1; // 중력 기본 스케일 지정
        GravityDir = 1; // 중력 기본 방향 지정
        veloX = 0; // 공의 X 힘 값
        veloY = 0; // 공의 Y 힘 값 

        //맵의 끝 좌표들
        mapMinY = Map_Sprite.transform.position.y - Map_Sprite.bounds.size.x / 2;
        mapMinX = Map_Sprite.transform.position.x - Map_Sprite.bounds.size.x / 2;
        mapMaxY = Map_Sprite.transform.position.y + Map_Sprite.bounds.size.x / 2;
        mapMaxX = Map_Sprite.transform.position.x + Map_Sprite.bounds.size.x / 2;
        //---------------
        Rigd.velocity = new Vector2(0, 0);// 현재 공의 작용하는 힘의 값 초기화
        Rigd.gravityScale = 0;// 공이 받는 중력 스케일 0 으로 초기화
        this.transform.position = StartPos.position;
        if (GameMgr.Inst.m_GameScene.btlFSM.IsGameState()) // 지금 플레이 버튼을 눌렀냐
        {
            if (trailTemp != null)// 꼬리가 이미 있다면 삭제
            {
                trailTemp = null;
                Destroy(GameObject.Find("TRAIL"));
            }
            IsNoneGravity = false; // 무중력이냐? / 아니
            Rigd.gravityScale = 1;
            GravityVector.x = Physics2D.gravity.x; // 중력 벡터 초기화
            GravityVector.y = Physics2D.gravity.y;
            Rigd.velocity = new Vector2(4, 0);// 공을 x 쪽으로 4만큼 힘을 가함
            GameObject trail = Instantiate(TrailObjPrefabs, GameMgr.Inst.m_GameScene.m_gameUI.transform); // 꼬리 다시 생성
            trail.name = "TRAIL";
            trailTemp = trail;
        }
    }
    void Update()
    {
        if (GameMgr.Inst.m_GameScene.btlFSM.IsGameState())
        {
            trailTemp.transform.position = this.transform.position;

            if (this.transform.position.y <= mapMinY || this.transform.position.y >= mapMaxY
                || this.transform.position.x <= mapMinX || this.transform.position.x >= mapMaxX) // 맵 넘어가면 다시 에딧 상태
                GameMgr.Inst.m_GameScene.btlFSM.SetActState();


            if (IsNoneGravity == true) // 무중력이냐? / 응
            {
                Physics2D.gravity = new Vector2(0,0); // 모든 중력값 0으로 초기화
            }
            else
            {
                Physics2D.gravity = GravityVector; // 아니면 중력 벡터 값 넣어주기
            }

        }
        else if (GameMgr.Inst.m_GameScene.btlFSM.IsEditState()) // 에딧 상태때의 공의 모습을 변환
        {
            this.gameObject.transform.position = StartPos.position;
            Rigd.velocity = new Vector2(0, 0); // 안움직이게
            if (this.transform.localRotation.z != 0) // 안돌아가게
                this.transform.localRotation = new Quaternion(0, 0, 0, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Trap")//트랩을 밟았을 때 (가시 또는 전기장판을 밟았을 때)
        {
            GameMgr.Inst.m_GameScene.btlFSM.SetActState();// Act 상태로 전환
        }
        else if (collision.gameObject.tag == "Ground" && GameMgr.Inst.m_GameScene.btlFSM.IsGameState())//땅에 닿았을때 힘을 실어준다.
        {
            if(Rigd.velocity.x >= 0 && Rigd.velocity.x <= 0.5f)
            {
                Rigd.velocity = new Vector2(4, 0);
            }
            else if(Rigd.velocity.x < 0 && Rigd.velocity.x >= -0.5f)
            {
                Rigd.velocity = new Vector2(-4, 0);
            }
        }
    }

    //중력장 닿았을때
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameMgr.Inst.m_GameScene.m_hudUI.editDlg.GetItemIndex() == 0 && GameMgr.Inst.m_GameScene.btlFSM.IsGameState())
        {// 게임 상태 일때
            switch (collision.gameObject.tag)
            {
                case "Gravity_1": // 중력 2배 중력장
                    if(IsNoneGravity == false)
                    {
                        GravityScale = 2;
                        GravityVector = new Vector2(0, GravityVector.y * GravityScale);
                        Rigd.velocity = new Vector2(Rigd.velocity.x * 2, Rigd.velocity.y * 2);
                        Physics2D.gravity = GravityVector;
                    }
                    break;
                case "Gravity_2": // 무중력 중력장
                    IsNoneGravity = !IsNoneGravity;
                    break;
                case "Gravity_3": // 역방향 중력장
                        GravityDir *= -1;

                    GravityVector = new Vector2(0, -9.81f);
                    GravityVector = new Vector2(0, GravityVector.y * GravityScale * GravityDir);
                    if(IsNoneGravity == false)
                    {
                        Physics2D.gravity = GravityVector;
                    }
                    break;
                case "Gravity_4": // 블랙홀 중력장

                    TimeToInBlackHole = 10.0f;
                    break;
                case "Trans":// 방향 바꾸는 발판을 밟았을 때

                    if (dir == 1)
                        dir = -1;
                    else if (dir == -1)
                        dir = 1;

                        Rigd.velocity = new Vector2(-Rigd.velocity.x * 2, Rigd.velocity.y) ;

                    collision.gameObject.GetComponent<Animator>().SetTrigger("IsTrans");
                    break;
                case "Goal":
                    GameMgr.Inst.m_GameScene.bIsSuccessed = true;
                    GameMgr.Inst.m_GameScene.btlFSM.SetWaveState();
                    break;
            }
        }
    }
    //빠져 나올때
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (GameMgr.Inst.m_GameScene.m_hudUI.editDlg.GetItemIndex() == 0 && GameMgr.Inst.m_GameScene.btlFSM.IsGameState())
        {
            switch (collision.gameObject.tag)
            {
                case "Gravity_1": // 중력 2배 중력장
                    GravityScale = 1;
                    GravityVector = new Vector2(0, -9.81f);
                    GravityVector = new Vector2(0, GravityVector.y * GravityScale * GravityDir);
                    Physics2D.gravity = GravityVector;
                    break;
                case "Gravity_2": // 무중력 중력장

                    break;
                case "Gravity_3": // 역방향 중력장

                    break;
                case "Gravity_4": // 블랙홀 중력장
                    break;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (GameMgr.Inst.m_GameScene.m_hudUI.editDlg.GetItemIndex() == 0 && GameMgr.Inst.m_GameScene.btlFSM.IsGameState())
        {
            switch (collision.gameObject.tag)
            {
                case "Gravity_1": // 중력 2배 중력장
                    GravityScale = 2;
                    break;
                case "Gravity_4": // 블랙홀 중력장
                    posX = Mathf.Lerp(this.gameObject.transform.position.x, collision.transform.position.x, Time.deltaTime);
                    posY = Mathf.Lerp(this.gameObject.transform.position.y, collision.transform.position.y, Time.deltaTime);
                    this.gameObject.transform.position = new Vector2(posX, posY);
                    veloX = Mathf.Lerp(veloX, 0, Time.deltaTime);
                    veloY = Mathf.Lerp(veloY, 0, Time.deltaTime);
                    Rigd.velocity = new Vector2(veloX, veloY);
                    TimeToInBlackHole -= Time.deltaTime;
                    DestroyBlackHole();
                    break;
            }
        }
    }

    void DestroyBlackHole()// 블랙홀에 오래 있으면 공 뒤지게 만듬
    {
        if (TimeToInBlackHole <= 0)
        {
            GameMgr.Inst.m_GameScene.btlFSM.SetEditState();
        }
    }
}
