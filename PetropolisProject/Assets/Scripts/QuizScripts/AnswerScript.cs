using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerScript : MonoBehaviour
{
    public bool isCorrect = false;
    public QuizManager quizManager;

    public void Answer()
    {
        if (isCorrect) // 정답인 버튼을 선택했을 경우
        {
            Debug.Log("Currect Answer");
            quizManager.Correct();
        }
        else // 오답인 버튼을 선택했을 경우
        {
            Debug.Log("Wrong Answer");
            quizManager.Wrong();
        }
    }
}
