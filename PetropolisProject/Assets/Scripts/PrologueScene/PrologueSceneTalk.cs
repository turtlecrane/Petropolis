using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PrologueSceneTalk : MonoBehaviour
{
    
    public GameObject TalkBox;
    public TextMeshProUGUI NpcName;
    public TextMeshProUGUI Context;

    public ProAniController aniCtrl;
    private int CameraStat;

    public isCameraPlay cameraPlay;
    private bool CameraMove;

    public int count;
    
    private PlayerName playerName;
    public GameObject Remy;
    public GameObject NarOut;
    
    void Start()
    {
        playerName = GameObject.Find("PlayerName").GetComponent<PlayerName>();
        count = 1;
        Invoke("TalkBoxActive", 14f);//씬에 들어오고 15초뒤에 텍스트박스 활성화 (대화 시작)
        NpcName.text = "[ 레미 ]";
        Context.text = "아이 귀여워라 \n 이제부터 여기가 우리 집이야.";
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
            aniCtrl.NPC.SetInteger("Status",2);//박수시작
            Context.text = "앞으로도 지금처럼 건강했으면 좋겠다!\n 즐거운 일만 가득하게 될거야.";
        }
        else if(count == 3)
        {
            aniCtrl.Camera.SetInteger("Stat",2);//시점변경
            TalkBoxInactive();
        }
        else if (count == 4)
        {
            Remy.gameObject.SetActive(false);//레미퇴장
            TalkBoxActive();
            aniCtrl.Camera.SetInteger("Stat",3);
            NpcName.text = "[ "+playerName.Name+" ]";
            Context.text = "와! 주인이 너무 친절해 너무 행복하다";
        }
        else if (count == 5)
        {
            Context.text = "그런데 전부터 날 돌봐주시던 의사아저씨가 한 말이 자꾸 마음에 걸려...";
            
        }
        else if (count == 6)
        {
            aniCtrl.Camera.SetInteger("Stat",4);
            Context.text = "<color=#FFD400>[ 보호받지 못한채 방치되거나 떠나버리는 경우가 생각보다 매우 많거든요. ]</color> 라니...";
        }
        else if (count == 7)
        {
            aniCtrl.Camera.SetInteger("Stat",5);
            Context.text = "나는 이렇게 좋은 곳에서 지내는데 세상엔 아직 그렇지 못한 동물들이 많은가봐...";
        }
        else if (count == 8)
        {
            aniCtrl.Camera.SetInteger("Stat",3);
            Context.text = "어쩌면 내가 도움이 될 수 있지 않을까?!";
        }
        else if (count == 9)
        {
            aniCtrl.Camera.SetInteger("Stat",6);
            Context.text = "내일, 여기저기 돌아다니면서 이야기를 들어봐야겠다!";
        }
        else if (count == 10)
        {
            aniCtrl.Camera.SetInteger("Stat",7);
            Context.text = "돌아다니려면 몰래 나가야겠지...조금 미안하지만...별 문제 없을거야..!";
        }
        else if (count >= 11)
        {
            TalkBoxInactive();
            NarOut.gameObject.SetActive(true);
            Invoke("GotoTuto",5f);
        }
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

    public void GotoTuto()
    {
        SceneManager.LoadScene("Level_house");//튜토리얼로
    }
}
