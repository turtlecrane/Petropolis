using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    private bool clearRoadGame_1 = false;
    private bool clearRoadGame_2 = false;
    private bool clearRoadGame_3 = false;
    private bool passRoadGame_1 = false;
    private bool passRoadGame_2 = false;
    private bool passRoadGame_3 = false;
    private bool clearQuiz = false;
    private bool clearFrisbee = false;
    private bool clearTimeAttack = false;

    private int quizScore = 0;

    public GameObject quizNpc;
    public Transform cam;
    public Transform vCamFreeLook;
    

    // Set ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
    public void SetClearRoadGame_1(bool clear)
    {
        clearRoadGame_1 = clear;
    }
    
    public void SetClearRoadGame_2(bool clear)
    {
        clearRoadGame_2 = clear;
    }
    
    public void SetClearRoadGame_3(bool clear)
    {
        clearRoadGame_3 = clear;
    }
    
    public void SetPassRoadGame_1(bool pass)
    {
        passRoadGame_1 = pass;
    }
    
    public void SetPassRoadGame_2(bool pass)
    {
        passRoadGame_2 = pass;
    }
    
    public void SetPassRoadGame_3(bool pass)
    {
        passRoadGame_3 = pass;
    }
    
    public void SetClearQuiz(bool clear)
    {
        clearQuiz = clear;
    }

    public void SetQuizScore(int score)
    {
        quizScore = score;
    }
    
    public void SetClearFrisbee(bool clear)
    {
        clearFrisbee = clear;
    }

    public void SetClearTimeAttack(bool clear)
    {
        clearTimeAttack = clear;
    }
    // ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
    
    //Get ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
    public bool GetClearRoadGame_1()
    {
        return clearRoadGame_1;
    }
    
    public bool GetClearRoadGame_2()
    {
        return clearRoadGame_2;
    }
    
    public bool GetClearRoadGame_3()
    {
        return clearRoadGame_3;
    }
    
    public bool GetPassRoadGame_1()
    {
        return passRoadGame_1;
    }
    
    public bool GetPassRoadGame_2()
    {
        return passRoadGame_2;
    }
    
    public bool GetPassRoadGame_3()
    {
        return passRoadGame_3;
    }

    public bool GetClearQuiz()
    {
        return clearQuiz;
    }

    public int GetQuizScore()
    {
        return quizScore;
    }
    
    public bool GetClearFrisbee()
    {
        return clearFrisbee;
    }
    
    public bool GetClearTimeAttack()
    {
        return clearTimeAttack;
    }
    // ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ

    public void SetQuizNpcId()
    {
        SaveData saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
        if (quizScore < 8)
        {
            quizNpc.GetComponent<ObjData>().id = 14002;
            saveData.SearchAndSaveChangedId(quizNpc);
        }
        else
        {
            quizNpc.GetComponent<ObjData>().id = 14003;
            saveData.SearchAndSaveChangedId(quizNpc);
        }
    }
    
}
