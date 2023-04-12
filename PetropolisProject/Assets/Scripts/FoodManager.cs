using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
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

    public float maxFever = 300.0f; // Fever 상태의 최대 체력
    public float maxDisease = 250.0f; // Disease 상태의 최대 체력
    public float maxFatal = 200.0f; // Fatal 상태의 최대 체력
    public float maxHungry = 500.0f; // 기본 최대 체력, 배고픔 정도
    public float satiety = 100.0f; // 포만감

    private RawImage feverImage; // fever 아이콘
    private RawImage diseaseImage; // disease 아이콘
    private RawImage fatalImage; // fatal 아이콘
    private GameObject hungrys; // 배부름, 보통, 배고픔, 매우 배고픔 아이콘
    
    private int foodId;  // Food 객체의 id
    private int foodType = 0; // 0 = GoodFood, 1 = BadFood
    private FoodObjData fObjData;
    private Dictionary<int, string[]> foodData;
    private int foodContentIndex = 0;  // foodData에서 interactbox에 표시되는 텍스트가 저장된 인덱스
    private int foodResultIndex = 1;  // foodData에서 UI에 표시되는 텍스트가 저장된 인덱스

    private float reduce = -1.0f; // 감소
    private float fever; // 고열 상태 체력
    private float disease; // 감염 상태 체력
    private float hungry; // 기본 체력
    private float fatal; // 치명적 상태 체력
    private bool onFever = false; // 고열 상태 확인
    private bool onDisease = false; // 감염 상태 확인
    private bool onFatal = false; // 치명적 상태 확인

    private float feverConditionColor; // fever 아이콘의 G, B 색상값 감소량
    private float diseaseConditionColor; // disease 아이콘의 G, B 색상값 감소량
    private float fatalConditionColor; // fatal 아이콘의 G, B 색상값 감소량
    void Awake()
    {
        foodData = new Dictionary<int, string[]>();//초기화
        FoodTextData();
    }

    private void Start()
    {
        fever = maxFever;
        disease = maxDisease;
        fatal = maxFatal;
        hungry = maxHungry;
        
        feverConditionColor = 1.0f / maxFever;
        diseaseConditionColor = 1.0f / maxDisease;
        fatalConditionColor = 1.0f / maxFatal;
        
        feverImage = conditions.transform.GetChild(1).gameObject.GetComponent<RawImage>();
        diseaseImage = conditions.transform.GetChild(2).gameObject.GetComponent<RawImage>();
        fatalImage = conditions.transform.GetChild(3).gameObject.GetComponent<RawImage>();

        hungrys = conditions.transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        hungry += reduce * Time.deltaTime; // 게임 시작부터 지속적으로 감소
        // hungry += reduce * 20.0f * Time.deltaTime;
        //Debug.Log(hungry);
        
        if (onFever) // 고열 상태
        {
            fever += reduce * Time.deltaTime;
            IncreasingRed(feverImage, feverConditionColor);
        }

        if (onDisease) // 감염 상태
        {
            disease += reduce * Time.deltaTime;
            IncreasingRed(diseaseImage, diseaseConditionColor);
        }
        
        if (onFatal) // 치명적인 상태
        {
            fatal += reduce * Time.deltaTime;
            IncreasingRed(fatalImage, fatalConditionColor);
        }

        if (hungry >= 400.0f) // 배부름 상태
        {
            if (hungry > maxHungry)
                hungry = maxHungry;
            SetActiveHungry(0);
        }
        else if (hungry >= 250.0f && hungry < 400.0f) // 보통 상태
        {
            SetActiveHungry(1);
        }
        else if (hungry >= 100.0f && hungry < 250.0f) // 배고픔 상태
        {
            SetActiveHungry(2);
        }
        else if (hungry >= 0.0f && hungry < 100.0f) // 매우 배고픔 상태
        {
            SetActiveHungry(3);
        }
        else if (hungry < 0.0f)
        {
            hungry = 0.0f;
        }
    }

    void FoodTextData()
    {
        foodData.Add(1000, new string[] { "GoodFood 테스트", "GoodFood를 먹고 포만감을 얻었다!" });
        foodData.Add(2000, new string[] { "FeverFood 테스트", "FeverFood를 먹고 고열이 발생했다!" });
        foodData.Add(3000, new string[] { "DiseaseFood 테스트", "DiseaseFood를 먹고 질병에 감염되었다!" });
        foodData.Add(4000, new string[] { "FatalFood 테스트", "FatalFood를 먹고 치명적인 상황이 되었다! "});
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
            hungry += satiety;
        }
        else if (foodType == 1) // FeverFood
        {
            SetActiveCondition(foodType);
            onFever = true;
        }
        else if (foodType == 2) // DiseaseFood
        {
            SetActiveCondition(foodType);
            onDisease = true;
        }
        else if (foodType == 3) // FatalFood
        {
            SetActiveCondition(foodType);
            onFatal = true;
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

    
    public void SetFever(float setFever) // 병원의 치료 이벤트를 위한 함수
    {
        fever = setFever;
    }
    
    public void SetDisease(float setDisease) // 병원의 치료 이벤트를 위한 함수
    {
        disease = setDisease;
    }

    public void SetOnFever(bool setOnFever) // 병원의 치료 이벤트를 위한 함수
    {
        onFever = setOnFever;
    }

    public void SetOnDisease(bool setOnDisease) // 병원의 치료 이벤트를 위한 함수
    {
        onDisease = setOnDisease;
    }

    private void IncreasingRed(RawImage rawImage, float conditionColor) // 아이콘의 색상을 점점 붉게 바꿔주는 함수
    {
        Color color = rawImage.color;
        color.g += reduce * conditionColor * Time.deltaTime;
        color.b += reduce * conditionColor * Time.deltaTime;
        if (color.g < 0)
        {
            color.g = 0.0f;
        }

        if (color.b < 0)
        {
            color.b = 0.0f;
        }
        rawImage.color = color;
    }

    private void SetActiveHungry(int index) // Hungry 값에 따라 아이콘을 변경해주는 함수
    {
        if (!hungrys.transform.GetChild(index).gameObject.activeSelf) // 이미 Active 상태일 경우 연산하지 않음
        {
            hungrys.transform.GetChild(index).gameObject.SetActive(true);
            for (int i = 0; i < 4; i++)
            {
                if (i == index)
                {
                    continue;
                }
                else
                {
                    hungrys.transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }
    }

    private void SetActiveCondition(int index)
    {
        conditions.transform.GetChild(index).gameObject.SetActive(true);
    }

    public void SetInactiveCondition(int index) // 병원의 치료 이벤트를 위한 함수
    {
        conditions.transform.GetChild(index).gameObject.SetActive(false);
    }
}
