using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGMSlider : MonoBehaviour
{
    private Slider slider;
    private AudioSource BGMSystem;
    
    private BGM bgm;
    public float LastSaveVolume = 1f;
    void Start()
    {
        bgm = GameObject.Find("BGMSystem").GetComponent<BGM>();
        BGMSystem = GameObject.Find("BGMSystem").GetComponent<AudioSource>();
        slider = GetComponent<Slider>();
        LastSaveVolume = PlayerPrefs.GetFloat("LastSaveVolume", 1f); // PlayerPrefs에서 마지막으로 저장된 볼륨값 불러오기
        slider.value = LastSaveVolume;
        BGMSystem.volume = LastSaveVolume;
    }

    void Update()
    {
        BGMSystem.volume = slider.value;
        // 슬라이더 값을 저장하기 위해 이벤트 리스너를 추가
        slider.onValueChanged.AddListener(delegate {SaveVolume();});
        bgm.prevVolume = LastSaveVolume;
    }
    
    void SaveVolume()
    {
        LastSaveVolume = slider.value;
        PlayerPrefs.SetFloat("LastSaveVolume", LastSaveVolume); // 마지막으로 저장된 볼륨값을 PlayerPrefs에 저장
        PlayerPrefs.Save(); // PlayerPrefs 값을 저장
    }
}
