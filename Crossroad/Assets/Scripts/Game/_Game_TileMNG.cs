using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//━━━━━━━━━━━━━━━━━━━━//
// 함수 옆 ▣ 표시는 함수설명을 위한 주석
//━━━━━━━━━━━━━━━━━━━━//

public class _Game_TileMNG : MonoBehaviour
{
	private int tileCountX; // 타일의 한 X 개수
	private int tileCountY; // 타일의 한 Y 개수
	public int curQuizIndex; // 현재 열고있는 퀴즈 번호

	private float originWidth, originHeight;
	private RectTransform parent;
	private GridLayoutGroup grid;
	private GameObject prefabTile; // 기본형 타일
	private Dictionary<int, int> dictHint = new Dictionary<int, int>(); // 퀴즈 힌트 연 횟수 <퀴즈 id,성구 몇번인지>
	public  Dictionary<int, bool> dictAnswer = new Dictionary<int, bool>(); // 퀴즈 맞춘 여부

	[SerializeField] Text txtLore; // 설명
	[SerializeField] Text txtTitle; // 제목
	[SerializeField] Text txtExplain; // 추가설명(해설)
	[SerializeField] Button[] btnHint; // 버튼 난이도설정
	[SerializeField] Image[] ImageHint; // 버튼 난이도설정
	[SerializeField] Button btnAnswer; // 정답버튼
	[SerializeField] List<int> pinkTilelist_1 = new List<int>(); // 분홍타일 인덱스 지정
	[SerializeField] List<int> pinkTilelist_2 = new List<int>(); // 분홍타일 인덱스 지정
	[SerializeField] List<int> pinkTilelist_3 = new List<int>(); // 분홍타일 인덱스 지정
	[SerializeField] Transform ContentTxt;

    public void PlusScore(int id)
    {
		switch (dictHint[id])
		{
			case 0 : GameMgr.Inst.m_gameInfo.StageCurScore += 20; break;
			case 1 : GameMgr.Inst.m_gameInfo.StageCurScore += 10; break;
			case 2 : GameMgr.Inst.m_gameInfo.StageCurScore += 5; break;
		}
    }
	//IEnumerator Enum_Scroll()
    //{
	//	yield return new WaitForSeconds(Time.deltaTime * 3);
	//	Vector3 pos = new Vector3(-230, 94.27702f, 1);
	//	ContentTxt.position = pos;
    //}

	public void Clear()//청소
    {
		var child = this.GetComponentsInChildren<Transform>();
		foreach (var iter in child)
		{
			// 부모(this.gameObject)는 삭제 하지 않기 위한 처리
			if (iter != this.transform)
			{
				Destroy(iter.gameObject);
			}
		}
		dictAnswer.Clear();
		dictHint.Clear();

		if(GameMgr.Inst.m_gameInfo.StageNum > 0)
        {
			tileCountX = 5;
			tileCountY = 5;
		}
		if(GameMgr.Inst.m_gameInfo.StageNum > 5)
		{
			tileCountX = 7;
			tileCountY = 7;
		}
		if (GameMgr.Inst.m_gameInfo.StageNum > 10)
        {
			tileCountX = 9;
			tileCountY = 10;
		}
	}

	public void SetPinkTile(int tileCount)
	{
		if(tileCount == 5)
        {
			for (int i = 0; i < pinkTilelist_1.Count; i++)
				transform.GetChild(pinkTilelist_1[i]).localScale = new Vector3(1, 1, 2); // 분홍타일은 스리슬쩍 z스케일을 2로 구분함
		}
		else if (tileCount == 7)
		{
			for (int i = 0; i < pinkTilelist_2.Count; i++)
				transform.GetChild(pinkTilelist_2[i]).localScale = new Vector3(1, 1, 2); // 분홍타일은 스리슬쩍 z스케일을 2로 구분함
		}
		else if (tileCount == 9)
		{
			for (int i = 0; i < pinkTilelist_3.Count; i++)
				transform.GetChild(pinkTilelist_3[i]).localScale = new Vector3(1, 1, 2); // 분홍타일은 스리슬쩍 z스케일을 2로 구분함
		}
	}

    void Awake()
	{
		prefabTile = Resources.Load("Prefabs/prefabTile", typeof(GameObject)) as GameObject;
		parent = gameObject.GetComponent<RectTransform>();
		grid = gameObject.GetComponent<GridLayoutGroup>();

		originWidth = parent.rect.width;
		originHeight = parent.rect.height;
	}

    public void Initialize()
    {
		Clear();
		CreateTile(); // 타일 생성
		// 생성된 타일들의 사이즈 맞게 조절
		SetDynamicGrid(tileCountX * tileCountY, tileCountX, tileCountX); 
		SetTextSize(); // 텍스트 설정
		SetPinkTile(tileCountX);
		if(dictAnswer.Count > 0)
			SelectNextQuiz(); // 첫 퀴즈 자동선택
    }

    public void CreateTile() // 타일 생성
    {
		// n X n 의 개수에 맞게 생성
		for(int i = 0; i < tileCountX * tileCountY; i++)
        {
			GameObject kObj = Instantiate(prefabTile, transform);
        }
		grid.constraintCount = tileCountY;
	}

	public void onClickedHint(int index) // ▣ 힌트 버튼을 누를 시 오는 함수
    {
		if (curQuizIndex == 0) return;

		SetHintImage(index); // 힌트 외관 변경
		onClickedButton(curQuizIndex, index); // 표시 데이터 바꿔주기
	}

	public void SetHintImage(int index) // ▣ 힌트버튼의 외관을 변경하는 
    {
		// ▼ 전부 안보이게
		for (int i = 0; i < 3; i++)
		{
			btnHint[i].GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
			ImageHint[i].gameObject.SetActive(false);
			btnHint[i].transform.GetChild(0).transform.gameObject.SetActive(false);
		}
		// ▲ 전부 안보이게 

		// 선택한 힌트버튼 활성화
		ImageHint[index].gameObject.SetActive(true);
		btnHint[index].GetComponent<Image>().color = new Color(1, 1, 1, 1);

		// 만약 내가 선택한 힌트가 더 높은 수준의 힌트라면
		if (dictHint[curQuizIndex] < index)
			dictHint[curQuizIndex] = index; // dict[퀴즈번호]에 마지막으로 연 힌트의 번호를 저장

		for (int i = 0; i <= dictHint[curQuizIndex]; i++) // dict[퀴즈번호] 열었던 퀴즈의 개수만큼 반복
			btnHint[i].transform.GetChild(0).transform.gameObject.SetActive(true); // 체크표시 전부 활성화
	}


	public void SetTextSize() // ▣ 타일 안에있는 텍스트 크기 조절
    {
		// 텍스트의 사이즈 재조정 ( 재조정된 타일 사이즈에 맞게 텍스트도 조정 )
		for (int i = 0; i < tileCountX * tileCountY; i++)
			transform.GetChild(i).GetChild(0).GetComponent<RectTransform>().sizeDelta = grid.cellSize;
		// 테이블데이터에서 설정된 퀴즈 적용
		for (int i = 0; i < AssetMgr.Inst.m_AssetQuiz.Count; i++)
        {
			AssetQuiz kAss = AssetMgr.Inst.m_AssetQuiz[i]; // 퀴즈 하나 불러와서
			if (kAss.m_StageId == GameMgr.Inst.m_gameInfo.StageNum)
			{
				if (kAss.m_id != 0)
				{
					if(dictHint.ContainsKey(kAss.m_id) == false)
						dictHint.Add(kAss.m_id, 0); // 퀴즈는 기본적으로 힌트를 하나도 연상태가 아니여야함
					if (dictAnswer.ContainsKey(kAss.m_id) == false)
						dictAnswer.Add(kAss.m_id, false); // 퀴즈는 기본적으로 풀어져 있지 않은 상태여야함
				}
				SetQuizData(kAss.startpos, kAss.dir, kAss.m_id, kAss.text); // 실제 타일에 적용
			}
        }
	}

	public void SetQuizData(int start, int dir, int id, string text) // ▣ 타일에 들어간 총 퀴즈데이터 설정
    {
		// start : 퀴즈타일의 시작인덱스
		// dir : 퀴즈타일의 문자가 진행될 방향 ( 0 : 가로 , 1 : 세로 )
		// text : 타일 하나하나에 들어갈 문자열

		for (int i = 0; i < text.Length; i++) // 텍스트의 길이만큼 생성
		{
			int index = start;

			// 방향에 따른 처리
			if (dir == 0) index = start + i;
			if (dir == 1) index = start + (i * tileCountX);

			// 텍스트 하나하나 분리
			List<char> ch = new List<char>();
			for (int j = 0; j < text.Length; j++)
				ch.Add(text[j]);

			if (index < transform.childCount)
			{
				// 타일 흰색으로 변경 *(원래는 검은색)
				transform.GetChild(index).GetComponent<Image>().color = Color.white;
			// 텍스트 한글자 넣기
				if (!ch[i].Equals('/'))
					transform.GetChild(index).GetChild(0).GetComponent<Text>().text = ch[i].ToString();
				if (ch[i].Equals(';'))
					transform.GetChild(index).GetChild(0).GetComponent<Text>().text = "같은";
			// 아이디가 0이 아니면 정보 표시
			if (id > 0)
				transform.GetChild(index).GetComponent<Button>().onClick.AddListener(() => { onClickedButton(id); });
            }
		}
	}

	public void SelectNextQuiz() // ▣ 안푼 퀴즈 자동선택
    {
		bool b = true;
		foreach (int Key in dictAnswer.Keys)  // 모든 퀴즈 반복
		{
			if (!dictAnswer[Key]) // 안푼퀴즈가있으면
			{
				onClickedButton(Key); // 그 퀴즈의 번호로 재선택
				b = false;
				break;
			}
		}
		btnAnswer.interactable = dictAnswer[curQuizIndex] ? false : true; // 버튼활성화 여부 (3항 연산자)

		if (b == true)
		{
			GameMgr.Inst.m_GameScene.resultUI.Open();//결과창 오픈	  
		}
		else if (AnswerCheck() == true)
		{
			GameMgr.Inst.m_GameScene.resultUI.Open();//결과창 오픈	  
		}	
	}

	public void SelectQuizColor(int id) // ▣ 선택한 퀴즈 범위 초록색으로 표시
	{
		// 모든 타일을 형식에 맞게 전부 초기화
		for (int i = 0; i < tileCountX * tileCountY; i++)
		{
			if (transform.GetChild(i).GetComponent<Image>().color == new Color(0, 0.55f, 0))
				transform.GetChild(i).GetComponent<Image>().color = Color.white;

			if(transform.GetChild(i).localScale.z == 2) // 분홍타일의 구분점.. z스케일이 2이면
				transform.GetChild(i).GetComponent<Image>().color = new Color(1,0.5f,0.5f); // 분홍타일로 따로 변경

			if (transform.GetChild(i).GetChild(0).GetComponent<Text>().color == Color.white)
				transform.GetChild(i).GetChild(0).GetComponent<Text>().color = Color.black;
		}

		// 퀴즈 번호에 해당하는 타일을 초록색으로 활성화
		for (int j = 0; j < AssetMgr.Inst.m_AssetQuiz.Count; j++)
		{
			AssetQuiz kAss = AssetMgr.Inst.m_AssetQuiz[j];
			if (kAss.m_id == id)
			{
				for (int i = 0; i < kAss.text.Length; i++)
				{
					int index = kAss.startpos;

					// 방향에 따른 처리
					if (kAss.dir == 0) index = kAss.startpos + i;
					if (kAss.dir == 1) index = kAss.startpos + (i * tileCountX);

					transform.GetChild(index).GetComponent<Image>().color = new Color(0, 0.55f, 0);
					transform.GetChild(index).GetChild(0).GetComponent<Text>().color = Color.white;
				}
			}
		}
	}

	public void OpenQuizAnswer(int id) // ▣ 선택한 퀴즈 정답 공개
	{
		for (int j = 0; j < AssetMgr.Inst.m_AssetQuiz.Count; j++)
		{
			AssetQuiz kAss = AssetMgr.Inst.m_AssetQuiz[j];
			if (kAss.m_id == id)
			{
				for (int i = 0; i < kAss.text.Length; i++)
				{
					int index = kAss.startpos;

					// 방향에 따른 처리
					if (kAss.dir == 0) index = kAss.startpos + i;
					if (kAss.dir == 1) index = kAss.startpos + (i * tileCountX);

					// 텍스트 하나하나 분리
					List<char> ch = new List<char>();
					for (int k = 0; k < kAss.answer.Length; k++)
						ch.Add(kAss.answer[k]);

					// 텍스트 공개하기
					transform.GetChild(index).GetChild(0).GetComponent<Text>().text = ch[i].ToString();
				}
			}
		}
		AnswerCheck();
		SelectNextQuiz();
	}

	public void onClickedButton(int id, int hint = -1) // ▣ 퀴즈타일을 누르거나, 힌트버튼을 누를경우 여기에 옴.
    {
		if (curQuizIndex != id)
		{
			SelectQuizColor(id); // 선택한 퀴즈범위 색칠
			curQuizIndex = id; // 현재 선택퀴즈 변수 바꾸기
			btnAnswer.interactable = dictAnswer[curQuizIndex] ? false : true; // 정답버튼 표시 여부
		}

		// 퀴즈번호랑 일치하는 실제 데이터 가져오기
		AssetQuizData kAss = new AssetQuizData(); 
        for (int i = 0; i < AssetMgr.Inst.m_AssetQuizData.Count; i++)
        {
			if(id == AssetMgr.Inst.m_AssetQuizData[i].m_id)
            {
				kAss = AssetMgr.Inst.m_AssetQuizData[i];
				break;
			}
        }


		// hint : 현재 내가 몇번째 힌트를 볼것인지
		if (hint == -1) hint = dictHint[id]; // 버튼을 눌러서 이 함수에 온경우
											 // 기본값인 -1이 들어오게된다. 이 경우 내가 마지막까지 연 힌트의 인덱스를 적용시킴.

		// 제목 수정 및
		// 힌트의 값에 따라서 보여줄 데이터에 차이를 둠.
		if (hint == 0)
		{
			txtLore.text = ConvertLore(kAss.hint1);
			txtTitle.text = kAss.title1;
		}
        if (hint == 1) 
        {
			txtLore.text = ConvertLore(kAss.hint2);
			txtTitle.text = kAss.title2;
		}
		if (hint == 2)
		{
			string ExplainStr = kAss.explain;
			txtLore.text = ConvertLore(kAss.hint3) + string.Format("\n\n<color=#B9BEFF>해설 : {0}</color>", ExplainStr);
			txtTitle.text = kAss.title3;
		}
		SetHintImage(hint); // 힌트 외관 변경
	}

    private string ConvertLore(string hint1)
    {
		return hint1.Replace("\\n", "\n");
    }

    // cnt : 컬럼 총 갯수, minColsInARow : 한 Row에 최소 컬럼 갯수, maxRow : 최대 Row 수.
    public void SetDynamicGrid(int cnt, int minColsInARow, int maxRow)
	{
		int rows = Mathf.Clamp(Mathf.CeilToInt((float)cnt / minColsInARow), 1, maxRow + 1);
		int cols = Mathf.CeilToInt((float)cnt / rows);

		float spaceW = (grid.padding.left + grid.padding.right) + (grid.spacing.x * (cols - 1));
		float spaceH = (grid.padding.top + grid.padding.bottom) + (grid.spacing.y * (rows - 1));

		float maxWidth = originWidth - spaceW;
		float maxHeight = originHeight - spaceH;

		float width = Mathf.Min(parent.rect.width - (grid.padding.left + grid.padding.right) - (grid.spacing.x * (cols - 1)), maxWidth);
		float height = Mathf.Min(parent.rect.height - (grid.padding.top + grid.padding.bottom) - (grid.spacing.y * (rows - 1)), maxHeight);

		grid.cellSize = new Vector2(width / cols, height / rows);
	}

	public bool AnswerCheck()
	{
		Color SelectColor = new Color(0, 0.55f, 0);
		bool check = true;

		for (int i = 0; i < tileCountX * tileCountY; i++)
		{
			foreach (int Key in dictAnswer.Keys)  // 모든 퀴즈 반복
			{
				if (transform.GetChild(i).GetComponent<Image>().color == Color.white || transform.GetChild(i).GetComponent<Image>().color == SelectColor)
				{
					if (transform.GetChild(i).GetChild(0).GetComponent<Text>().text == "")
						check = false;
				}
			}
		}
		return check;
	}
}
