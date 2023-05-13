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
            if (portalNum == 1)
            {
                if (mgManager.GetClearRoadGame_1()) // 클리어 했을 경우
                {
                    other.gameObject.transform.position = nextPortal.transform.position; // 다음 위치(NextPortal)로 이동
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
                if (mgManager.GetClearRoadGame_2())
                {
                    other.gameObject.transform.position = nextPortal.transform.position;
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
                if (mgManager.GetClearRoadGame_3())
                {
                    other.gameObject.transform.position = nextPortal.transform.position;
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
                other.gameObject.transform.position = nextPortal.transform.position;
            }
            else
            {
                other.gameObject.transform.position = nextPortal.transform.position;
            }
        }
    }
}
