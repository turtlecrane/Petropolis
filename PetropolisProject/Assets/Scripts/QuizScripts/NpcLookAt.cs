using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcLookAt : MonoBehaviour // 퀴즈의 Npc가 카메라 방향을 보도록 만드는 클래스
{
    // Start is called before the first frame update
    void Start()
    {
        Camera camera = Camera.main;
        transform.LookAt(camera.transform.position);
    }
}
