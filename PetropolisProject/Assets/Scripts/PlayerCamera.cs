using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private Vector3 offsets;                // 카메라의 초기 좌표, 현재 좌표
    private Quaternion camTurnAngleY, camTurnAngleX; // 카메라의 회전 쿼터니언

    public float rotateSpeed = 5.0f;        // 카메라 회전 속도

    public GameObject Target;               // 카메라가 따라다닐 타겟

    public float offsetX = 0.0f;            // 카메라의 x좌표
    public float offsetY = 2.0f;           // 카메라의 y좌표
    public float offsetZ = -4.0f;          // 카메라의 z좌표

    public float CameraSpeed = 10.0f;       // 카메라의 속도

    public float CameraUp = 1.0f;           // 플레어이보다 조금 더 위쪽을 보기 위한 카메라 시점 보정치

    private Vector3 TargetPos;              // 타겟의 위치
    private Vector3 GazePos;                // 카메라 시선의 위치
    private Vector3 dir;                    // 플레이어 위치에서 카메라 방향으로의 방향
    private Vector3 hitOffsets;             // Ray가 hit한 위치에서의 offset
    private float camera_Dist = 0f;         // 플레이어와 카메라 사이의 거리
    //private Color _rayColor = Color.green;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.lockState = CursorLockMode.Confined;  // 마우스 커서 잠금
        offsets = new Vector3(offsetX, offsetY, offsetZ); // offset 초기화
 
        SetGazePos(); // GazePos 설정
        transform.position = Target.transform.position + offsets; // 카메라 위치 초기화
        transform.LookAt(GazePos);

        //camera_Dist = Vector3.Distance(Target.transform.position, transform.position);
        camera_Dist = Vector3.Distance(GazePos, transform.position); // 플레이어와 카메라 사이의 거리 계산
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
        //transform.position = Vector3.Lerp(transform.position, TargetPos, Time.deltaTime * CameraSpeed);

        //transform.LookAt(Target.transform);

        dir = (transform.position - GazePos).normalized; // 현재 카메라의 위치와 플레이어 위치 간의 방향 계산

        SetGazePos(); 

        //// 구현중

        RaycastHit hitinfo;
        //Physics.Raycast(Target.transform.position, - dir, out hitinfo, camera_Dist);
        Physics.Raycast(GazePos, dir, out hitinfo, camera_Dist);

        if (hitinfo.point != Vector3.zero)  // Raycast 성공시
        {
            hitOffsets = hitinfo.point - dir; // hit된 좌표에서 조금 앞으로 보정치를 줌
            hitOffsets = camTurnAngleY * camTurnAngleX * hitOffsets;
            transform.position = Vector3.Lerp(transform.position, hitOffsets, Time.deltaTime * CameraSpeed);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, TargetPos, Time.deltaTime * CameraSpeed);
        }
        ////

        transform.LookAt(GazePos);
    }

    //void OnDrawGizmos()
    //{
    //    Gizmos.color = _rayColor;
    //    Gizmos.DrawRay(Target.transform.position, dir * camera_Dist);
    //}

    void SetGazePos()
    {
        GazePos.x = Target.transform.position.x;
        GazePos.y = Target.transform.position.y + CameraUp;
        GazePos.z = Target.transform.position.z;
    }
}
