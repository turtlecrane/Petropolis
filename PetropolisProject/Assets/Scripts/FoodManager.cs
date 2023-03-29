using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FoodManager : MonoBehaviour
{
    public GameObject scanObject;
    public GameObject conditions;
    public TextMeshProUGUI content;   // interactbox에 표시되는 텍스트
    public TextMeshProUGUI conditionText;  // Food와 상호작용을 했을 때 UI에 표시되는 텍스트
    public int ConditionTextDelayTime = 3;  // ConditionText가 표시되고 사라지기까지의 시간

    private int foodId;  // Food 객체의 id
    private int foodType = 0; // 0 = GoodFood, 1 = BadFood
    private FoodObjData fObjData;
    private Dictionary<int, string[]> foodData;
    private int foodContentIndex = 0;  // foodData에서 interactbox에 표시되는 텍스트가 저장된 인덱스
    private int foodResultIndex = 1;  // foodData에서 UI에 표시되는 텍스트가 저장된 인덱스

    void Awake()
    {
        foodData = new Dictionary<int, string[]>();//초기화
        FoodTextData();
    }

    void FoodTextData()
    {
        foodData.Add(1000, new string[] { "GoodFood 테스트", "GoodFood를 먹고 상태가 좋아졌다!" });
        foodData.Add(2000, new string[] { "BadFood 테스트", "BadFood를 먹고 상태가 나빠졌다!" });
    }

    public void Action(GameObject scanObj)
    {
        scanObject = scanObj;
        fObjData = scanObject.GetComponent<FoodObjData>();
        foodId = fObjData.foodId;
        foodType = fObjData.GetFoodType();
    }

    public void SetContent() // interactBox의 Text를 설정하는 함수
    {
        content.text = foodData[foodId][foodContentIndex];
    }

    public void SetCondition()  // 플레이어의 상태 아이콘을 표시하는 함수
    {
        if (foodType == 0) // GoodFood
        {
            conditions.transform.GetChild(0).gameObject.SetActive(true);
            conditions.transform.GetChild(1).gameObject.SetActive(false);
        }
        else if (foodType == 1) // BadFood
        {
            conditions.transform.GetChild(0).gameObject.SetActive(false);
            conditions.transform.GetChild(1).gameObject.SetActive(true);
        }
        ConditionTextActive();
        conditionText.text = foodData[foodId][foodResultIndex];
        Invoke("ConditionTextInactive", ConditionTextDelayTime);
    }

    void ConditionTextActive()  // 상호작용시에 UI 텍스트 출력
    {
        conditionText.gameObject.SetActive(true);
    }

    void ConditionTextInactive()  // UI 텍스트 숨김
    {
        conditionText.gameObject.SetActive(false);
    }
}
