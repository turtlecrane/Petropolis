using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isCameraPlay : MonoBehaviour
{
    private Animator animator;
    public bool isMove;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //카메라가 움직이고 있는지 멈췄는지 추적
         if (animator != null && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            isMove = true;
        }
        else
        {
            isMove = false;
        }
    }
}