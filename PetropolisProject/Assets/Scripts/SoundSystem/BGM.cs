using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    public AudioClip mainMenuAudio;
    public AudioClip HomeTownAudio;
    public AudioClip ParkAudio;
    public AudioClip DownTownAudio;
    public AudioClip alleyAudio;
    public AudioClip tutoAudio;
    public AudioClip RoadMiniGame;
    public AudioClip QuizMiniGame;
    public AudioClip NameCut;
    public AudioClip ProlCut;
    public AudioClip ResAudio;
    
    private AudioSource audioSource;
    public int BgmFlag; //플레이어가 어느 위치에 있는지 가져와서 브금을 바꿔줌 -> PlayerStatus.cs와 연결됨
    
    private string sceneName;//씬에 따라 다른 브금을 설정
    private bool mainMapAudioPlayed = false;
    private bool catRig3Found = false;

    public float prevVolume; //마지막으로 수정된 브금볼륨값이 저장됨. -> BGMSlider.cs와 연결됨
    public float fadeOutSpeed = 1f;

    public bool isMenu = false;
    public bool isTuto = false;
    public bool isRoadMiniGame = false;
    public bool isQuizMiniGame = false;
    public bool isNameCut = false;
    public bool isProlCut = false;
    public bool isEndlCut = false;
    public bool isRes = false;
    void Start()
    {
        isTuto = false;
        prevVolume = 1f;//브금 볼륨 초기값
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = mainMenuAudio;
        audioSource.Play();
    }

    void Update()
    {
        sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        if (sceneName == "MainMenu_Played" && !isMenu)
        {
            isRes = false;
            audioSource.clip = mainMenuAudio;
            audioSource.volume = prevVolume;
            audioSource.Play();
            isMenu = true;
        }
        if (sceneName == "NameScene" && !isNameCut)
        {
            isMenu = false;
            audioSource.clip = NameCut;
            audioSource.volume = prevVolume;
            audioSource.Play();
            isNameCut = true;
        }
        else if (sceneName == "PrologueScene" && !isProlCut)
        {
            isNameCut = false;
            audioSource.clip = ProlCut;
            audioSource.volume = prevVolume;
            audioSource.Play();
            isProlCut = true;
        }
        else if (sceneName == "EndingScene" && !isEndlCut)
        {
            audioSource.clip = ProlCut;
            audioSource.volume = prevVolume;
            audioSource.Play();
            isEndlCut = true;
        }
        else if (sceneName == "Result" && !isRes)
        {
            isMenu = false;
            isEndlCut = false;
            audioSource.clip = ResAudio;
            audioSource.volume = prevVolume;
            audioSource.Play();
            isRes = true;
        }
        else if (sceneName == "Level_house" && !isTuto)
        {
            isProlCut = false;
            audioSource.clip = tutoAudio;
            audioSource.volume = prevVolume;
            audioSource.Play();
            isTuto = true;
        }
        else if (sceneName == "Quiz" && !isRoadMiniGame)
        {
            isQuizMiniGame = false;
            audioSource.clip = RoadMiniGame;
            audioSource.volume = prevVolume;
            audioSource.Play();
            isRoadMiniGame = true;
        }
        else if (sceneName == "RoadMiniGame" && !isQuizMiniGame)
        {
            isRoadMiniGame = false;
            audioSource.clip = QuizMiniGame;
            audioSource.volume = prevVolume;
            audioSource.Play();
            isQuizMiniGame = true;
        }
        else if (sceneName == "MainMap")
        {
            isRoadMiniGame = false;
            isQuizMiniGame = false;
            isTuto = false;
            PlayerStatus playerStatus = GameObject.Find("catRig3").GetComponent<PlayerStatus>();
            BgmFlag = playerStatus.AreaID; //플레이어의 위치정보를 가져와서 브금 바꿔주기
            /*if (!mainMapAudioPlayed)
            {
                audioSource.Play();
                mainMapAudioPlayed = true; 
            }*/
            
            // BgmFlag에 따라 배경음악을 변경
            switch (BgmFlag)
            {
                case 0:
                    if (audioSource.volume > 0)
                    {
                        audioSource.volume -= fadeOutSpeed*Time.deltaTime;//점차적으로 브금 줄어들기
                    }
                    else
                    {
                        audioSource.clip = null;
                        audioSource.Stop();
                    }
                    break;
                case 1:
                    if (audioSource.clip != HomeTownAudio)
                    {
                        audioSource.volume = prevVolume;
                        audioSource.clip = HomeTownAudio;
                        audioSource.Play();
                    }
                    break;
                case 2:
                    if (audioSource.clip != ParkAudio)
                    {
                        audioSource.volume = prevVolume;
                        audioSource.clip = ParkAudio;
                        audioSource.Play();
                    }
                    break;
                case 3:
                    if (audioSource.clip != DownTownAudio)
                    {
                        audioSource.volume = prevVolume;
                        audioSource.clip = DownTownAudio;
                        audioSource.Play();
                    }
                    break;
                case 5:
                    if (audioSource.clip != alleyAudio)
                    {
                        audioSource.volume = prevVolume;
                        audioSource.clip = alleyAudio;
                        audioSource.Play();
                    }
                    break;
                default:
                    break;
            }
            
        }
    }
}
