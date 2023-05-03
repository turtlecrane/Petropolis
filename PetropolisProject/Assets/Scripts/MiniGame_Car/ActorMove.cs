using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorMove : MonoBehaviour
{

    protected Rigidbody ActorBody = null;
    
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

        this.transform.position += offsetpos;
    }
    
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SetActorMove(E_DirectionType.Up);
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow))
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

    protected void OnTriggerEnter(Collider other)
    {
        Debug.LogFormat("OnTriggerEnter : {0}, {1}", other.name, other.tag);

        if (other.tag.Contains("Crash"))
        {
            Debug.LogFormat("부딪혔다! 작업 처리하자.");

        }


    }

    protected void OnTriggerExit(Collider other)
    {
        
    }
}
