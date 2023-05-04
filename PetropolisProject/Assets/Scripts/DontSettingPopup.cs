using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontSettingPopup : MonoBehaviour
{
    private GameObject Canvas;
    private SettingsOnEsc settingsOnEsc;
    void Start()
    {
        Canvas = GameObject.Find("Canvas");
        settingsOnEsc = Canvas.GetComponent<SettingsOnEsc>();
    }

    void Update()
    {
        DontDisplaySettingPopup();
    }

    public void DontDisplaySettingPopup()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!settingsOnEsc.isOpen)
            {
                settingsOnEsc.isOpen = true;
            }
        }
    }
    
}
