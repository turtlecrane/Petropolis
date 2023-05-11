using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    //카메라 워크에 따른 의사 움직임 변경하기
    public Animator Camera;
    public Animator NPC;
    public int CameraStat;

    void Start()
    {
        Invoke("SetChangeCamera", 20f);
    }

    void Update() 
    {
        CameraStat = Camera.GetInteger("Stat");
        switch (CameraStat)
        {
            case 1:
                ChangeDoctor();
                break;
            case 2:
                break;
            case 3:
                break;
        }
    }

    void SetChangeCamera()
    {
        Camera.SetInteger("Stat",1);
    }

    void ChangeDoctor()
    {
        NPC.SetInteger("Status",CameraStat);
    }
}
