using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TalkManager : MonoBehaviour
{
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
        NpcName.text = "[ " + scanObject.name + " ]";
        Talk(objData.id, objData.isNpc);
    }

    void Talk(int id, bool isNpc)
    {
        string talkData = dialogManager.GetTalk(id, talkIndex);

        if (talkData == null)//다음 대화가 null 일때
        {
            isTextBox = false;
            talkIndex = 0; //대사가 끝나면 인덱스 초기화
            interactManager.TextBox.SetActive(false);
            return;//바로 함수의 진행을 끝냄
        }
        
        if (isNpc)
        {
            Context.text = talkData;
        }
        else
        {
            Context.text = talkData;
        }
        isTextBox = true;
        talkIndex++;//그 다음 대사를 가져오기 위해서 인덱스를 늘려주기
    }
}
