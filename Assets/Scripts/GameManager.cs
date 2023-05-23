using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //UI
    [Header("UI")]
    public GameObject MenuUI;
    public GameObject InGameUI;
    public RectTransform ButtonGroup;
    public float SlideSpeed = 5f;

    //test
    [Header("test")]
    private bool isMenuOpen;
    public float PullMinX;
    public float PullMaxY;

    private Vector2 targetPosition;
    

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = ButtonGroup.anchoredPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(isMenuOpen)
        {
            ButtonGroup.anchoredPosition = Vector2.Lerp(ButtonGroup.anchoredPosition, targetPosition, SlideSpeed * Time.deltaTime);
        }
    }

    //슬라이드 버튼 클릭 시 이벤트
    public void OnClickSlideBtn()
    {
        isMenuOpen = !isMenuOpen;

        if (isMenuOpen)
        {
            // 버튼들을 당기는 위치 계산
                targetPosition = new Vector2(targetPosition.x + PullMinX, targetPosition.y);
        }
        else
        {
            // 버튼들을 숨기는 위치 계산
                targetPosition = new Vector2(targetPosition.x + PullMaxY, targetPosition.y);
        }
    }

    //인게임 메뉴 버튼 클릭 시 이벤트
    public void OnClickMenuBtn()
    {
        MenuUI.SetActive(true);
        InGameUI.SetActive(false);
    }

    //메뉴창->게임 종료 버튼
    public void OnClickExitBtn()
    {
        OnApplicationQuit();
    }

    //메뉴창->메뉴 닫기 버튼
    public void OnClickCloseBtn()
    {
        MenuUI.SetActive(false);
        InGameUI.SetActive(true);
    }

    //종료 대충
    public void OnApplicationQuit()
    {
        Application.Quit();
    }
}