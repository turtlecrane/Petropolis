using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultConfirm : MonoBehaviour
{
    private GameObject SaveData;
    private GameObject PlayerName;
    private GameObject Effsound;

    public void resultClick()
    {
        SaveData = GameObject.Find("SaveData");
        PlayerName = GameObject.Find("PlayerName");
        Effsound = GameObject.Find("EffectSound");
        Destroy(SaveData);
        Destroy(PlayerName);
        Destroy(Effsound);
    }

}
