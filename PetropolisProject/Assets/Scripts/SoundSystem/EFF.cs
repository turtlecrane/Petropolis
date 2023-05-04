using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EFF : MonoBehaviour
{
    //태그에 EffSound가 붙어있는 오디오 소스의 볼륨을 총괄합니다
    public GameObject[] effSounds;
    private AudioSource effAudioSource;
    public string sceneName;
    public float effVolume;
    void Start() //메인메뉴 -> Click만 찾아지는게 정상
    {
        effVolume = 0.5f;//효과음 볼륨 초기값
        effSounds = GameObject.FindGameObjectsWithTag("EffSound");
    }

    void Update() //메인메뉴에서 벗어났을때도 찾아야함
    {
        sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name; //현재 씬이 무엇인지 판별
        if (sceneName == "MainMenu")
        {
            foreach (GameObject effSound in effSounds)
            {
                AudioSource audioSource = effSound.GetComponent<AudioSource>();
                audioSource.volume = effVolume; //effsystem에 있는 float값이 모든 효과음의 볼륨수치가 된다.
            }
        }
        else //if (sceneName == "MainMap")  //메인메뉴를 제외한 씬들은 전부 씬에 입장하면 EffSound를 찾는다
        {
            effSounds = GameObject.FindGameObjectsWithTag("EffSound");//메인맵에 있는 EffSound태그 달린 오브젝트 추가(새로고침)
            foreach (GameObject effSound in effSounds)
            {
                AudioSource audioSource = effSound.GetComponent<AudioSource>();
                audioSource.volume = effVolume;
            }
        }
    }
}
