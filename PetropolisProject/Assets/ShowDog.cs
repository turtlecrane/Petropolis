using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDog : MonoBehaviour
{
    public GameObject dog;
    public Transform pos;
    public bool isFind = false;

    void Update()
    {
        if (isFind)
        {
            FindDog();
        }
    }
    public void FindDog()
    {
        dog.SetActive(true);
        dog.transform.position = pos.position;
    }
}
