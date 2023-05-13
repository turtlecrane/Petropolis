using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadData : MonoBehaviour
{
    private SaveData saveData;
    private GameObject[] npc;
    private GameObject player;
    private Transform playerPos;
    
    public MiniGameManager mgManager;

    public QuestManager qManager;
    // Start is called before the first frame update

    void Awake()
    {
        saveData = GameObject.Find("SaveData").GetComponent<SaveData>(); // SaveData 불러오기
        SetPlayer();
        playerPos = saveData.GetPlayerPos(); // PlayerPos 불러오기
        player.transform.position = playerPos.position; // player에 이전 transform 적용
        player.transform.rotation = playerPos.rotation;
    }
    void Start()
    {
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
            }
        }
        SetClear();
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
    }
}
