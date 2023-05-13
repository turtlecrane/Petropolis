using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    // NPC의 상태를 제어하는 스크립트
    public int State; //상태 디버깅 ( 1 = 긍정, 2 = 부정 ) -> TalkManager.cs에서 제어됨.
    public bool isChange; //다음에 대화할때 다른 대사로 바뀌게.
    private SettingsOnEsc settingsOnEsc;
    ObjData ObjData;
    
    private TalkManager talkManager;
    public bool moveStatus;//움직이고 있는 상태의 NPC인지 판단
    private Vector3 curPos;//시작지점 위치 저장
    
    Animator NpcAnimation;
    private bool ChangeID;

    void Start()
    {
        talkManager = GameObject.Find("TalkManager").GetComponent<TalkManager>();
        curPos = transform.position;
        settingsOnEsc = GameObject.Find("Canvas").GetComponent<SettingsOnEsc>();
        isChange = false;
        ChangeID = false;
        NpcAnimation = GetComponent<Animator>();
        ObjData = GetComponent<ObjData>();
    }

    void Update()
    {
        if (!ObjData.isDoctor) // 의사 Npc가 아닐 경우
        {
            Vector3 currentPosition = transform.position; //현재 위치 계속 추적
            float distance = Vector3.Distance(currentPosition, curPos); //시작 지점과 현재 위치의 거리 계산
            if (distance > 0.001f) // 움직임 판단 임계값
            {
                moveStatus = true;
                curPos = currentPosition; //현재 위치를 이전 위치로 저장
            }
            else
            {
                moveStatus = false;
            }

            if (isChange && !settingsOnEsc.isOpen) //대사 구분자가 9였고, 대화가 완료되면
            {
                if (!ChangeID)
                {
                    ObjData.id++; //NPC아이디를 하나 증가시킨다. (다른 존재가 되어버리는것)
                    ChangeID = true; //무한반복 방지
                    isChange = false; //초기화
                }
            }

            if (!isChange)
            {
                ChangeID = false;
            }
        }
        else if (ObjData.isDoctor) // 의사 NPC일 경우
        {
            if (isChange && !settingsOnEsc.isOpen) //대사 구분자가 9였고, 대화가 완료되면
            {
                if (!ChangeID)
                {
                    if (ObjData.id == 2001) { } // 상태가 좋았을 경우 
                    else // 상태가 나빴을 경우
                    {
                        ObjData.id++; //NPC아이디를 하나 증가시킨다. (다른 존재가 되어버리는것)
                    }
                    ChangeID = true; //무한반복 방지
                    isChange = false; //초기화
                }
            }

            if (!isChange)
            {
                ChangeID = false;
            }
        }

        NpcAnimation.SetInteger("Status",State);
    }

    void FixedUpdate()
    {
        if (moveStatus)
        {
            State = 10;
        }
    }

    public void MedicalCheck() // 질병 체크
    {
        TreatManager treatManager = GetComponent<TreatManager>();
        ObjData.id++; //NPC아이디를 하나 증가시킨다.
        treatManager.DiseaseCheck();
    }

    public void DoTreatment() // 질병 치료
    {
        GetComponent<TreatManager>().Treatment();
    }

    public void HideDog() // 개 사라짐
    {
        /*GameObject npcDog = GameObject.Find("NPCdog");
        npcDog.transform.position = Vector3(0,0,-10);*/
        //gameObject.transform.position = new Vector3(-50,0,110); // 안보이는 곳에 숨겨 사라진것처럼 처리
        gameObject.SetActive(false);
    }

    public void FindDog() // 개가 나옴
    {
        ShowDog showDog = GetComponent<ShowDog>();
        showDog.isFind = true; // 개가 NPC옆으로 나오게 하는 함수
    }

    public void HideBoth()
    {
        var dog = GameObject.Find("dog2");
        gameObject.transform.position = new Vector3(-51,0,110); // 안보이는 곳에 숨겨 사라진 것처럼 처리
        dog.transform.position = new Vector3(-52,0,110);        // 안보이는 곳에 숨겨 사라진 것처럼 처리

    }
    
}
