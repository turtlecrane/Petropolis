using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsOnEsc : MonoBehaviour
{
    public bool isOpen;
    public GameObject Setting;
    // public int count; //시간 흐름 체크용
    void Start()
    {
        isOpen = false;
    }

    void Update()
    {
        //계속해서 팝업 테그를 가진 오브젝트가 있는지 검사
        GameObject[] popups = GameObject.FindGameObjectsWithTag("Popup"); 
        
        if (popups.Length > 0)
        {
            Time.timeScale = 0;
            // Debug.Log("Popup found!");
        }
        else
        {
            Time.timeScale = 1;
            isOpen = false;
            // Debug.Log("Popup not found.");
        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isOpen)
            {
                isOpen = true;
                Setting.SetActive(true);
            }
            else if (isOpen)
            {
                isOpen = false;
                Setting.SetActive(false);
            }
            
            // 시간이 흐르고있는지 디버그
            // if (Time.timeScale == 1)
            // {
            //     count++;
            //     Debug.Log(count);
            // }
            
        }
    }
}
