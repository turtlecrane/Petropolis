using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private Vector3 startoffsets, offsets;                // 카메라의 초기 좌표, 현재 좌표
    Quaternion camTurnAngleY, camTurnAngleX; // 카메라의 회전 쿼터니언

    public float rotateSpeed = 5.0f;        // 카메라 회전 속도

    public GameObject Target;               // 카메라가 따라다닐 타겟

    public float offsetX = 0.0f;            // 카메라의 x좌표
    public float offsetY = 2.0f;           // 카메라의 y좌표
    public float offsetZ = -4.0f;          // 카메라의 z좌표

    public float CameraSpeed = 10.0f;       // 카메라의 속도
    Vector3 TargetPos;                      // 타겟의 위치

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.lockState = CursorLockMode.Confined;
        startoffsets = new Vector3(offsetX, offsetY, offsetZ);
        offsets = startoffsets;
    }

    void LateUpdate()
    {
        //if (Input.GetMouseButton(0)) // 마우스 좌클릭 시
        //{
        camTurnAngleY = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotateSpeed, Vector3.up); // 마우스 X축 -> 좌우 회전(오브젝트 Y축)
        camTurnAngleX = Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * rotateSpeed, Vector3.left); // 마우스 Y축 -> 상하 회전(오브젝트 X축)
        offsets = camTurnAngleY * camTurnAngleX * offsets;
        //}

        // 타겟의 x, y, z 좌표에 카메라의 좌표를 더하여 카메라의 위치를 결정
        TargetPos = Target.transform.position + offsets;

        // 카메라의 움직임을 부드럽게 하는 함수(Lerp)
        transform.position = Vector3.Lerp(transform.position, TargetPos, Time.deltaTime * CameraSpeed);

        transform.LookAt(Target.transform);
    }
}
