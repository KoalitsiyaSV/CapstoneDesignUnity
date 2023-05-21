using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //UI
    [Header("UI")]
    public GameObject MenuUI;
    public GameObject InGameUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
