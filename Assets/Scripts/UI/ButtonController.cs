using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [Header("UI")]
    public GameObject InGameUI;
    public GameObject MenuUI;
    public GameObject SettingUI;

    public RectTransform BookPos;
    public float testTime;

    public Vector2 targetPos;

    private Animator animator;
    private bool isMenuOpen = false;

    public void Start()
    {
        animator = GetComponent<Animator>();
        targetPos = new Vector2(0, 0);
    }

    public void Update()
    {
        if (isMenuOpen)
        {
            BookPos.anchoredPosition = Vector2.Lerp(BookPos.anchoredPosition, targetPos, 3f * Time.deltaTime);
        }
    }

    public void OnClickMenuBtn()
    {
        targetPos = new Vector2(0, 0);
        InGameUI.SetActive(false);
        MenuUI.SetActive(true);
        isMenuOpen = !isMenuOpen;
    }

    public void OnClickInventoryBtn()
    {
        InGameUI.SetActive(false);
        SettingUI.SetActive(true);
    }

    public void OnClickCloseMenuBtn()
    {
        Invoke("CloseMenuBtnLerp", 1f);
        Invoke("CloseMenuBtnAction", 2f);
    }

    private void CloseMenuBtnLerp()
    {
        targetPos = new Vector2(0, -500f);
        BookPos.anchoredPosition = Vector2.Lerp(BookPos.anchoredPosition, targetPos, 3f * Time.deltaTime);
    }

    private void CloseMenuBtnAction()
    {
        MenuUI.SetActive(false);
        InGameUI.SetActive(true);
        isMenuOpen = !isMenuOpen;
    }

    public void OnClickCloseInventoryBtn()
    {
        SettingUI.SetActive(false);
        InGameUI.SetActive(true);
    }

    public void OnClickExitBtn()
    {
        OnApplicationQuit();
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
    }


}
