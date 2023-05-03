using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public float MoveSpeed = 4f;
    public float RangeDestroy = 8;

    void Start()
    {
        
    }

    
    void Update()
    {
        float movex = MoveSpeed * Time.deltaTime;
        this.transform.Translate(movex, 0f, 0f);

        if (this.transform.localPosition.x >= RangeDestroy)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
