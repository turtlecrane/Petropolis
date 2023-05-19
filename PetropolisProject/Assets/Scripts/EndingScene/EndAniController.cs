using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndAniController : MonoBehaviour
{
    public Animator Camera;
    public Animator NPC;
    public int CameraStat;
    public int NPCStat;
    
    void Start()
    {
        Invoke("SetChangeCamera", 14f);
    }

    void Update() 
    {
        CameraStat = Camera.GetInteger("Stat");
        if (CameraStat == 1)
        {
            NPC.SetInteger("Status",1);
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
