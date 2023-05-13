using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetDoctorId : MonoBehaviour
{
    public GameObject doctor;
    public void Reset()
    {
        doctor.GetComponent<ObjData>().id = 2000;
    }
}
