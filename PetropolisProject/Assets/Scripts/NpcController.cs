using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    // NPC의 상태를 제어하는 스크립트
    public int State; //상태 디버깅 ( 1 = 긍정, 2 = 부정 ) -> TalkManager.cs에서 제어됨.
    Animator NpcAnimation;
    
    void Start()
    {
        NpcAnimation = GetComponent<Animator>();
    }

    void Update()
    {
        NpcAnimation.SetInteger("Status",State);
    }
}
