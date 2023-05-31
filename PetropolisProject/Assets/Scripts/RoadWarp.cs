using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadWarp : MonoBehaviour
{
    public MiniGameManager mgManager;
    public int portalNum = 0;
    public GameObject nextPortal;

    private void OnTriggerEnter(Collider other) // 충돌한 순간
    {
        if (other.gameObject.tag == "Cat" || other.gameObject.tag == "Dog") // 플레이어 구분
        {
            Vector3 camOffset = other.gameObject.transform.position - Camera.main.transform.position;
            if (portalNum == 1)
            {
                if (mgManager.GetPassRoadGame_1()) // 클리어 했을 경우
                {
                    Warp(other.transform, camOffset);
                }
                else // 클리어 하지 않았을 경우
                {
                    GetComponent<GotoScene>().SceneChange(); // 미니게임 로드
                    SaveData saveData = GameObject.Find("SaveData").GetComponent<SaveData>(); 
                    // saveData에서 플레이어 위치 변경
                    saveData.SetPlayerPos(nextPortal.transform);
                }
            }
            else if (portalNum == 2)
            {
                if (mgManager.GetPassRoadGame_2())
                {
                    Warp(other.transform, camOffset);
                }
                else
                {
                    GetComponent<GotoScene>().SceneChange();
                    SaveData saveData = GameObject.Find("SaveData").GetComponent<SaveData>(); 
                    // saveData에서 플레이어 위치 변경
                    saveData.SetPlayerPos(nextPortal.transform);
                }
            }
            else if (portalNum == 3)
            {
                if (mgManager.GetPassRoadGame_3())
                {
                    Warp(other.transform, camOffset);
                }
                else
                {
                    GetComponent<GotoScene>().SceneChange();
                    SaveData saveData = GameObject.Find("SaveData").GetComponent<SaveData>(); 
                    // saveData에서 플레이어 위치 변경
                    saveData.SetPlayerPos(nextPortal.transform);
                }
            }
            else if (portalNum == 4)
            {
                GetComponent<ResetDoctorId>().Reset();
                Warp(other.transform, camOffset);
            }
            else
            {
                Warp(other.transform, camOffset);
            }
        }
    }

    private void Warp(Transform other, Vector3 camOffset)
    {
        other.position = nextPortal.transform.position; // 다음 위치(NextPortal)로 이동
        mgManager.Warp(other, camOffset);
    }
}
