using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    public Car CloneTarget = null;
    public Transform GenerationPos = null;

    public int GenerationPersent = 50;  // 차량 나오는 확률

    public float CloneDelaySec = 1f;    // 딜레이 시간

    protected float NextSecToClone = 0f;

    public float carsetposY = 0.5f;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        float currSec = Time.time;
        if(NextSecToClone <= Time.time )
        {
            int randomval = Random.Range(0, 100);
            if( randomval <= GenerationPersent)
            {
                CloneCar();
            }
            NextSecToClone = currSec + CloneDelaySec;
        }
        
    }

    void CloneCar()
    {
        Transform clonepos = GenerationPos;
        Vector3 offsetpos = clonepos.position;
        offsetpos.y = carsetposY;

        GameObject cloneobj = GameObject.Instantiate(CloneTarget.gameObject, offsetpos, GenerationPos.rotation, this.transform);

        cloneobj.SetActive(true);

    }
}
