using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    public List<QuestionAndAnswers> QnA; // 문제리스트

    public GameObject[] options; // 선택지 버튼

    public int currentQuestion; // 현재 문제
    public GameObject StartPanel; // 첫 화면
    public GameObject QuizPanel; // 퀴즈 화면
    public GameObject GoPanel; // 종료 화면
    public GameObject btnNext; // 다음 버튼
    public GameObject QuizNPC;
    public GameObject npcTextbg;
    public TextMeshPro npcText;
    public TextMeshProUGUI QuestionText; // 문제 텍스트
    public TextMeshProUGUI SolutionText; // 해설 텍스트
    public TextMeshProUGUI ScoreText; // 점수 표기 텍스트

    private int totalQuestions = 0; // 총 문제 개수
    private int answerCount = 0;
    private bool isSelect = false;
    private Animator npcAnimator;
    private SaveData saveData;

    public int score; // 점수
    // Start is called before the first frame update
    void Start()
    {
        saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
        npcTextbg.SetActive(false);
        npcAnimator = QuizNPC.GetComponent<Animator>();
        totalQuestions = QnA.Count;
        StartPanel.SetActive(true);
        QuizPanel.SetActive(false);
        GoPanel.SetActive(false);
    }

    public void QuizStart() // 퀴즈 시작
    {
        StartPanel.SetActive(false);
        GenerateQuestion();
        QuizPanel.SetActive(true);
    }
    
    //public void retry()
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //}

    void GameOver() // 퀴즈 종료
    {
        QuizPanel.SetActive(false);
        GoPanel.SetActive(true);
        npcTextbg.SetActive(true);
        if (score < 8)
        {
            npcText.text = "흥.";
        }
        else
        {
            npcText.text = "굉장한데?";
        }
        
        ScoreText.text = score + "/" + totalQuestions;
    }

    public void Correct() // 정답
    {
        if (!isSelect)
        {
            score += 1;
            isSelect = true;
            npcTextbg.SetActive(true);
            npcText.text = "정답이야!";
            QnA.RemoveAt(currentQuestion); // 현재 진행된 문제를 리스트에서 제거
            npcAnimator.SetInteger("Status", 1);
            npcAnimator.SetTrigger("Trigger");
            SetSolution();
        }
    }

    public void Wrong() // 오답
    {
        if (!isSelect)
        {
            isSelect = true;
            npcTextbg.SetActive(true);
            npcText.text = "오답이네.";
            QnA.RemoveAt(currentQuestion); // 현재 진행된 문제를 리스트에서 제거
            npcAnimator.SetInteger("Status", 2);
            npcAnimator.SetTrigger("Trigger");
            SetSolution();
        }
    }

    void SetAnswers() // 선택지 버튼의 Text 오브젝트에 선택지들을 작성
    {
        answerCount = QnA[currentQuestion].Answers.Length;
        if (answerCount == 2)
        {
            options[2].SetActive(false);
        }
        else
        {
            options[2].SetActive(true);
        }
        for (int i = 0; i < answerCount; i++)
        {
            options[i].transform.GetChild(1).gameObject.SetActive(false); // 직전 문제의 O, X 끄기
            options[i].transform.GetChild(2).gameObject.SetActive(false);
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = QnA[currentQuestion].Answers[i];

            if (QnA[currentQuestion].CorrectAnswer == i + 1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true; // 정답 버튼 설정
            }
        }

    }

    void GenerateQuestion() // 문제 설정
    {
        SolutionText.gameObject.SetActive(false); // 해설 텍스트 끄기
        if (QnA.Count > 0) // QnA 리스트에 하나라도 남아 있으면
        {
            currentQuestion = Random.Range(0, QnA.Count); // 랜덤으로 1문제 택
            QuestionText.text = QnA[currentQuestion].Question; // 문제 텍스트 설정
            SolutionText.text = QnA[currentQuestion].Solution; // 해설 텍스트 설정
            SetAnswers(); // 선택지 설정
        }
        else
        {
            Debug.Log("Out of Questions");
            GameOver(); // 퀴즈 종료
        }
    }

    public void NextQuestion() // 다음 문제
    {
        isSelect = false;
        npcTextbg.SetActive(false);
        npcAnimator.SetInteger("Status", 0);
        GenerateQuestion();
        btnNext.SetActive(false);
    }

    void SetSolution() // 해설 표기
    {
        SetCorrectWrong();
        SolutionText.gameObject.SetActive(true);
        btnNext.SetActive(true);
    }

    void SetCorrectWrong() // 정답, 오답에 따라 버튼 위에 O, X 표기 GetChild(1) = O, GetChild(2) = X
    {
        for (int i = 0; i < answerCount; i++)
        {
            if (options[i].GetComponent<AnswerScript>().isCorrect == true)
            {
                options[i].transform.GetChild(1).gameObject.SetActive(true);
            }
            else
            {
                options[i].transform.GetChild(2).gameObject.SetActive(true);
            }
        }
    }

    public void SetQuiz()
    {
        saveData.ClearQuiz();
        saveData.SetQuizScore(score);
    }
}
