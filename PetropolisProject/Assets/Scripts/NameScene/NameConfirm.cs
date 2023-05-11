using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class NameConfirm : MonoBehaviour
{
    public GameObject NameSpace;
    private GameObject talkManagerObj;
    private NameSceneTalk nameSceneTalk;
    private PlayerName playerName;
    
    [SerializeField] public TMP_InputField inputField;

    private void Start()
    {
        talkManagerObj = GameObject.Find("TalkManager");
        nameSceneTalk = talkManagerObj.GetComponent<NameSceneTalk>();
        playerName = GameObject.Find("PlayerName").GetComponent<PlayerName>();
    }
    public void OnButtonClick()
    {
        if (string.IsNullOrEmpty(inputField.text))
        {
            nameSceneTalk.count = 404;//error
            Debug.Log("아무것도 입력하지 않았습니다.");
        }
        else
        {
            Debug.Log("입력된 값: " + inputField.text);
            playerName.Name = inputField.text;//플레이어 이름 저장
            NameSpace.gameObject.SetActive(false);
            nameSceneTalk.count = 1000;//true
        }
    }
}
