using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDog : MonoBehaviour
{
    public GameObject dog;
    public Transform pos;
    private Vector3 targetPostion;


    public void FindDog()
    {
        
        targetPostion = new Vector3(gameObject.transform.position.x, pos.transform.position.y, gameObject.transform.position.z);
        
        dog.SetActive(true);
        dog.transform.position = pos.position;
        dog.transform.LookAt(targetPostion);
        
    }
}
