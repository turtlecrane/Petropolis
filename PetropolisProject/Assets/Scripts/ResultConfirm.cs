using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultConfirm : MonoBehaviour
{
    private GameObject SaveData;
    private GameObject PlayerName;

    public void resultClick()
    {
        SaveData = GameObject.Find("SaveData");
        PlayerName = GameObject.Find("PlayerName");
        Destroy(SaveData);
        Destroy(PlayerName);
    }

}
