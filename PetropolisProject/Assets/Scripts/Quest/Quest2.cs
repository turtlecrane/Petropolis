using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest2 : MonoBehaviour
{
    public GameObject dogExclamation;
    public GameObject targetExclamation;
    private QuestManager qManager;
    // Start is called before the first frame update
    void Start()
    {
        qManager = GetComponent<QuestManager>();
    }

    public void ExclamationOnOff()
    {
        if (qManager.GetIngQuest_2())
        {
            if (qManager.GetClearQuest_2())
            {
                dogExclamation.SetActive(true);
                targetExclamation.SetActive(false);
            }
            else
            {
                dogExclamation.SetActive(false);
                targetExclamation.SetActive(true);
            }
        }
        else
        {
            if (qManager.GetClearQuest_2())
            {
                dogExclamation.SetActive(false);
                targetExclamation.SetActive(false);
            }
            else if (qManager.GetFailQuest_2())
            {
                dogExclamation.SetActive(false);
                targetExclamation.SetActive(false);
            }
            else
            {
                dogExclamation.SetActive(true);
                targetExclamation.SetActive(false);
            }
        }
    }
}
