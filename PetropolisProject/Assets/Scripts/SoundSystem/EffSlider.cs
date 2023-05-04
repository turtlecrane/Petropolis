using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffSlider : MonoBehaviour
{
    private Slider slider;
    private EFF EffSystem;
    void Start()
    {
        EffSystem = GameObject.Find("EFFSystem").GetComponent<EFF>();//EFF에 있는 effVolume값으로 볼륨을 "일괄조절" 함
        slider = GetComponent<Slider>();
        slider.value = EffSystem.effVolume;
    }

    void Update()
    {
        EffSystem.effVolume = slider.value;
    }
}