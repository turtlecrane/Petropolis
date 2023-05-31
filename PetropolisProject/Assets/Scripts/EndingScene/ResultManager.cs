using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    public GameObject cat;
    
    [Header("ClearPanel")]
    public GameObject clearPanel;
    public TextMeshProUGUI clearPanelTitle;
    public TextMeshProUGUI questClearCount;
    public TextMeshProUGUI minigameClearCount;
    public TextMeshProUGUI quizScore;
    
    [Header("FoodPanel")]
    public GameObject foodPanel;
    public TextMeshProUGUI foodPanelTitle;
    public TextMeshProUGUI foodPlayerName;
    public TextMeshProUGUI goodFoodCount;
    public TextMeshProUGUI badFoodCount;
    public TextMeshProUGUI dnfFoodCount;
    
    [Header("EndPanel")]
    public GameObject endPanel;
    public TextMeshProUGUI endPanelTitle;
    [Header("Rank")]
    public Image rankImage;
    public Sprite sRank;
    public Sprite aRank;
    public Sprite bRank;
    public Sprite cRank;
    public Sprite fRank;
    
    
    
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
        clearPanelTitle.text = "<color=blue>" + playerName.Name + "</color>의 클리어 기록";
        questClearCount.text = "총 <color=green>" + saveData.ReturnQuestCount().Length + "개</color>의 퀘스트 중 <color=green>"
                               + questCount + "개</color>를 클리어 했네요!";
        minigameClearCount.text = "미니게임은 총 <color=green>" + saveData.ReturnMinigameCount().Length + "개</color> 중 <color=green>" +
                                  minigameCount + "개</color>를 클리어 했어요!";
        quizScore.text = "퀴즈는 총 <color=green>10문제</color> 중 <color=green>" + saveData.GetQuizScore() + "개</color>를 맞혔군요!";
        //ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
        
        // FoodPanel
        foodPanelTitle.text = "<color=blue>" + playerName.Name + "</color>의 음식 섭취 기록";
        foodPlayerName.text = "<color=blue>" + playerName.Name + "</color>은(는) 오늘 하루";
        goodFoodCount.text = "좋은 음식 <color=green>" + foodCount[0] + "개</color>";
        badFoodCount.text = "건강에 나쁜 음식 <color=purple>" + foodCount[1] + "개</color>";
        dnfFoodCount.text = "위험한 음식 <color=red>" + (foodCount[2]+foodCount[3]) + "개</color>를 먹었습니다!";
        //ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
        
        // EndPanel
        endPanelTitle.text = "<color=blue>" + playerName.Name + "</color>의 랭크";

        if (rankScore < 10)
        {
            //resultRank.text = "F";
            rankImage.sprite = fRank;
        }
        else if (rankScore < 20 && rankScore >= 10)
        {
            //resultRank.text = "C";
            rankImage.sprite = cRank;
        }
        else if (rankScore < 30 && rankScore >= 20)
        {
            //resultRank.text = "B";
            rankImage.sprite = bRank;
        }
        else if (rankScore < 40 && rankScore >= 30)
        {
            //resultRank.text = "A";
            rankImage.sprite = aRank;
        }
        else if (rankScore <= 45 && rankScore >= 40)
        {
            //resultRank.text = "S";
            rankImage.sprite = sRank;
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
