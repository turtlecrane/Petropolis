using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorMove : MonoBehaviour
{

    protected Rigidbody ActorBody = null;
    public GameObject PlayerSpawn;
    public GameObject EndSpawn;

    public GameObject finishUI;

    private int cnt = 0; // 차량 부딪힌 횟수 카운트

    void Start()
    {
        
    }

    public enum E_DirectionType
    {
        Up = 0,
        Down,
        Left,
        Right
    }
    [SerializeField]
    protected E_DirectionType m_DirectionType = E_DirectionType.Up;
    protected void SetActorMove(E_DirectionType p_movetype)
    {
        Vector3 offsetpos = Vector3.zero;

        switch (p_movetype)
        {
            case E_DirectionType.Up:
                offsetpos = Vector3.forward;
                break;
            case E_DirectionType.Down:
                offsetpos = Vector3.back;
                break;
            case E_DirectionType.Left:
                offsetpos = Vector3.left;
                break;
            case E_DirectionType.Right:
                offsetpos = Vector3.right;
                break;
            default:
                Debug.LogErrorFormat("SetActor Error : {0}", p_movetype);
                break;
        }

        this.transform.position += offsetpos;  // 캐릭터 이동
    }

    protected void InputUpdate()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SetActorMove(E_DirectionType.Up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SetActorMove(E_DirectionType.Down);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SetActorMove(E_DirectionType.Left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SetActorMove(E_DirectionType.Right);
        }
    }
    
    void Update()
    {
        InputUpdate();
    }

    protected void OnTriggerEnter(Collider other)
    {
        Debug.LogFormat("OnTriggerEnter : {0}, {1}", other.name, other.tag);

        if (other.tag.Contains("Crash"))
        {
            Debug.LogFormat("부딪혔다! 작업 처리하자.");
            this.transform.position = PlayerSpawn.transform.position;  // 차량과 부딪히면 맨 처음 지정 위치로 돌아감
            cnt++;
            if ( cnt >=3 )
            {
                this.transform.position = EndSpawn.transform.position;  // 3번 이상 부딪히면 목적지 강제이동, 끝내기 (+다침 상태관련 기능은 여기에)
                finishUI.SetActive(true);
                Invoke("LoadNextScene", 2f);
            }
        }

        if(other.CompareTag("Clear")) // 목적지 도착시
        {
            finishUI.SetActive(true);
            Invoke("LoadNextScene", 2f);
        }

    }

    protected void OnTriggerExit(Collider other)
    {
        
    }

    void LoadNextScene()
    {
        Debug.Log("다음 씬으로 넘어갑니다.");
    }
}
