using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultManager : MonoBehaviour
{
    public GameObject cat;
    public GameObject clearPanel;
    public TextMeshProUGUI clearPanelTitle;
    public TextMeshProUGUI questClearCount;
    public TextMeshProUGUI minigameClearCount;
    public TextMeshProUGUI quizScore;
    public GameObject foodPanel;
    public TextMeshProUGUI foodPanelTitle;
    public TextMeshProUGUI goodFoodCount;
    public TextMeshProUGUI badFoodCount;
    public TextMeshProUGUI dangerFoodCount;
    public TextMeshProUGUI fatalFoodCount;
    public GameObject endPanel;
    
    public TextMeshProUGUI endPanelTitle;
    public TextMeshProUGUI resultRank;
    
    private Animator ani;
    private SaveData saveData;
    private PlayerName playerName;

    private int questCount = 0;

    private int minigameCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        clearPanel.SetActive(true);
        foodPanel.SetActive(false);
        endPanel.SetActive(false);
        saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
        playerName = GameObject.Find("PlayerName").GetComponent<PlayerName>();
        ani = cat.GetComponent<Animator>();
        ani.SetTrigger("isDeath");
        
        bool[] quests = saveData.ReturnQuestCount();
        bool[] minigames = saveData.ReturnMinigameCount();
        Count(quests, ref questCount);
        Count(minigames, ref minigameCount);

        SetTexts();
    }

    private void Count(bool[] games, ref int count)
    {
        for (int i = 0; i < games.Length; i++)
        {
            if (games[i])
            {
                count++;
            }
        }
    }

    private void SetTexts()
    {
        int[] foodCount = saveData.ReturnFoodCount();

        int rankScore = questCount + minigameCount + saveData.GetQuizScore() * 2
            + foodCount[0] * 2 - foodCount[1] - foodCount[2] * 2 - foodCount[3] * 3;
        
        // ClearPanel
        clearPanelTitle.text = playerName.Name + "의 클리어 기록";
        questClearCount.text = "퀘스트 클리어(+1) : " + questCount + " / 3";
        minigameClearCount.text = "미니게임 클리어(+1) : " + minigameCount + " / 4";
        quizScore.text = "퀴즈 점수(+2) : " + saveData.GetQuizScore() + " / 10";
        //ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
        
        // FoodPanel
        foodPanelTitle.text = playerName.Name + "의 음식 섭취 기록";
        goodFoodCount.text = "Good(+2) : " + foodCount[0] + " / 9";
        badFoodCount.text = "Bad(-1) : " + foodCount[1] + " / 8";
        dangerFoodCount.text = "Danger(-2) : " + foodCount[2] + " / 4";
        fatalFoodCount.text = "Fatal(-3) : " + foodCount[3] + " / 7";
        //ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
        
        // EndPanel
        endPanelTitle.text = playerName.Name + "의 랭크";

        if (rankScore < 10)
        {
            resultRank.text = "F";
        }
        else if (rankScore < 20 && rankScore >= 10)
        {
            resultRank.text = "C";
        }
        else if (rankScore < 30 && rankScore >= 20)
        {
            resultRank.text = "B";
        }
        else if (rankScore < 40 && rankScore >= 30)
        {
            resultRank.text = "A";
        }
        else if (rankScore <= 45 && rankScore >= 40)
        {
            resultRank.text = "S";
        }
        //ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
    }

    public void CtoF()
    {
        foodPanel.SetActive(true);
        clearPanel.SetActive(false);
    }
    
    public void FtoE()
    {
        endPanel.SetActive(true);
        foodPanel.SetActive(false);
    }
}
