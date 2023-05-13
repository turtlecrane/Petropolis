using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToQuiz : MonoBehaviour
{
    private ObjData objData;

    private bool doQuiz;
    // Start is called before the first frame update
    void Start()
    {
        doQuiz = false;
        objData = GetComponent<ObjData>();
    }

    // Update is called once per frame
    void Update()
    {
        if (objData.id == 12001 && !doQuiz)
        {
            doQuiz = true;
            GetComponent<GotoScene>().SceneChange();
        }
    }
}
