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
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            startUI.SetActive(false);
        }
    }

    void ActiveUI()
    {
        startUI.SetActive(isActive);
    }
}
