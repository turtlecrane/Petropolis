using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjData : MonoBehaviour
{
    public String name;
    public int id;
    public bool isNpc;//npc인지 아닌지 구분
    public bool isDoctor;
    private int beforeId; // 변경되기 전의 Id
    private SaveData saveData;

    void Start()
    {
        beforeId = id;
        saveData = GameObject.Find("SaveData").gameObject.GetComponent<SaveData>();
    }

    void Update()
    {
        if (isNpc) // Npc의 경우 Id 변경 여부를 체크
        {
            CheckId();
        }
    }

    void CheckId()
    {
        if (beforeId != id) // 만약 Id가 변경되었다면
        {
            saveData.SearchAndSaveChangedId(gameObject); // SaveData에 저장
        }
        beforeId = id;
    }
}
