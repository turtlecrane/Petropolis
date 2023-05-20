using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadData : MonoBehaviour
{
    private SaveData saveData;
    private GameObject[] npc;
    private GameObject player;
    private GameObject[] food;
    private Transform playerPos;
    
    public MiniGameManager mgManager;

    public QuestManager qManager;

    public FoodManager fManager;
    // Start is called before the first frame update

    void Awake()
    {
        saveData = GameObject.Find("SaveData").GetComponent<SaveData>(); // SaveData 불러오기
        SetPlayer();
        if (saveData.GetSaveHungry() != 0.0f)
        {
            fManager.LoadHungry(saveData.GetSaveHungry());
            if (saveData.GetSaveOnDisease())
            {
                fManager.LoadOnDisease(saveData.GetSaveOnDisease());
                fManager.LoadDisease(saveData.GetSaveDisease());
            }
        }
        else
        {
            fManager.LoadHungry(fManager.maxHungry);
        }

        playerPos = saveData.GetPlayerPos(); // PlayerPos 불러오기
        player.transform.position = playerPos.position; // player에 이전 transform 적용
        player.transform.rotation = playerPos.rotation;
    }
    void Start()
    {
        SetClear();
        
        ///ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
        if (saveData.GetNpcLength() != 0) // 저장된 Npc 데이터가 있다면
        {
            npc = GameObject.FindGameObjectsWithTag("NPC");
            for (int i = 0; i < saveData.GetNpcLength(); i++) 
                // 기존에 저장된 Npc의 Id와 새로 생긴 Npc의 Id가 다르다면 저장된 Npc의 Id로 변환
            {
                if (npc[i].GetComponent<ObjData>().name == saveData.GetNpcName(i) &&
                    npc[i].GetComponent<ObjData>().id != saveData.GetNpcId(i))
                {
                    npc[i].GetComponent<ObjData>().id = saveData.GetNpcId(i);
                }

                if (npc[i].GetComponent<ObjData>().name == "메건" ||
                    npc[i].GetComponent<ObjData>().name == "검둥이")
                {
                    if (qManager.GetIngQuest_1())
                    {
                        if (npc[i].GetComponent<ObjData>().name == "검둥이")
                        {
                            npc[i].gameObject.SetActive(false);
                        }
                    }
                    else
                    {
                        if (qManager.GetClearQuest_1())
                        {
                            npc[i].gameObject.SetActive(false);
                        }
                        else
                        {
                            npc[i].gameObject.SetActive(true);
                        }
                    }
                }
            }
        }
        ///ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ

        int[] eatList = saveData.GetEatList();
        if (eatList != null)
        {
            food = GameObject.FindGameObjectsWithTag("Food");
            for (int i = 0; i < eatList.Length; i++)
            {
                if (eatList[i] == 0)
                {
                    break;
                }
                else
                {
                    for (int j = 0; j < food.Length; j++)
                    {
                        if (food[j].GetComponent<FoodObjData>().foodId == eatList[i])
                        {
                            food[j].SetActive(false);
                            break;
                        }
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void SetPlayer() // SaveData의 SetPlayer와 같음
    {
        if (GameObject.FindGameObjectWithTag("Cat") != null)
        {
            player = GameObject.FindGameObjectWithTag("Cat");
        }
        else
        {
            player = GameObject.FindGameObjectWithTag("Dog");
        }
    }

    private void SetClear() // 클리어 설정
    {
        mgManager.SetClearRoadGame_1(saveData.GetClearRoadGame_1());
        mgManager.SetClearRoadGame_2(saveData.GetClearRoadGame_2());
        mgManager.SetClearRoadGame_3(saveData.GetClearRoadGame_3());
        mgManager.SetClearQuiz(saveData.GetClearQuiz());
        if (mgManager.GetClearQuiz())
        {
            mgManager.SetQuizScore(saveData.GetQuizScore());
            mgManager.SetQuizNpcId();
        }
        qManager.SetIngQuest_1(saveData.GetIngQuest_1());
        qManager.SetClearQuest_1(saveData.GetClearQuest_1());
        qManager.SetIngQuest_2(saveData.GetIngQuest_2());
        qManager.SetClearQuest_1(saveData.GetClearQuest_2());
        qManager.SetIngQuest_3(saveData.GetIngQuest_3());
        qManager.SetClearQuest_3(saveData.GetClearQuest_3());
    }
}
