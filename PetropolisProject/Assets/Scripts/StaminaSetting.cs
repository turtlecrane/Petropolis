using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaSetting : MonoBehaviour
{
    private GameObject FImage;
    private Image Stamina;

    private void Start()
    {
        FImage = GameObject.FindWithTag("Stamina");
        Stamina = FImage.GetComponent<Image>();
    }

    void Update()
    {
        if (Stamina.fillAmount < 0.5f)
        {
            Stamina.color = new Color(255/255f, 186/255f, 66/255f);
            if (Stamina.fillAmount < 0.22f)
            {
                Stamina.color= new Color(255/255f, 90/255f, 89/255f);
            }
        }
        else
        {
            Stamina.color= new Color(38/255f, 200/255f, 78/255f);
        }
    }
}
