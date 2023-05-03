using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestionAndAnswers : MonoBehaviour
{
    public string Question; // 문제
    public string[] Answers; // 선택지
    public string Solution; // 해설
    public int CorrectAnswer; // 정답의 번호
}
