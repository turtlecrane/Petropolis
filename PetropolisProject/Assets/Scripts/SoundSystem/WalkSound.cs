using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkSound : MonoBehaviour
{
    public AudioClip basicWalk;
    public AudioClip basicRun;
    public AudioClip grassWalk;
    public AudioClip grassRun;
    
    private AudioSource audioSource;
    
    //플레이어의 위치에따라서 걷는 소리가 달라집니다.
    //플레이어가 뛰는중인지 뛰지않는중인지 추적하여 오디오 클립을 바꿉니다.
    private PlayerStatus playerStatus;
    public int WalkFlag;
    
    void Start()
    {
        //playerStatus.moveStatus = 0-> 정지, 1-> 달리기는중, 2->걷는중
        playerStatus = GameObject.FindWithTag("Cat").gameObject.GetComponent<PlayerStatus>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        WalkFlag = playerStatus.moveStatus;
        switch (WalkFlag)
        {
            case 0: //정지
                audioSource.clip = null;
                audioSource.Stop();
                break;
            case 1: //달리기
                if (audioSource.clip != basicRun && !playerStatus.inGrass)
                {
                    audioSource.clip = basicRun;
                    audioSource.Play();
                }
                else if (audioSource.clip != grassRun && playerStatus.inGrass)
                {
                    audioSource.clip = grassRun;
                    audioSource.Play();
                }
                break;
            case 2: //걷기
                if (audioSource.clip != basicWalk && !playerStatus.inGrass)
                {
                    audioSource.clip = basicWalk;
                    audioSource.Play();
                }
                else if (audioSource.clip != grassWalk && playerStatus.inGrass)
                {
                    audioSource.clip = grassWalk;
                    audioSource.Play();
                }
                break;
        }
    }
}
