using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public GameObject gameObject;
    public TextMeshProUGUI tutorialText;

    private string[] textOptions = { "<color=#00FF00>W,A,S,D</color>를 눌러 움직입니다", "<color=#00FF00>Space Bar</color>를 눌러 점프합니다", "특정 오브젝트 앞에서 <color=#00FF00>왼쪽 마우스 버튼</color>를 눌러 상호작용 합니다", "<color=#00FF00>Left Shift</color>를 눌러 달립니다", "<color=#00FF00>[ T ] </color> 버튼을 눌러서 게임 튜토리얼을 확인 할 수 있어요!" };
    private int currentTextIndex = 0;
    private bool[] optionUsed;

    // Start is called before the first frame update
    void Start()
    {
        optionUsed = new bool[textOptions.Length];
        for (int i = 0; i < optionUsed.Length; i++)
        {
            optionUsed[i] = false;
        }
        tutorialText.text = textOptions[currentTextIndex];
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)||Input.GetKeyDown(KeyCode.A)||Input.GetKeyDown(KeyCode.S)||Input.GetKeyDown(KeyCode.D))
        {
            SetTextOption(1);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            SetTextOption(2);
        }
        else if (Input.GetMouseButtonDown(0))
        {
            SetTextOption(3);
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            SetTextOption(4);
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            gameObject.SetActive(false);
        }

    }

    private void SetTextOption(int index)
    {
        if (!optionUsed[index])
        {
            currentTextIndex = index;
            tutorialText.text = textOptions[currentTextIndex];
            optionUsed[currentTextIndex] = true;
            
        }
    }

}

