using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerCameraTest : MonoBehaviour
{
    private Vector3 offsets;                // 카메라의 초기 좌표, 현재 좌표
    private float xRotate, yRotate, xRotateMove, yRotateMove;

    public Camera camera;
    public GameObject player;
    public float rotateSpeed = 200.0f;        // 카메라 회전 속도

    public float offsetX = 0.0f;            // 카메라의 x좌표
    public float offsetY = 2.0f;           // 카메라의 y좌표
    public float offsetZ = -4.0f;          // 카메라의 z좌표

    public float cameraSpeed = 10.0f;       // 카메라의 속도

    public float cameraUp = 1.0f;          // 플레어이보다 조금 더 위쪽을 보기 위한 카메라 시점 보정치

    private Vector3 GazePos;                // 카메라 시선의 위치
    private Vector3 dir;                    // 플레이어 위치에서 카메라 방향으로의 방향
    private Vector3 hitOffsets;             // Ray가 hit한 위치에서의 offset
    private GameObject cameraPos;
    private float camera_Dist = 0f;         // 플레이어와 카메라 사이의 거리
    //private Color _rayColor = Color.green;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.lockState = CursorLockMode.Confined;  // 마우스 커서 잠금

        cameraPos = transform.GetChild(0).gameObject;
        offsets = new Vector3(offsetX, offsetY, offsetZ); // offset 초기화
        SetGazePos(); // GazePos 설정
        transform.position = player.transform.position;
        cameraPos.transform.position = transform.position + offsets; // 카메라 위치 초기화
        camera.transform.LookAt(GazePos);

        //camera_Dist = Vector3.Distance(Target.transform.position, transform.position);
        camera_Dist = Vector3.Distance(GazePos, cameraPos.transform.position); // 플레이어와 카메라 사이의 거리 계산
    }

    void LateUpdate()
    {
        transform.position = player.transform.position;
        
        xRotateMove = -Input.GetAxis("Mouse Y") * Time.deltaTime * rotateSpeed;;
        yRotateMove = Input.GetAxis("Mouse X") * Time.deltaTime * rotateSpeed;;

        yRotate = yRotate + yRotateMove;
        xRotate = xRotate + xRotateMove;

        xRotate = Mathf.Clamp(xRotate, -40, 40);
        //yRotate = Mathf.Clamp(yRotate, -90, 90);

        Quaternion quat = Quaternion.Euler(new Vector3(xRotate, yRotate, 0));
        transform.rotation 
            = Quaternion.Slerp(transform.rotation, quat, Time.deltaTime /* x speed */);
        
        dir = (cameraPos.transform.position - GazePos).normalized; // 현재 카메라의 위치와 플레이어 위치 간의 방향 계산

        SetGazePos();

        RaycastHit hitinfo;
        //Physics.Raycast(Target.transform.position, - dir, out hitinfo, camera_Dist);
        int layerMask = (1 << LayerMask.NameToLayer("Ignore Raycast"));  // Everything에서 Player 레이어만 제외하고 충돌 체크함
        layerMask  = ~layerMask ;
        Physics.Raycast(GazePos, dir, out hitinfo, camera_Dist, layerMask);

        if (hitinfo.point != Vector3.zero)  // Raycast 성공시
        {
            hitOffsets = hitinfo.point - (dir * 0.05f); // hit된 좌표에서 조금 앞으로 보정치를 줌
            camera.transform.position = Vector3.Lerp(camera.transform.position, hitOffsets, Time.deltaTime * cameraSpeed);
        }
        else
        {
            camera.transform.position = cameraPos.transform.position;
        }

        camera.transform.LookAt(GazePos);
    }

    //void OnDrawGizmos()
    //{
    //    Gizmos.color = _rayColor;
    //    Gizmos.DrawRay(Target.transform.position, dir * camera_Dist);
    //}

    void SetGazePos()
    {
        GazePos.x = transform.position.x;
        GazePos.y = transform.position.y + cameraUp;
        GazePos.z = transform.position.z;
    }
}
