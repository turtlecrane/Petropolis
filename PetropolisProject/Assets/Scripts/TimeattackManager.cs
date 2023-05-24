using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeattackManager : MonoBehaviour
{
    public GameObject gameObject;
    public float gameTime = 60f; // 게임 시간 (초)
    public TextMeshProUGUI timeText; // UI에 표시될 시간 텍스트

    private float timer; // 타이머
    public bool isGameRunning; // 게임 실행 여부
    public bool isCorrect;

    public ObjData ObjData;
    public NpcController npcController;
    private SaveData saveData;
    private QuestManager qManager;

    void Start()
    {
        saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
        qManager = GameObject.Find("QuestManager").GetComponent<QuestManager>();
        isGameRunning = false;
        isCorrect = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameRunning)
        {
            // 타이머 감소
            gameObject.SetActive(true);
            timer -= Time.deltaTime;

            UpdateUITime();

            // 시간초과시 종료
            if (timer <= 0f)
            {
                qManager.SetIngQuest_2(false);
                saveData.SetIngQuest_2(false);
                saveData.FailQuest_2();
                qManager.SetFailQuest_2(true);
                qManager.Quest2SetEx();
                EndGame();
                ObjData.id = 15002;
            }
        }
    }
    //시작
    public void StartGame()
    {
        isGameRunning = true;
        qManager.SetIngQuest_2(true);
        saveData.SetIngQuest_2(true);
        qManager.Quest2SetEx();
        timer = gameTime;
        UpdateUITime();
    }
    //종료
    public void EndGame()
    {
        isGameRunning = false;
        gameObject.SetActive(false);
    }

    public void InteractTarget()
    {
        if(isGameRunning == true){
            isCorrect = true;
            qManager.SetClearQuest_2(true);
            saveData.ClearQuest_2();
            qManager.Quest2SetEx();
            ObjData.id = 15003;
            EndGame();
        }
    }

    //남은 시간 표시
    private void UpdateUITime()
    {
        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);
        string timeString = string.Format("{0:00}:{1:00}", minutes, seconds);
        timeText.text = timeString;
    }
}