using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject startUI;
    //public GameObject finishUI;

    private bool isActive = true;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("ActiveUI", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            startUI.SetActive(false);
        }
    }

    void ActiveUI()
    {
        startUI.SetActive(isActive);
    }
}
