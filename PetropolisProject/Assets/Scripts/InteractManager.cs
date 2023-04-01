using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractManager : MonoBehaviour
{
    public TalkManager manager;
    public FoodManager fmanager;
    GameObject scanObject;

    private bool isHit;
    private bool isMouseHit;
    private string HitTag;
    public GameObject InteractBox;
    public GameObject TextBox;

    // ray의 길이, 색상
    public float RayMaxDistance = 1.00f;
    private Color _rayColor = Color.green;

    private SettingsOnEsc soe;

    void Start()
    {
        soe = GameObject.Find("Canvas").GetComponent<SettingsOnEsc>();
    }

    void Update()
    {
        //마우스 버튼 입력 받기
        if (Input.GetMouseButtonDown(0) && isHit)
        {
            soe.isOpen = true;
            //Debug.Log("히트가 되었고 마우스를 클릭함.");
            if (HitTag == "NPC")
            {
                InteractBox.SetActive(false);
                TextBox.SetActive(true);
                manager.Action(scanObject);
            }
            else if (HitTag == "Interaction")
            {
                InteractBox.SetActive(true);
                TextBox.SetActive(false);
            }
            else if (HitTag == "Food") // Tag가 Food인 객체일 경우
            {
                fmanager.Action(scanObject);
                fmanager.SetContent();
                InteractBox.SetActive(true);
                TextBox.SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            InteractBox.SetActive(false);
            TextBox.SetActive(false);
        }
    }

    void OnDrawGizmos()
    {
        //isHit = Physics.BoxCast(transform.position, transform.lossyScale / 2.0f, transform.forward, out RaycastHit hit, transform.rotation, RayMaxDistance);
        isHit = Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, RayMaxDistance);
        Gizmos.color = _rayColor;

        if (isHit)//히트가 되면
        {
            Gizmos.DrawRay(transform.position, transform.forward * hit.distance);
            //Gizmos.DrawWireCube(transform.position + transform.forward * hit.distance, transform.lossyScale);

            var o = hit.collider.gameObject;
            scanObject = o;
            HitTag = o.tag;
        }
        else //히트가 안되면
        {
            Gizmos.DrawRay(transform.position, transform.forward * RayMaxDistance);
            InteractBox.SetActive(false);
            TextBox.SetActive(false);
            scanObject = null;
            HitTag = "";
        }
    }

    public void SelectYes() // interactBox의 Yes Button을 눌렀을 때 호출되는 함수
    {
        if (HitTag == "Food")
        {
            fmanager.SetCondition();
        }
    }
}