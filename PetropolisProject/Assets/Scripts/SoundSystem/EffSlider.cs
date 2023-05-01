using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffSlider : MonoBehaviour
{
    private Slider slider;
    private AudioSource EffSystem;
    void Start()
    {
        EffSystem = GameObject.Find("EffSystem").GetComponent<AudioSource>();
        slider = GetComponent<Slider>();
        slider.value = EffSystem.volume;
    }

    void Update()
    {
        EffSystem.volume = slider.value;
    }
}