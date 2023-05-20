using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;

public class FoodManager : MonoBehaviour
{
    public PlayerRigidbody player; // Death 애니메이션을 호출하기 위한 PlayerRigidBody
    public GameObject scanObject;
    public GameObject conditions;
    public GameObject gameOver;
    public TextMeshProUGUI content;   // interactbox에 표시되는 텍스트
    public TextMeshProUGUI conditionText;  // Food와 상호작용을 했을 때 UI에 표시되는 텍스트
    public int ConditionTextDelayTime = 3;  // ConditionText가 표시되고 사라지기까지의 시간

    public float bad = 0.0f; // 고열 상태 체력
    public float danger = 75.0f;
    public float fatal = 150.0f; // 치명적 상태 체력
    public float maxDisease = 300.0f; // Disease 상태의 최대 체력
    public float maxHungry = 500.0f; // 기본 최대 체력, 배고픔 정도
    public float satiety = 100.0f; // 포만감

    private RawImage diseaseImage; // disease 아이콘
    private int diseaseImageIndex = 1; // disease 아이콘의 Child 인덱스
    private GameObject hungrys; // 배부름, 보통, 배고픔, 매우 배고픔 아이콘
    private SaveData saveData;
    
    private int foodId;  // Food 객체의 id
    private int foodType = 0; // 0 = GoodFood, 1 = BadFood
    private FoodObjData fObjData;
    private Dictionary<int, string[]> foodData;
    private int foodContentIndex = 0;  // foodData에서 interactbox에 표시되는 텍스트가 저장된 인덱스
    private int foodResultIndex = 1;  // foodData에서 UI에 표시되는 텍스트가 저장된 인덱스

    private float reduce = -1.0f; // 감소
    private float disease; // 감염 상태 체력
    private float hungry; // 기본 체력
    private bool onDisease = false; // 감염 상태 확인
    private bool isDeath = false; // 플레이어 게임 오버

    private float diseaseConditionColor; // disease 아이콘의 G, B 색상값 감소량

    private GameObject d_image;
    private Vector3 diseaseImagePos;
    private float diseaseImagePosMax = 2.0f; // 좌(우)로 이동가능한 (x)최대값
    private float diseaseImagePosSpeed = 50.0f; // 이동속도
    void Awake()
    {
        foodData = new Dictionary<int, string[]>();//초기화
        FoodTextData();
    }

    private void Start()
    {
        saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
        if (!onDisease)
        {
            disease = maxDisease;
        }
        diseaseConditionColor = 1.0f / maxDisease;
        d_image = conditions.transform.GetChild(1).gameObject;
        diseaseImage = d_image.GetComponent<RawImage>();
        diseaseImagePos = d_image.transform.position;
        if (onDisease)
        {
            d_image.SetActive(true);
            diseaseImage.color = saveData.GetSaveDiseaseStateColor();
        }
        hungrys = conditions.transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        if (!isDeath) // 플레이어가 생존 중이면
        {
            hungry += reduce * Time.deltaTime; // 게임 시작부터 지속적으로 감소
            // hungry += reduce * 20.0f * Time.deltaTime;
            //Debug.Log(hungry);
            saveData.SaveHungry(hungry);
            if (hungry < 0.0f || disease < 0.0f)
            {
                player.PlayerDeath();
                Invoke("SetActiveGameOver", 3.0f);
                isDeath = true;
            }

            if (onDisease) // 감염 상태
            {
                Vector3 v = diseaseImagePos;
                disease += reduce * Time.deltaTime;
                saveData.SaveDisease(disease);
                if (disease < 0.0f)
                {
                    disease = 0.0f;
                    d_image.transform.position = diseaseImagePos;
                }
                else if (disease >= 0.0f && disease < 150.0f)
                {
                    v.x += diseaseImagePosMax * Mathf.Sin(Time.time * diseaseImagePosSpeed);
                    d_image.transform.position = v;
                }

                IncreasingRed(diseaseImage, diseaseConditionColor);
            }

            if (hungry >= 400.0f) // 배부름 상태
            {
                if (hungry > maxHungry)
                {
                    hungry = maxHungry;
                    saveData.SaveHungry(hungry);
                }

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
                saveData.SaveHungry(hungry);
            }
        }
    }

    void FoodTextData()
    {
        //GoodFood
        foodData.Add(1000, new string[] { "GoodFood 테스트", "GoodFood를 먹고 포만감을 얻었다!" });
        
        foodData.Add(1001, new string[] { "맛있어보이는 삶은 고기다. 먹을까?", "삶은 고기를 먹고 포만감을 얻었다!" });
        foodData.Add(1002, new string[] { "맛있어보이는 고구마다. 먹을까?", "고구마를 먹고 포만감을 얻었다!" });
        foodData.Add(1003, new string[] { "맛있어보이는 수박이다. 먹을까?", "수박을 먹고 수분과 비타민을 보충했다!" });
        foodData.Add(1004, new string[] { "맛있어보이는 삶은 완두콩이다. 먹을까?", "삶은 완두콩을 먹고 영양분을 얻었다!" });
        foodData.Add(1005, new string[] { "맛있어보이는 호박이다. 먹을까?", "호박을 먹고 비타민, 섬유질, 칼슘을 보충했다!" });
        foodData.Add(1006, new string[] { "맛있어보이는 고양이용 연어통조림이다. 먹을까?", "연어통조림을 먹고 포만감을 얻었다!" });
        foodData.Add(1007, new string[] { "맛있어보이는 고양이용 참치통조림이다. 먹을까?", "참치통조림을 먹고 포만감을 얻었다!" });
        foodData.Add(1008, new string[] { "맛있어보이는 요거트다. 먹을까?", "요거트를 먹고 건강이 좋아졌다!" });
        foodData.Add(1009, new string[] { "맛있어보이는 버섯이다. 먹을까?", "버섯을 먹고 포만감을 얻었다!" });
        //ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
        
        //BadFood
        foodData.Add(2000, new string[] { "BadFood 테스트", "BadFood를 먹고 고열이 발생했다!" });
        
        
        foodData.Add(2001, new string[] { "맛있어보이는 아이스크림이다. 먹을까?", "아이스크림을 먹고 배탈이 나버렸다! 바로 상점가의 병원으로 가자." });
        foodData.Add(2002, new string[] { "맛있어보이는 소시지다. 먹을까?", "소시지를 먹고 비만이 발생했다! 바로 상점가의 병원으로 가자." });
        foodData.Add(2003, new string[] { "맛있어보이는 떡이다. 먹을까?", "떡을 먹다 목에 걸려버렸다! 바로 상점가의 병원으로 가자." });
        foodData.Add(2004, new string[] { "맛있어보이는 아보카도다. 먹을까?", "아보카도를 먹고 퍼신 성분에 중독되었다! 바로 상점가의 병원으로 가자." });
        foodData.Add(2005, new string[] { "맛있어보이는 치즈다. 먹을까?", "치즈를 먹고 배탈이 나버렸다! 바로 상점가의 병원으로 가자." });
        foodData.Add(2006, new string[] { "맛있어보이는 빵이다. 먹을까?", "빵을 먹고 비만이 발생했다! 바로 상점가의 병원으로 가자." });
        foodData.Add(2007, new string[] { "맛있어보이는 감자칩이다. 먹을까?", "감자칩을 먹고 배탈이 나버렸다! 바로 상점가의 병원으로 가자." });
        foodData.Add(2008, new string[] { "맛있어보이는 감자튀김이다. 먹을까?", "감자튀김을 먹고 비만이 발생했다! 바로 상점가의 병원으로 가자." });
        //ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
        
        //DangerFood
        foodData.Add(3000, new string[] { "DangerFood 테스트", "DangerFood를 먹고 질병에 감염되었다!" });
        
        foodData.Add(3001, new string[] { "통조림이 열려있다. 먹을까?", "부패된 통조림을 먹고 식중독이 발생했다! 즉시 병원에 가자." });
        foodData.Add(3002, new string[] { "맛있어보이는 뼈 있는 닭고기다. 먹을까?", "닭뼈가 뾰족하게 부서져 목을 찌르고 있다! 즉시 병원에 가자."});
        foodData.Add(3003, new string[] { "맛있어보이는 생고기다. 먹을까?", "생고기를 먹고 톡소플라즈마에 감염되어버렸다! 즉시 병원에 가자." });
        foodData.Add(3004, new string[] { "맛있어보이는 생새우다. 먹을까?", "생새우를 먹고 티아민 결핍이 발생했다! 즉시 병원에 가자." });
        //ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
        
        //FatalFood
        foodData.Add(4000, new string[] { "FatalFood 테스트", "FatalFood를 먹고 치명적인 상황이 되었다! "});
        
        foodData.Add(4001, new string[] { "맛있어보이는 초콜릿이다. 먹을까?", "초콜릿을 먹고 치명적인 테오브로민 중독에 걸려버렸다! 한시라도 빨리 병원에 가야해!"});
        foodData.Add(4002, new string[] { "맛있어보이는 와인이다. 먹을까?", "초콜릿을 먹고 치명적인 알코올 중독에 걸려버렸다! 한시라도 빨리 병원에 가야해!"});
        foodData.Add(4003, new string[] { "맛있어보이는 포도다. 먹을까?", "초콜릿을 먹고 치명적인 급성 신부전에 걸려버렸다! 한시라도 빨리 병원에 가야해!"});
        foodData.Add(4004, new string[] { "맛있어보이는 마늘이다. 먹을까?", "초콜릿을 먹고 치명적인 용혈성 빈혈에 걸려버렸다! 한시라도 빨리 병원에 가야해!"});
        foodData.Add(4005, new string[] { "맛있어보이는 양파다. 먹을까?", "초콜릿을 먹고 치명적인 용혈성 빈혈에 걸려버렸다! 한시라도 빨리 병원에 가야해!"});
        foodData.Add(4006, new string[] { "맛있어보이는 만두다. 먹을까?", "만두 속의 파, 양파때문에 치명적인 용혈성 빈혈에 걸려버렸다! 한시라도 빨리 병원에 가야해!"});
        foodData.Add(4007, new string[] { "맛있어보이는 피자다. 먹을까?", "피자 속의 양파와 마늘때문에 치명적인 용혈성 빈혈에 걸려버렸다! 한시라도 빨리 병원에 가야해!" });
        //ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
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
            saveData.SaveHungry(hungry);
        }
        else // BadFood, DangerFood, FatalFood
        {
            SetActiveCondition(foodType);
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

    public bool GetOnDisease()
    {
        return onDisease;
    }
    
    public void SetDisease(float setDisease) // 병원의 치료 이벤트를 위한 함수
    {
        disease = setDisease;
        saveData.SaveDisease(setDisease);
    }

    public void SetOnDisease(bool setOnDisease) // 병원의 치료 이벤트를 위한 함수
    {
        onDisease = setOnDisease;
        saveData.SaveOnDisease(setOnDisease);
    }

    public void LoadDisease(float saveDisease)
    {
        disease = saveDisease;
    }

    public void LoadOnDisease(bool saveOndisease)
    {
        onDisease = saveOndisease;
    }
    
    public void LoadHungry(float savehungry)
    {
        hungry = savehungry;
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
        float state = 0.0f;
        Color color = diseaseImage.color;
        onDisease = true;
        saveData.SaveOnDisease(onDisease);
        if (!conditions.transform.GetChild(diseaseImageIndex).gameObject.activeSelf) // 이미 Active 상태일 경우 연산하지 않음
        {
            conditions.transform.GetChild(diseaseImageIndex).gameObject.SetActive(true);
        }
        if (index == 1)
        {
            state = bad;
        }
        else if (index == 2)
        {
            state = danger;
        }
        else if (index == 3)
        {
            state = fatal;
        }
        disease += reduce * state;
        color.g += reduce * state * diseaseConditionColor;
        color.b += reduce * state * diseaseConditionColor;
        diseaseImage.color = color;
        saveData.SaveDiseaseStateColor(color);
    }

    public void SetInactiveCondition() // 병원의 치료 이벤트를 위한 함수
    {
        conditions.transform.GetChild(diseaseImageIndex).gameObject.SetActive(false);
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
        saveData.SaveDiseaseStateColor(color);
    }

    private void SetActiveGameOver()
    {
        TextMeshProUGUI gameOverText = gameOver.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        if (hungry <= 0.0f)
        {
            gameOverText.text = "배고픔을 견디지 못하고 쓰러져버렸다...";
        }
        else if (disease <= 0.0f)
        {
            gameOverText.text = "질병을 치료하지 못해 쓰러져버렸다...";
        }
        gameOver.SetActive(true);
    }
}
