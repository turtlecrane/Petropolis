using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractManager : MonoBehaviour
{
    private bool isHit;
    private bool isMouseHit;
    private string HitTag;
    public GameObject InteractBox;
    public GameObject TextBox;
    // ray의 길이
    public float RayMaxDistance = 0.75f;
    // ray의 색상
    private Color _rayColor = Color.green;
   
    void Update()
    {
        //마우스 버튼 입력 받기
        if (Input.GetMouseButtonDown(0)) {
            //클릭한곳에 레이 발사
            RaycastHit mousehit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            isMouseHit = Physics.Raycast(ray, out mousehit);
            
            if (isMouseHit && isHit) { //마우스를 클릭했는데 클릭한 지점에 충돌되는 오브젝트가 있으며, 플레이어도 충돌중이면!
                if (HitTag == "NPC" && HitTag == mousehit.transform.tag)
                {
                    // Debug.Log("충돌한 오브젝트와 클릭한 오브젝트가 같습니다. ( NPC )");
                    InteractBox.SetActive(false);
                    TextBox.SetActive(true);
                }
                if (HitTag == "Food" && HitTag == mousehit.transform.tag)
                {
                    // Debug.Log("충돌한 오브젝트와 클릭한 오브젝트가 같습니다. ( Food )");
                    InteractBox.SetActive(true);
                    TextBox.SetActive(false);
                }
            }
        }
    }
    
    void OnDrawGizmos()
    {
        isHit = Physics.BoxCast(transform.position, transform.lossyScale / 3.0f, 
            transform.forward, out RaycastHit hit, transform.rotation, RayMaxDistance);
        Gizmos.color = _rayColor;

        if (isHit)//히트가 되면
        {
            Gizmos.DrawRay(transform.position, transform.forward * hit.distance);
            Gizmos.DrawWireCube(transform.position + transform.forward * hit.distance, transform.lossyScale);
            HitTag = hit.collider.gameObject.tag;
            //Debug.Log(hit.collider.gameObject.name);
        }
        else //히트가 안되면
        {
            Gizmos.DrawRay(transform.position, transform.forward * RayMaxDistance);
            InteractBox.SetActive(false);
            TextBox.SetActive(false);
            HitTag = "";
        }
    }
}
