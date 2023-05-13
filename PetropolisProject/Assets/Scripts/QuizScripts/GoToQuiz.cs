using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToQuiz : MonoBehaviour
{
    private ObjData objData;

    // Start is called before the first frame update
    void Start()
    {
        objData = GetComponent<ObjData>();
    }

    // Update is called once per frame
    void Update()
    {
        if (objData.id == 14001)
        {
            objData.id++;
            GetComponent<GotoScene>().SceneChange();
        }
    }
}
