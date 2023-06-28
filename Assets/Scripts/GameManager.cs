using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //UI
    [Header("UI")]
    public GameObject MenuUI;
    public GameObject InGameUI;
    public GameObject InventoryUI;
    public RectTransform ButtonGroup;
    //private float SlideSpeed = 1.5f;

    [Header("Cursor")]
    private bool isCursorActivated;

    //test
    [Header("test")]
    //public float PullMinX;
    //public float PullMaxY;

    [SerializeField] Texture2D CursorImage;

    private bool isButtonGroupPulled;
    private bool isInventoryOpen;
    private bool isMenuOpen;

    private Vector2 targetPosition;
    private float cameraHalfWidth;

    // Start is called before the first frame update
    void Start()
    {
        //cameraHalfWidth = Screen.width / 2 - 400;
        //targetPosition = ButtonGroup.anchoredPosition;

        Cursor.SetCursor(CursorImage, Vector2.zero, CursorMode.ForceSoftware);
    }

    // Update is called once per frame
    void Update()
    {
        // 마우스 제어
        //Cursor.lockState = CursorLockMode.Confined;

        //if (Input.GetKeyDown(KeyCode.LeftAlt)) isCursorActivated = true;
        //if (Input.GetKeyUp(KeyCode.LeftAlt)) isCursorActivated = false;

        //if (!isCursorActivated)
        //{
        //    Cursor.visible = false;
        //    Cursor.lockState = CursorLockMode.Locked;
        //}
        //else
        //{
        //    Cursor.visible = true;
        //    Cursor.lockState = CursorLockMode.None;
        //}

        // 버튼 슬라이드 기능
        //ButtonGroup.anchoredPosition = Vector2.Lerp(ButtonGroup.anchoredPosition, targetPosition, SlideSpeed * Time.deltaTime);
        
        //if(isMenuOpen)
        //{
        //    ButtonGroup.anchoredPosition = Vector2.Lerp(ButtonGroup.anchoredPosition, targetPosition, SlideSpeed * Time.deltaTime);
        //}
        //else
        //{
        //    ButtonGroup.anchoredPosition = Vector2.Lerp(ButtonGroup.anchoredPosition, targetPosition, SlideSpeed * Time.deltaTime);
        //}
    }

    //슬라이드 버튼 클릭 시 이벤트
    //public void OnClickSlideBtn()
    //{
    //    isButtonGroupPulled = !isButtonGroupPulled;

    //    if (isButtonGroupPulled)
    //    {
    //        // 버튼들을 당기는 위치 계산
    //            targetPosition = new Vector2(targetPosition.x - cameraHalfWidth, targetPosition.y);
    //    }
    //    else
    //    {
    //        // 버튼들을 숨기는 위치 계산
    //            targetPosition = new Vector2(targetPosition.x + cameraHalfWidth, targetPosition.y);
    //    }
    //}

    //메뉴 버튼 클릭 시 이벤트
    public void OnClickMenuBtn()
    {
        MenuUI.SetActive(true);
        InGameUI.SetActive(false);
    }

    //인벤토리 버튼 클릭 시 이벤트
    public void OnClickInvencoryBtn()
    {
        InventoryUI.SetActive(true);
        InGameUI.SetActive(false);
    }

    //메뉴 닫기 버튼 클릭 시 이벤트
    public void OnClickCloseMenuBtn()
    {
        MenuUI.SetActive(false);
        InGameUI.SetActive(true);
    }

    //인벤토리 닫기 버튼 클릭 시 이벤트
    public void OnClickCloseInventoryBtn()
    {
        InventoryUI.SetActive(false);
        InGameUI.SetActive(true);
    }

    //메뉴창->게임 종료 버튼
    public void OnClickExitBtn()
    {
        OnApplicationQuit();
    }


    //종료 대충
    public void OnApplicationQuit()
    {
        Application.Quit();
    }
}