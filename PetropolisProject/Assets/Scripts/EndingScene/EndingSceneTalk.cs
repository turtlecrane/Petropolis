using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndingSceneTalk : MonoBehaviour
{
    public GameObject TalkBox;
    public TextMeshProUGUI NpcName;
    public TextMeshProUGUI Context;

    public EndAniController aniCtrl;
    private int CameraStat;

    public isCameraPlay cameraPlay;
    private bool CameraMove;

    public int count;
    
    private PlayerName playerName;
    public GameObject Remy;
    public GameObject NarOut;
    public GameObject Blink;
    
    void Start()
    {
        playerName = GameObject.Find("PlayerName").GetComponent<PlayerName>();
        count = 1;
        Invoke("TalkBoxActive", 11f);//씬에 들어오고 15초뒤에 텍스트박스 활성화 (대화 시작)
        Invoke("Surprised", 11f);//텍스트 박스 나타나면 놀람!
        NpcName.text = "[ 레미 ]";
        Context.text = "헉!! " + playerName.Name +"!!!!";
    }

    void Update()
    {
        CameraMove = cameraPlay.isMove;
        CameraStat = aniCtrl.CameraStat;

        if (!CameraMove)
        {
            ClickCount();
        }
        if(count == 2)
        {
            Context.text = "하루종일 어디 갔다 온거야??";
        }
        else if(count == 3)
        {
            aniCtrl.Camera.SetInteger("Stat",2);//시점변경
            aniCtrl.NPC.SetInteger("Status",3);//울기
            Context.text = "걱정했잖니 .... ";
        }
        else if (count == 4)
        {
            aniCtrl.NPC.SetInteger("Status",0);//idle
            Context.text = "빨리 방으로 들어가자 ! ";
        }
        else if (count == 5)
        {
            Remy.gameObject.SetActive(false);//레미퇴장
            TalkBoxInactive();
            aniCtrl.Camera.SetInteger("Stat",3);//시점변경
        }
        else if (count == 6)
        {
            TalkBoxActive();
            NpcName.text = "[ "+playerName.Name+" ]";
            Context.text = "주인에게 걱정시켜서 미안하네...";
        }
        else if (count == 7)
        {
            aniCtrl.Camera.SetInteger("Stat",4);
            Context.text = "그래도 오늘 하루 여기저기 도움을 많이 준거 같아서 즐거웠어!";
        }
        else if (count == 8)
        {
            aniCtrl.Camera.SetInteger("Stat",5);
            Blink.SetActive(true);
            Context.text = "집에 돌아오니까 졸음이 쏟아진다...";
        }
        else if (count == 9)
        {
            Blink.SetActive(false);
            aniCtrl.Camera.SetInteger("Stat",6);
            Context.text = "일단 자야겠다..!";
        }
        else if (count >= 10)
        {
            TalkBoxInactive();
            NarOut.gameObject.SetActive(true);
            Invoke("GotoResult",5f); //결과화면 씬으로 전환해야함 (임시로 튜토씬)
        }
    }

    public void Surprised()
    {
        aniCtrl.NPC.SetInteger("Status",2);//놀라기
    }

    public void TalkBoxActive()
    {
        TalkBox.gameObject.SetActive(true);
    }
    public void TalkBoxInactive()
    {
        TalkBox.gameObject.SetActive(false);
    }

    public void ClickCount()
    {
        if (Input.GetMouseButtonDown(0))
        {
            count++;
        }
    }

    public void GotoResult()
    {
        SceneManager.LoadScene("Result");
    }
}
