using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SaveData : MonoBehaviour
{
    public static SaveData Instance; // SaveData 객체 저장
    private GameObject player; // 플레이어
    private GameObject[] npc; // Npc 리스트
    private GameObject[] food;
    private ObjData[] npcObjData; // Npc 리스트의 ObjData 저장
    public int[] eatList;
    private int foodEatIndex = 0;
    public Transform playerPos; // 플레이어 위치 저장
    
    private float disease = 0.0f; 
    private float hungry = 0.0f; 
    private bool onDisease = false; // 감염 상태 확인
    private Color diseaseColor;

    private int goodFoodCount = 0;
    private int badFoodCount = 0;
    private int dangerFoodCount = 0;
    private int fatalFoodCount = 0;
    // 미니게임 ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
    private bool clearRoadGame_1 = false;
    private bool clearRoadGame_2 = false;
    private bool clearRoadGame_3 = false;
    private bool clearQuiz = false;
    private bool clearFrisbee = false;
    private bool clearTimeAttack = false;

    private int quizScore;
    // ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
    
    // 퀘스트 ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
    private bool ingQuest_1 = false;
    private bool ingQuest_2 = false;
    private bool ingQuest_3 = false;

    private bool clearQuest_1 = false;
    private bool clearQuest_2 = false;
    private bool failQuest_2 = false;
    private bool clearQuest_3 = false;
    // ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
    
    private int nowscene; // 현재 씬
    private int beforescene; // 이전 씬
    
    void Awake()
    {
        if (Instance != null) // SaveData 오브젝트가 이미 존재하면
        {
            Destroy(gameObject); // 새로 생긴 SaveData 제거
            return;
        }
        Instance = this;

        DontDestroyOnLoad(transform.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        nowscene = 0;
        SetPlayer();

        playerPos.position = player.transform.position; // 포지션값 초기화
        npc = GameObject.FindGameObjectsWithTag("NPC"); // 메인 씬의 NPC 태그를 가진 모든 오브젝트 리스트에 추가
        npcObjData = new ObjData[npc.Length]; // npcObjData 초기화
        for (int i = 0; i < npc.Length; i++)
        {
            npcObjData[i] = npc[i].GetComponent<ObjData>(); // npcObjData에 데이터 저장
        }
        
        food = GameObject.FindGameObjectsWithTag("Food");
        eatList = new int[food.Length];
    }

    // Update is called once per frame
    void Update()
    {
        beforescene = nowscene;
        CheckScene();
        ChangeScene();
        if (nowscene == 0) // 메인 씬이면 playerPos에 player의 현재 Position, Rotation 저장
        {
            playerPos.position = player.transform.position;
            playerPos.rotation = player.transform.rotation;
        }
        
    }

    public void AddEatList(FoodObjData nowEat)
    {
        eatList[foodEatIndex] = nowEat.GetFoodId();
        foodEatIndex++;
        if (nowEat.GetFoodId() < 2000 && nowEat.GetFoodId() > 1000)
        {
            goodFoodCount++;
        }
        else if (nowEat.GetFoodId() < 3000 && nowEat.GetFoodId() > 2000)
        {
            badFoodCount++;
        }
        else if (nowEat.GetFoodId() < 4000 && nowEat.GetFoodId() > 3000)
        {
            dangerFoodCount++;
        }
        else if (nowEat.GetFoodId() < 5000 && nowEat.GetFoodId() > 4000)
        {
            fatalFoodCount++;
        }
    }

    public int[] ReturnFoodCount()
    {
        int[] counts = new int[4];
        counts[0] = goodFoodCount;
        counts[1] = badFoodCount;
        counts[2] = dangerFoodCount;
        counts[3] = fatalFoodCount;

        return counts;
    }

    public bool[] ReturnQuestCount()
    {
        bool[] counts = new bool[3];
        counts[0] = GetClearQuest_1();
        counts[1] = GetClearQuest_2();
        counts[2] = GetClearQuest_3();

        return counts;
    }
    
    public bool[] ReturnMinigameCount()
    {
        bool[] counts = new bool[4];
        counts[0] = GetClearRoadGame_1();
        counts[1] = GetClearRoadGame_2();
        counts[2] = GetClearRoadGame_3();
        counts[3] = GetClearQuiz();

        return counts;
    }

    public int[] GetEatList()
    {
        return eatList;
    }
    
    private void SetPlayer() // 플레이어 찾기
    {
        if (GameObject.FindGameObjectWithTag("Cat") != null) // Cat 태그를 가진 오브젝트가 있으면
        {
            player = GameObject.FindGameObjectWithTag("Cat"); // player에 Cat을 등록
        }
        else
        {
            player = GameObject.FindGameObjectWithTag("Dog"); // 없으면 Dog를 등록
        }
    }

    public void SaveDisease(float disease)
    {
        this.disease = disease;
    }
    
    public void SaveHungry(float hungry)
    {
        this.hungry = hungry;
    }
    
    public void SaveOnDisease(bool od)
    {
        onDisease = od;
    }

    public void SaveDiseaseStateColor(Color color)
    {
        diseaseColor = color;
    }
    
    public float GetSaveDisease()
    {
        return disease;
    }
    
    public float GetSaveHungry()
    {
        return hungry;
    }
    
    public bool GetSaveOnDisease()
    {
        return onDisease;
    }

    public Color GetSaveDiseaseStateColor()
    {
        return diseaseColor;
    }
    
    private void CheckScene() // 현재 씬 체크
    {
        if (SceneManager.GetActiveScene().name == "MainMap")
            nowscene = 0;
        else
            nowscene = 1;
    }

    private void ChangeScene() // 메인 씬으로 돌아온다면
    {
        if (nowscene == 0 && nowscene != beforescene)
        {
            SetPlayer();
        }
    }

    // 클리어 설정 함수 ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
    public void ClearRoadGame_1()
    {
        clearRoadGame_1 = true;
    }
    
    public void ClearRoadGame_2()
    {
        clearRoadGame_2 = true;
    }
    
    public void ClearRoadGame_3()
    {
        clearRoadGame_3 = true;
    }
    
    public void ClearQuiz()
    {
        clearQuiz = true;
    }

    public void SetQuizScore(int score)
    {
        quizScore = score;
    }
    
    public void ClearFrisbee()
    {
        clearFrisbee = true;
    }

    public void ClearTimeAttack()
    {
        clearTimeAttack = true;
    }

    public void ClearQuest_1()
    {
        clearQuest_1 = true;
    }

    public void ClearQuest_2()
    {
        clearQuest_2 = true;
    }

    public void FailQuest_2()
    {
        failQuest_2 = true;
    }

    public void ClearQuest_3()
    {
        clearQuest_3 = true;
    }
    
    // ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ

    // 클리어 여부 로드 함수
    public bool GetClearRoadGame_1()
    {
        return clearRoadGame_1;
    }
    
    public bool GetClearRoadGame_2()
    {
        return clearRoadGame_2;
    }
    
    public bool GetClearRoadGame_3()
    {
        return clearRoadGame_3;
    }
    
    public bool GetClearQuiz()
    {
        return clearQuiz;
    }

    public int GetQuizScore()
    {
        return quizScore;
    }
    
    public bool GetClearFrisbee()
    {
        return clearFrisbee;
    }

    public bool GetClearTimeAttack()
    {
        return clearTimeAttack;
    }

    public bool GetClearQuest_1()
    {
        return clearQuest_1;
    }

    public bool GetClearQuest_2()
    {
        return clearQuest_2;
    }

    public bool GetFailQuest_2()
    {
        return failQuest_2;
    }

    public bool GetClearQuest_3()
    {
        return clearQuest_3;
    }
    
    // ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
    
    // 퀘스트 진행중
    public void SetIngQuest_1(bool ing)
    {
        ingQuest_1 = ing;
    }
    
    public void SetIngQuest_2(bool ing)
    {
        ingQuest_2 = ing;
    }
    
    public void SetIngQuest_3(bool ing)
    {
        ingQuest_3 = ing;
    }

    public bool GetIngQuest_1()
    {
        return ingQuest_1;
    }
    
    public bool GetIngQuest_2()
    {
        return ingQuest_2;
    }
    
    public bool GetIngQuest_3()
    {
        return ingQuest_3;
    }
    
    // ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
    
    public void SearchAndSaveChangedId(GameObject Npc) // Npc의 ObjData에서 Id 변화를 감지하면
    {
        for (int i = 0; i < npcObjData.Length; i++) // NpcObjData 리스트에서 찾아 수정
        {
            if (npcObjData[i].name == Npc.GetComponent<ObjData>().name)
            {
                npcObjData[i].id = Npc.GetComponent<ObjData>().id;
            }
        }
    }

    public int GetNpcLength()
    {
        if (npcObjData == null) // 데이터 로딩과정 상 npcObjData에 아직 아무것도 들어있지 않다면
        {
            return 0; // 0을 반환
        }
        else
        {
            return npcObjData.Length;
        }
    }

    public string GetNpcName(int index) // Npc 이름 반환
    {
        return npcObjData[index].name;
    }

    public int GetNpcId(int index) // Npc Id 반환
    {
        return npcObjData[index].id;
    }

    public Transform GetPlayerPos() // PlayerPos 반환
    {
        return playerPos;
    }

    public void SetPlayerPos(Transform Pos)
    {
        playerPos.position = Pos.position;
    }
}
