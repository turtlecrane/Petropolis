using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnController : MonoBehaviour
{
    private void Update()
    {
        if (Time.timeScale != 1)//시간이 흐르고 있지 않다면
        {
            Time.timeScale = 1;
        }
    }

    public void OnClickStartBtn()
    {
        Debug.Log("Clicked start btn");
        Invoke("GotoGameStage", 1f);   
    }
    public void GotoGameStage()
    {
        SceneManager.LoadScene("NameScene");
    }

    public void OnClickOptionBtn()
    {
        Debug.Log("Clicked option btn");
    }

    public void OnClickExitBtn()
    {
        Debug.Log("Clicked exit btn");
        Application.Quit();
    }
}
