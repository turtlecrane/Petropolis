using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TalkManager : MonoBehaviour
{
    public int Split;
    private NpcController npcController;
    public DialogManager dialogManager;
    public InteractManager interactManager;
    public TimeattackManager TAManager;

    public TextMeshProUGUI NpcName;
    public TextMeshProUGUI Context;
    public GameObject scanObject;
    
    public int talkIndex;
    private GameObject megan;
    private PlayerName playerName;

    private void Start()
    {
        playerName = GameObject.Find("PlayerName").GetComponent<PlayerName>();
    }
    
    public void Action(GameObject scanObj)//NPC에 상호작용하면 이거 실행됨
    {
        scanObject = scanObj;
        ObjData objData = scanObject.GetComponent<ObjData>();
        npcController = scanObject.GetComponent<NpcController>();
        NpcName.text = "[ " + objData.name + " ]";
        Talk(objData.id, objData.isNpc);
    }

    void Talk(int id, bool isNpc)
    {
        string talkData = dialogManager.GetTalk(id, talkIndex);

        if (talkData == null) //다음 대화가 null 일때
        {
            Split = 0;
            npcController.State = 0; //npc상태를 idle로
            talkIndex = 0; //대사가 끝나면 인덱스 초기화
            interactManager.TextBox.SetActive(false);
            return; //바로 함수의 진행을 끝냄
        }
        if (isNpc)
        {
            Context.text = talkData.Split(':')[0];
            //Debug.Log(talkData.Split(':')[1]);
            if (talkData.Split(':')[1] == "1")
            {
                npcController.State = 1;
            }
            else if (talkData.Split(':')[1] == "2")
            {
                npcController.State = 2;
            }
            else if (talkData.Split(':')[1] == "9") //다음에 대화할때 바뀐 대사를 말하기위하여 오브젝트 ID를 바꾸기 위해 사용
            {
                npcController.isChange = true;
            }
            else if (talkData.Split(':')[1] == "10")//플레이어 대사일땐 다이얼로그 이름을 플레이어로 바꾸기
            {
                NpcName.text = "[ "+ playerName.Name +" ]";
            }
            else if (talkData.Split(':')[1] == "0")
            {
                npcController.State = 0;
            }
            else if (talkData.Split(':')[1] == "98") // 질병 체크 분기점
            {
                npcController.isChange = true;
                npcController.MedicalCheck();
                talkIndex = -1; // 원활한 대사 출력을 위한 talkIndex 초기화
            }
            else if (talkData.Split(':')[1] == "99") // 질병 치료
            {
                npcController.DoTreatment();
            }

            else if (talkData.Split(':')[1] == "89") // 개 사라짐
            {
                talkIndex = -1;
                npcController.HideDog();
                SaveData saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
                saveData.SetIngQuest_1(true);
                QuestManager qManager = GameObject.Find("QuestManager").GetComponent<QuestManager>();
                qManager.SetIngQuest_1(saveData.GetIngQuest_1());
                qManager.Quest1SetEx();
                megan = GameObject.Find("Megan");
                megan.GetComponent<ObjData>().id = 10001; // 메건 대화창 바뀜
            }
            else if (talkData.Split(':')[1] == "88") // NPC옆으로 개 나옴
            {
                npcController.FindDog();
            }
            else if (talkData.Split(':')[1] == "87") // 개 + NPC 동시에 사라짐
            {
                talkIndex = -1;
                npcController.HideBoth();
                SaveData saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
                saveData.SetIngQuest_1(false);
                saveData.ClearQuest_1();
                QuestManager qManager = GameObject.Find("QuestManager").GetComponent<QuestManager>();
                qManager.SetIngQuest_1(saveData.GetIngQuest_1());
                qManager.SetClearQuest_1(saveData.GetClearQuest_1());
                qManager.Quest1SetEx();
            }
            
            else if (talkData.Split(':')[1] == "101")
            {
                npcController.isChange = true;
            }
            else if (talkData.Split(':')[1] == "102") // 쓰레기장 퀘스트
            {
                npcController.isChange = true;
                SaveData saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
                saveData.SetIngQuest_3(true);
                QuestManager qManager = GameObject.Find("QuestManager").GetComponent<QuestManager>();
                qManager.SetIngQuest_3(saveData.GetIngQuest_3());
            }
            else if (talkData.Split(':')[1] == "103") // 쓰레기장 퀘스트
            {
                talkIndex = -1;
                npcController.isChange = true;
                SaveData saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
                saveData.ClearQuest_3();
                QuestManager qManager = GameObject.Find("QuestManager").GetComponent<QuestManager>();
                qManager.SetClearQuest_3(saveData.GetClearQuest_3());
                Quest3 q3 = GameObject.Find("QuestManager").GetComponent<Quest3>();
                q3.Clear();
            }
            else if (talkData.Split(':')[1] == "104") // 쓰레기장 퀘스트
            {
                npcController.isChange = true;
                SaveData saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
                saveData.SetIngQuest_3(false);
                QuestManager qManager = GameObject.Find("QuestManager").GetComponent<QuestManager>();
                qManager.SetIngQuest_3(saveData.GetIngQuest_3());
            }
            else if (talkData.Split(':')[1] == "111")
            {
                npcController.isChange = true;
                TAManager.StartGame();
            }
            else if (talkData.Split(':')[1] == "112")
            {
                SaveData saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
                saveData.SetIngQuest_2(false);
                QuestManager qManager = GameObject.Find("QuestManager").GetComponent<QuestManager>();
                qManager.SetIngQuest_2(saveData.GetIngQuest_3());
                qManager.Quest2SetEx();
            }
            talkIndex++; //그 다음 대사를 가져오기 위해서 인덱스를 늘려주기
        }
        else//그럴일은 없겠지만 NPC가 아닌 물건과 대화를 하는 경우
        {
            Context.text = talkData;
        }
    }
}
