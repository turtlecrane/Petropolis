using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GotoScene : MonoBehaviour
{
    public String SceneName;

    public void SceneChange()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneName);
        Debug.Log("Change Scene . . .");
    }
}
