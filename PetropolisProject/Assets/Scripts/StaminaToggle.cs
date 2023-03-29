using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaToggle : MonoBehaviour
{
    public GameObject Stamina;
    public PlayerRigidbody playerRigidbody;
    private StaminaSetting staminaSetting;
    private Animator staminaAniMator;
    private bool isShow;
    void Start()
    {
        staminaSetting = Stamina.GetComponent<StaminaSetting>();
        staminaAniMator = Stamina.GetComponent<Animator>();
    }

    void Update()
    {
        //스테미너의 양이 실시간으로 스테미너 UI에 표시된다.
        staminaSetting.Stamina.fillAmount = playerRigidbody.stamina / 100.0f;
        
        //달리면 스테미너 UI를 활성화 시킨다. (기본적으론 비활성화)
        if (playerRigidbody.running == 1 && playerRigidbody.stamina > 0.06)
        {
            isShow = true;
            Stamina.SetActive(true);
        }
        
        //스테미너가 0.05이하면 비활성화
        //일반 달리지 않는 상태면 비활성화
        if (isShow)
        {
            if (playerRigidbody.stamina > 90.0f && playerRigidbody.running == 0)
            {
                staminaAniMator.SetInteger("Status",1);
                isShow = false;
                Invoke("Exit",0.5f);
            }
        }
    }

    private void Exit()
    {
        Stamina.SetActive(false);
    }
}
