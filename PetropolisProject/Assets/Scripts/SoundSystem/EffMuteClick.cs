using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffMuteClick : MonoBehaviour
{
    public bool isMute;
    public Button Button;
    public GameObject MuteImage;
    public Slider VolumeSlider;
    private EFF EffSystem;
   
    void Start()
    {
        EffSystem = GameObject.Find("EFFSystem").GetComponent<EFF>();
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
            EffSystem.effVolume = VolumeSlider.value;
        }
        else if (isMute)
        {
            isMute = false;
            MuteImage.SetActive(false);
            VolumeSlider.value = 0.5f;
            EffSystem.effVolume = VolumeSlider.value;
        }
    }
}
