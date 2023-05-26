using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenTuto : MonoBehaviour
{
    //각 씬의 캔버스에 적용되어
    //사용자가 T를 누르면 튜토리얼 이미지가 띄워지게 만드는 스크립트
    public GameObject tutoSystem;
    private bool isTutoActive;
    private int count;
    public GameObject[] tutoImg;

    void Update()
    {
        OpenTutoImg();
        ClickCount();

        switch (count)
        {
            case 1:
                tutoImg[0].SetActive(false);
                tutoImg[1].SetActive(true);
                break;
            case 2:
                tutoImg[1].SetActive(false);
                tutoImg[2].SetActive(true);
                break;
            case 3:
                tutoImg[2].SetActive(false);
                tutoImg[3].SetActive(true);
                break;
            case 4:
                tutoImg[3].SetActive(false);
                tutoImg[4].SetActive(true);
                break;
            case 5:
                tutoImg[4].SetActive(false);
                tutoImg[5].SetActive(true);
                break;
            case 6:
                tutoImg[5].SetActive(false);
                tutoSystem.SetActive(false);
                isTutoActive = false;
                break;
        }
    }

    public void OpenTutoImg()
    {
        if (Input.GetKeyDown(KeyCode.T) && !isTutoActive)
        {
            Debug.Log("튜토리얼 오픈");
            tutoSystem.SetActive(true);
            isTutoActive = true;
        }
    }

    public void ClickCount()
    {
        if (isTutoActive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                count++;
            }
        }
        else
        {
            count = 0;
        }
    }
}
