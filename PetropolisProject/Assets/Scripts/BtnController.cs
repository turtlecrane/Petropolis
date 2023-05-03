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
        SceneManager.LoadScene("MainMap");//추후에 캐릭터 선택씬으로 변경해야됨
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
