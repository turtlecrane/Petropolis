using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

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

    public GameObject TutorialDoor;
    private Animator TutorialAnimator;

    // ray의 길이, 색상
    public float RayMaxDistance = 1.00f;
    private Color _rayColor = Color.green;

    private SettingsOnEsc soe;
    private ObjData objData;
    private Dictionary<int, string[]> interactionData;
    private int objId;
    private int interactionContentIndex = 0;  // foodData에서 interactbox에 표시되는 텍스트가 저장된 인덱스
    private TextMeshProUGUI interactionContent;

    private Transform _selection;

    public TimeattackManager TAManager;

    public RaycastHit hit;

    void Awake()
    {
        interactionData = new Dictionary<int, string[]>();
        InteractionTextData();
    }
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
                if (scanObject.GetComponent<ObjData>().isDoctor)
                {
                    scanObject.GetComponent<TreatManager>().SetIsDisease();
                }
            }
            else if (HitTag == "Interaction")
            {
                Action(scanObject);
                SetInteractText();
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
            else if (HitTag == "DoorOpen")
            {
                scanObject.GetComponent<Animator>().SetTrigger("Open");
            }
            else if (HitTag == "NextScene")
            {
                SceneManager.LoadScene("MainMap");
            }
            else if (HitTag == "Target" && TAManager.isGameRunning == true)
            {
                TAManager.InteractTarget();
                scanObject.SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            InteractBox.SetActive(false);
            TextBox.SetActive(false);
        }
        
        //////////////////////////////////////////////////////////////////////
        isHit = Physics.Raycast(transform.position, transform.forward, out hit, RayMaxDistance);
        //히트가 안되는 오브젝트들은 하이라이트 기능 없도록
        if (_selection != null)
        {
            var selectedRender = scanObject.GetComponent<Renderer>();
            var selectOutline = scanObject.GetComponent<Outline>();
            //selectedRender.material.color = new Color32(255, 255, 255, 255); //Default color
            selectOutline.OutlineWidth = 0; // 아웃라인 하이라이트 제거
            _selection = null;
        }

        if (isHit) //히트가 되면
        {
            var o = hit.collider.gameObject;
            scanObject = o;
            HitTag = o.tag;
            if (HitTag == "Interaction" || HitTag == "Food") // 상호작용이랑 푸드 태그는 빨간 테두리 하이라이트
            {
                var selection = scanObject.transform;
                var selectedRender = scanObject.GetComponent<Renderer>();
                var selectOutline = scanObject.GetComponent<Outline>();
                if (selectedRender != null)
                {
                    selectOutline.OutlineWidth = 7; //아웃라인 하이라이트
                    selectOutline.OutlineColor = Color.red;
                }
                _selection = selection;
            }
        }
        else
        {
            InteractBox.SetActive(false);
            TextBox.SetActive(false);
            scanObject = null;
            HitTag = "";
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = _rayColor;

        if (isHit)//히트가 되면
        {
            Gizmos.DrawRay(transform.position, transform.forward * hit.distance);
        }
        else //히트가 안되면
        {
            Gizmos.DrawRay(transform.position, transform.forward * RayMaxDistance);
        }
    }

    public void SelectYes() // interactBox의 Yes Button을 눌렀을 때 호출되는 함수
    {
        if (HitTag == "Food")
        {
            fmanager.SetCondition();
            var fod = scanObject.GetComponent<FoodObjData>();
            fod.Eat();
        }
        else if (HitTag == "Interaction")
        {
            switch (objId)
            {
                case (1):
                    scanObject.GetComponent<GotoScene>().SceneChange();
                    break;
                case (2):
                    scanObject.GetComponent<GotoScene>().SceneChange();
                    break;
                case (3):
                    scanObject.GetComponent<GotoScene>().SceneChange();
                    break;
                case (4):
                    scanObject.GetComponent<GotoScene>().SceneChange();
                    break;
                default:
                    break;
            }
        }
    }

    void InteractionTextData()
    {
        interactionData.Add(1, new string[] { "방으로 이동하시겠습니까? \n ( 엔딩 )" });
        interactionData.Add(2, new string[] { "병원으로 이동하시겠습니까?" });
        interactionData.Add(3, new string[] { "병원에서 나가시겠습니까?" });
        interactionData.Add(4, new string[] { "퀴즈 테스트" });
    }

    void SetInteractText()
    {
        interactionContent = InteractBox.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>(); // InteractBox의 Text 불러오기
        interactionContent.text = interactionData[objId][interactionContentIndex];
    }

    void Action(GameObject scanObj)
    {
        scanObject = scanObj;
        objData = scanObject.GetComponent<ObjData>();
        objId = objData.id;
    }
}