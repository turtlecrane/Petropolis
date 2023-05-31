using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnController : MonoBehaviour
{
    public void OnClickStartBtn()
    {
        Debug.Log("Clicked start btn");
        Invoke("GotoGameStage", 1f);   
    }
    public void GotoGameStage()
    {
        SceneManager.LoadScene("MainMap");
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
