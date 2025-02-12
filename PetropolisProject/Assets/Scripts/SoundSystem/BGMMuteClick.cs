using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGMMuteClick : MonoBehaviour
{
   public bool isMute;
   public Button Button;
   public GameObject MuteImage;
   public Slider VolumeSlider;
   private AudioSource BGMSystem;
   
    void Start()
    {
        BGMSystem = GameObject.Find("BGMSystem").GetComponent<AudioSource>();
        isMute = false;
        Button.onClick.AddListener(toggleMute);
    }

    private void Update()
    {
        if (VolumeSlider.value > 0)
        {
            isMute = false;
            MuteImage.SetActive(false);
        }
    }

    private void toggleMute()
    {
        if (!isMute)
        {
            isMute = true;
            MuteImage.SetActive(true);
            VolumeSlider.value = 0f;
            BGMSystem.volume = VolumeSlider.value;
        }
        else if (isMute)
        {
            isMute = false;
            MuteImage.SetActive(false);
            VolumeSlider.value = 0.5f;
            BGMSystem.volume = VolumeSlider.value;
        }
    }
}
