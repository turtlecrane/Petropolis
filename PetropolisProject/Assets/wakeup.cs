using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wakeup : MonoBehaviour
{
    private float fallTime;
    private Rigidbody rbGO;
    private bool sleeping;

    // Start is called before the first frame update
    void Start()
    {
        rbGO = gameObject.AddComponent<Rigidbody>();
        rbGO.mass = 10.0f;
        //Physics.gravity = new Vector3(0, -2.0f, 0);
        sleeping = false;
        fallTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (fallTime > 1.0f)
        {
            if (sleeping)
            {
                rbGO.WakeUp();
                //Debug.Log("wakeup");
            }

            sleeping = !sleeping;

            fallTime = 0.0f;
        }

        fallTime += (5 * Time.deltaTime);
    }
}


