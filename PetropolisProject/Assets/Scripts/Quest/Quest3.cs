using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest3 : MonoBehaviour
{
    public GameObject doctor;
    public GameObject doctorExclamation;
    public GameObject target;
    public GameObject targetExclamation;
    private QuestManager qManager;
    // Start is called before the first frame update
    void Start()
    {
        qManager = GetComponent<QuestManager>();
        target.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (qManager.GetIngQuest_3())
        {
            doctorExclamation.gameObject.SetActive(false);
            if (!qManager.GetClearQuest_3())
            {
                targetExclamation.gameObject.SetActive(true);       
                target.gameObject.SetActive(true);
            }
            else
            {
                doctorExclamation.gameObject.SetActive(true);
                target.gameObject.SetActive(false);
            }
        }
        else
        {
            if (!qManager.GetClearQuest_3())
            {
                doctorExclamation.gameObject.SetActive(true);     
            }
            else
            {
                doctorExclamation.gameObject.SetActive(false);
                target.gameObject.SetActive(false);
            }
        }
    }

    public void Clear()
    {
        doctor.GetComponent<ObjData>().id++;
    }
}
