using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TalkManager : MonoBehaviour
{
    private NpcController npcController;
    
    public DialogManager dialogManager;
    public InteractManager interactManager;
    public bool isTextBox;
    public TextMeshProUGUI NpcName;
    public TextMeshProUGUI Context;
    public GameObject scanObject;
    public int talkIndex;
    
    public void Action(GameObject scanObj)
    {
        scanObject = scanObj;
        ObjData objData = scanObject.GetComponent<ObjData>();
        npcController = scanObject.GetComponent<NpcController>();
        NpcName.text = "[ " + scanObject.name + " ]";
        Talk(objData.id, objData.isNpc);
    }

    void Talk(int id, bool isNpc)
    {
        string talkData = dialogManager.GetTalk(id, talkIndex);

        if (talkData == null)//다음 대화가 null 일때
        {
            isTextBox = false;
            npcController.State = 0;//npc상태를 idle로
            talkIndex = 0; //대사가 끝나면 인덱스 초기화
            interactManager.TextBox.SetActive(false);
            return;//바로 함수의 진행을 끝냄
        }
        
        if (isNpc)
        {
            Context.text = talkData.Split(':')[0];
            //Debug.Log(talkData.Split(':')[1]);
            if (talkData.Split(':')[1] == "1")//구분자를 나눠서 그 구분자에따라 NPC모션이 결정됨.
            {
                npcController.State = 1;
            }
            else if (talkData.Split(':')[1] == "2")
            {
                npcController.State = 2;
            }
        }
        else
        {
            Context.text = talkData;
        }
        isTextBox = true;
        talkIndex++;//그 다음 대사를 가져오기 위해서 인덱스를 늘려주기
    }
}
