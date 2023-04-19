using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TreatManager : MonoBehaviour
{
    public FoodManager foodManager;
    public TalkManager talkManager;
    private SettingsOnEsc settingsOnEsc;
    private ObjData objData;
    
    public bool isChange;
    private bool ChangeID;
    
    
    // Start is called before the first frame update
    void Start()
    {
        objData = GetComponent<ObjData>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Treatment()
    {
        if (foodManager.GetOnDisease())
        {
            foodManager.SetDisease(foodManager.maxDisease);
            foodManager.SetOnDisease(false);
            foodManager.SetInactiveCondition();
        }
    }

    private void DoctorTalk()
    {

    }
}
