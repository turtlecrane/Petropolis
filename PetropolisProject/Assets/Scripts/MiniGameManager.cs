using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    private bool clearRoadGame_1 = false;
    private bool clearRoadGame_2 = false;
    private bool clearRoadGame_3 = false;
    private bool clearQuiz = false;
    private bool clearFrisbee = false;
    private bool clearTimeAttack = false;


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
    
    public void SetClearQuiz(bool clear)
    {
        clearQuiz = clear;
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

    public bool GetClearQuiz()
    {
        return clearQuiz;
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
}
