using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDog : MonoBehaviour
{
    public GameObject dog;
    public Transform pos;
    
    public void FindDog()
    {
        dog.SetActive(true);
        dog.transform.position = pos.position;
    }
}
