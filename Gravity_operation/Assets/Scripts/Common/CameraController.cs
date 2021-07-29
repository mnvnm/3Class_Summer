using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Camera cam = null;
    float ZoomSpeed = 10f;
    float minCamSize = 1, maxCamSize = 15;

    SpriteRenderer Map_Sprite;

    private float mapMinX, mapMinY, mapMaxX, mapMaxY;

    private Vector3 dragOrigin;

    GameObject Ball_Obj;
    Vector3 CamPos;
    [Range(0.01f,0.2f)]float ShakeRange = 0.1f;
    [Range(0.1f,1.0f)]float ShakeDuration = 0.5f;

    private void Awake()
    {
        GameMgr.Inst.SetCamControll(this);
        cam = GetComponent<Camera>();
    }

    void Start()
    {
    }

    public void Initialize()
    {
        CamPos = cam.transform.position;
        if (Ball_Obj == null)
        {
            Ball_Obj = GameMgr.Inst.m_GameScene.m_gameUI.ball.gameObject;
        }
        mapMinX = Map_Sprite.transform.position.x - Map_Sprite.bounds.size.x / 2;
        mapMaxX = Map_Sprite.transform.position.x + Map_Sprite.bounds.size.x / 2;
        mapMinY = Map_Sprite.transform.position.y - Map_Sprite.bounds.size.y / 2;
        mapMaxY = Map_Sprite.transform.position.y + Map_Sprite.bounds.size.y / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameMgr.Inst.m_GameScene.IsPause == false)
        {
            Zoom();
            if (GameMgr.Inst.m_GameScene.btlFSM.IsEditState())
            {
                DragOn();
            }
            else if (GameMgr.Inst.m_GameScene.btlFSM.IsGameState())
            {
                Ball_CameraMove(Ball_Obj);
            }
            else if (GameMgr.Inst.m_GameScene.btlFSM.IsActState())
            {
                Ball_CameraMove(Ball_Obj);
                if (Ball_Obj.transform.position.x - cam.transform.position.x < 0.01f && Ball_Obj.transform.position.x - cam.transform.position.x > -0.01f)
                {
                    GameMgr.Inst.m_GameScene.btlFSM.SetEditState();
                }
            }
        }
    }
    public void Shake()
    {
        AudioManager.Inst.PlaySFX("BallDie");
        //AudioManager.Inst.PlaySFX("BallDie_2");
        CamPos = cam.transform.position;
        InvokeRepeating("StartShake",0f,0.005f);
        Invoke("StopShake",ShakeDuration);
    }

    void StartShake()
    {
        float CamPosX = Random.value * ShakeRange * 2 - ShakeRange;
        float CamPosY = Random.value * ShakeRange * 2 - ShakeRange;
        Vector3 TempPos = cam.transform.position;
        TempPos.x += CamPosX;
        TempPos.y += CamPosY;
        TempPos.z = cam.transform.position.z;
        cam.transform.position = TempPos;
    }
    void StopShake()
    {
        CancelInvoke("StartShake");
    }
    void Zoom() // 휠을 당겨 줌을 할 때 들어오는 함수
    {
        float distance = Input.GetAxis("Mouse ScrollWheel") * -1 * ZoomSpeed; // <- 휠 당길때

        float NewSize = cam.orthographicSize + distance;
        cam.orthographicSize = Mathf.Clamp(NewSize, minCamSize, maxCamSize);
        if (Input.GetMouseButtonDown(2))
        {
            ZoomClear();
        }
    }

    public void ZoomClear() // 줌 사이즈 초기화
    {
        cam.orthographicSize = 5.0f;
    }

    void DragOn() // 마우스 드래그
    {
        if (Input.GetMouseButtonDown(0))
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);

        if(Input.GetMouseButton(0))
        {
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);

            cam.transform.position = ClampCamera(cam.transform.position + difference);
        }
    }

    private Vector3 ClampCamera(Vector3 targetPosition) // 드래그시 카메라 이동 및 맵 밖에 못가도록 막는 함수
    {
        float camHeight = cam.orthographicSize;
        float camWidth = cam.orthographicSize * cam.aspect;

        float minX = mapMinX + camWidth;
        float maxX = mapMaxX - camWidth;
        float minY = mapMinY + camHeight;
        float maxY = mapMaxY - camHeight;

        float newX = Mathf.Clamp(targetPosition.x, minX, maxX);
        float newY = Mathf.Clamp(targetPosition.y, minY, maxY);

        return new Vector3(newX, newY, targetPosition.z);
    }

    public void Ball_CameraMove(GameObject ball) // 공 따라다니는 함수
    {
        cam.transform.position = new Vector3(Mathf.Lerp(cam.transform.position.x,ball.transform.position.x,0.15f), Mathf.Lerp(cam.transform.position.y, ball.transform.position.y, 0.15f), cam.transform.position.z);
    }

    public void SetBg_Sprite(SpriteRenderer sprite)//배경 맵 가져오는 함수
    {
        Map_Sprite = sprite;
    }
}
