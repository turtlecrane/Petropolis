using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private bool ingQuest_1 = false;
    private bool ingQuest_2 = false;
    private bool ingQuest_3 = false;

    private bool clearQuest_1 = false;
    private bool clearQuest_2 = false;
    private bool failQuest_2 = false;
    private bool clearQuest_3 = false;

    private Quest1 q1;
    private Quest2 q2;
    private Quest3 q3;

    private void Awake()
    {
        q1 = GetComponent<Quest1>();
        q2 = GetComponent<Quest2>();
        q3 = GetComponent<Quest3>();
    }

    public void Quest1SetEx()
    {
        q1.ExclamationOnOff();
    }

    public void Quest2SetEx()
    {
        q2.ExclamationOnOff();
    }

    // Set ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
    public void SetIngQuest_1(bool ing)
    {
        ingQuest_1 = ing;
    }

    public void SetIngQuest_2(bool ing)
    {
        ingQuest_2 = ing;
    }

    public void SetIngQuest_3(bool ing)
    {
        ingQuest_3 = ing;
    }

    public void SetClearQuest_1(bool clear)
    {
        clearQuest_1 = clear;
    }

    public void SetClearQuest_2(bool clear)
    {
        clearQuest_2 = clear;
    }

    public void SetClearQuest_3(bool clear)
    {
        clearQuest_3 = clear;
    }

    public void SetFailQuest_2(bool clear)
    {
        failQuest_2 = clear;
    }

    // ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ

    //Get ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ

    public bool GetClearQuest_1()
    {
        return clearQuest_1;
    }

    public bool GetClearQuest_2()
    {
        return clearQuest_2;
    }

    public bool GetFailQuest_2()
    {
        return failQuest_2;
    }

    public bool GetClearQuest_3()
    {
        return clearQuest_3;
    }

    public bool GetIngQuest_1()
    {
        return ingQuest_1;
    }
    
    public bool GetIngQuest_2()
    {
        return ingQuest_2;
    }
    
    public bool GetIngQuest_3()
    {
        return ingQuest_3;
    }
    // ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
}
