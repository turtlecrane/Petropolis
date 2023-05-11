using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class NameSceneTalk : MonoBehaviour
{
    public GameObject TalkBox;
    public TextMeshProUGUI NpcName;
    public TextMeshProUGUI Context;

    public AnimationController aniCtrl;
    private int CameraStat;

    public isCameraPlay cameraPlay;
    private bool CameraMove;

    public int count;
    public GameObject NameSpace;

    private PlayerName playerName;
    
    void Start()
    {
        playerName = GameObject.Find("PlayerName").GetComponent<PlayerName>();
        count = 1;
        Invoke("TalkBoxActive", 21f);//씬에 들어오고 15초뒤에 텍스트박스 활성화 (대화 시작)
        NpcName.text = "[ 수의사 ]";
        Context.text = "오 안녕하세요. \n 고양이를 분양 받고싶다고 하신 고객님이셨죠? \n";
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
            aniCtrl.Camera.SetInteger("Stat",2);
            Context.text = "애완동물을 기른다는것은 정말 깊게 고민하고 결정해야 합니다. \n 보호받지 못한채 방치되거나 떠나버리는 경우가 생각보다 매우 많거든요.\n";
        }
        else if(count == 3)
        {
            Context.text = "다행히도 저희 동물병원에 아직 분양받지못한 고양이 한마리를 데리고있습니다. \n 믿음직 스러운 고객님이 저희 동물병원 고양이를 분양 받고싶어하셔서 다행입니다.";
        }
        else if (count == 4)
        {
            aniCtrl.Camera.SetInteger("Stat",3);
            TalkBoxInactive();
        }
        else if (count == 5)
        {
            aniCtrl.Camera.SetInteger("Stat",4);
            if (!CameraMove)
            {
                TalkBoxActive();
            }
            Context.text = "자, 그러면 이제부터 가족이 될 이 아이에게 이름을 지어주겠어요?";
        }
        else if (count == 6)
        {
            TalkBoxInactive();
            NameBoxActive();
        }
        else if (count == 1000)
        {
            TalkBoxActive();
            Context.text = "[ " + playerName.Name + " ]..."+"예쁜 이름이네요. \n 이 아이에게 관심을 많이 주며 키우시길 바랍니다. \n 동물에게 이상이 생기거나 문제가 생길경우 상점가에있는 우리 병원을 찾아주세요!";
        }
        else if (count == 1001)
        {
            TalkBoxInactive();
            SceneManager.LoadScene("PrologueScene");//튜토리얼로
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
    public void NameBoxActive()
    {
        NameSpace.gameObject.SetActive(true);
    }
    public void NameBoxInactive()
    {
        NameSpace.gameObject.SetActive(true);
    }

    public void ClickCount()
    {
        if (Input.GetMouseButtonDown(0))
        {
            count++;
        }
    }
}
