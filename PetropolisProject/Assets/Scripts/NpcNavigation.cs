using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcNavigation : MonoBehaviour
{
    //NPC의 움직임을 담당하는 스크립트입니다.
    //이 스크립트가 오브젝트에 있으면 목적지가 존재하고, 일정한 간격으로 움직이는 NPC임을 말합니다.
    
    public Transform target;
   
    public float moveSpeed = 4.0f; // 이동 속도
    private Vector3 curPos; //시작위치 저장;
    public bool isGoal;//목적지에 도착했는지
    public float stopDuration = 5f;//멈춰있는 시간
    private float stopTimer = 0f; // 멈춰있는 시간을 측정하는 타이머
    private NpcController npcController;
    
    private void Start()
    {
        isGoal = false;
        curPos = gameObject.transform.position;//시작위치 저장
        npcController = GetComponent<NpcController>();
    }

    void Update()
    {
        if (target != null)
        {
            if (!isGoal)
            {
                //현재 위치에서 목표 위치로 이동
                transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, curPos, moveSpeed * Time.deltaTime);
            }
            
            if (transform.position == target.position)
            {
                
                //Debug.Log("목표 지점에 도달했습니다.");
                transform.rotation = target.rotation;
                npcController.State = 0;
                stopTimer += Time.deltaTime;
                if (stopTimer >= stopDuration)
                {
                    isGoal = true;
                    //transform.Rotate(0f, 180f, 0f);
                    stopTimer = 0f;
                }
            }
            else if (transform.position == curPos)
            {
                //Debug.Log("다시 돌아간다요");
                npcController.State = 0;
                stopTimer += Time.deltaTime;
                if (stopTimer >= stopDuration)
                {
                    isGoal = false;
                    transform.Rotate(0f, 180f, 0f);
                    stopTimer = 0f;
                }
            }
        }
    }
}
