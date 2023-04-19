using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FoodType // 음식들이 플레이어에게 주는 영향 구분 Good = 0, Fever = 1, Disease = 2, Fatal = 3
{
    Good,
    Bad,
    Danger,
    Fatal,
}

public class FoodObjData : MonoBehaviour
{
    public int foodId = 0; // 음식의 식별번호 ex) Good = 1000, Bad = 2000
    public FoodType foodType;

    private int intfoodType; // FoodManager에 전달하기 위해 FoodType의 int형을 저장할 변수

    void Awake() // Inspector에서 설정한 FoodType에 따라 intfoodType에 값을 저장
    {
        switch (foodType)
        {
            case FoodType.Good:
                {
                    intfoodType = (int)FoodType.Good;
                    break;
                }
            case FoodType.Bad:
                {
                    intfoodType = (int)FoodType.Bad;
                    break;
                }
            case FoodType.Danger:
            {
                intfoodType = (int)FoodType.Danger;
                break;
            }
            case FoodType.Fatal:
            {
                intfoodType = (int)FoodType.Fatal;
                break;
            }
            default:
                break;
        }
    }

    public int GetFoodType() // FoodManager에 intfoodType을 전달하기 위한 함수
    {
        return intfoodType;
    }
}
