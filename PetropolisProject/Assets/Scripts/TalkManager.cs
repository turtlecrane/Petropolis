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

    public TextMeshProUGUI NpcName;
    public TextMeshProUGUI Context;
    public GameObject scanObject;

    public int talkIndex;

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
                NpcName.text = "[ 나 ]";
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
                npcController.HideDog();
            }
            else if (talkData.Split(':')[1] == "88") // NPC옆으로 개 나옴
            {
                npcController.FindDog();
            }
            else if (talkData.Split(':')[1] == "87") // 개 + NPC 동시에 사라짐
            {
                npcController.HideBoth();
            }
            
            talkIndex++; //그 다음 대사를 가져오기 위해서 인덱스를 늘려주기
        }
        else//그럴일은 없겠지만 NPC가 아닌 물건과 대화를 하는 경우
        {
            Context.text = talkData;
        }
    }
}
