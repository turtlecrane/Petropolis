using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//플레이어의 정보를 저장하는 스크립트입니다.
public class PlayerStatus : MonoBehaviour
{
    public string Name; //캐릭터 선택할때 이 변수를 참조해서 이름 할당해주면 될듯?
    
    //현재 플레이어가 위치한 구역을 구분. 1-Hometown, 2-Park, 3-downtown, 4-garbage, 5-alley, 0-그외
    public int AreaID;

    //달리는중인지 아닌지, 풀숲에 있는지 없는지 구분 -> WalkSound.cs에서 참조함
    public int moveStatus; //0-> 정지, 1-> 달리기는중, 2->걷는중
    public bool inGrass;
    
    private PlayerRigidbody playerIsRun;
    private Vector3 curPos;
    
    void Start()
    {
        playerIsRun = GameObject.FindWithTag("Cat").gameObject.GetComponent<PlayerRigidbody>();
        curPos = transform.position;
    }
    void Update()
    {
        Vector3 currentPosition = transform.position; //현재 위치 계속 추적
        float distance = Vector3.Distance(currentPosition, curPos); //시작 지점과 현재 위치의 거리 계산
        if (distance > 0.001f) // 움직임 판단 임계값
        {
            moveStatus = 2;
            curPos = currentPosition; //현재 위치를 이전 위치로 저장
            if (playerIsRun.running == 1)//달리는중이면
            {
                moveStatus = 1; //달리는중이다(끄덕)
            }
        }
        else
        {
            moveStatus = 0;
        }
    }
    
    //구역 판단
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "HomeTown")
        {
            AreaID = 1;
        }
        else if (other.gameObject.name == "Park")
        {
            AreaID = 2;
        }
        else if (other.gameObject.name == "DownTown")
        {
            AreaID = 3;
        }
        else if (other.gameObject.name == "Garbage")
        {
            AreaID = 4;
        }
        else if (other.gameObject.name == "alleyway")
        {
            AreaID = 5;
        }

        if (other.CompareTag("Grass"))
        {
            inGrass = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name == "HomeTown" || other.gameObject.name == "Park" || 
        other.gameObject.name == "DownTown" || other.gameObject.name == "Garbage" ||
        other.gameObject.name == "alleyway")
        {
            AreaID = 0;
        }
        if (other.CompareTag("Grass"))
        {
            inGrass = false;
        }
    }
}
