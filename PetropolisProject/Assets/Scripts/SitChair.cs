using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SitChair : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private GameObject target;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            target = GetClickedObject();

            if (target.Equals(gameObject))
            {
                Debug.Log("의자인거 확인");

            }
        }
    }
    private GameObject GetClickedObject()
    {
        RaycastHit hit;
        GameObject target = null;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (true == (Physics.Raycast(ray.origin, ray.direction * 10, out hit)))
        {
            target = hit.collider.gameObject;
        }
        return target;
    }
}
