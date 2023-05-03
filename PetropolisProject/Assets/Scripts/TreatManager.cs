using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TreatManager : MonoBehaviour
{
    public FoodManager foodManager;
    private ObjData objData;
    private NpcController npcController;
    private bool isDisease;
    
    
    // Start is called before the first frame update
    void Start()
    {
        isDisease = false;
        objData = GetComponent<ObjData>();
        npcController = GetComponent<NpcController>();
    }

    public void Treatment() // 질병 치료
    {
        if (isDisease)
        {
            foodManager.SetDisease(foodManager.maxDisease);
            foodManager.SetOnDisease(false);
            foodManager.SetInactiveCondition();
            isDisease = false;
        }
    }

    public void SetIsDisease() // foodManager로부터 질병 유무를 받아옴
    {
        isDisease = foodManager.GetOnDisease();
    }

    public void DiseaseCheck()
    {
        if (isDisease)
        {
            objData.id++; // 질병에 걸린 상태라면 id를 한번 더 증가시켜줌
        }
    }
}
