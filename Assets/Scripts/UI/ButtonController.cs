using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [Header("UI")]
    public GameObject InGameUI;
    public GameObject MenuUI;
    public GameObject InventoryUI;

    private bool isInventoryOpen;
    private bool isMenuOpen;

    public void OnClickMenuBtn()
    {
        InGameUI.SetActive(false);
        MenuUI.SetActive(true);
    }

    public void OnClickInventoryBtn()
    {
        InGameUI.SetActive(false);
        InventoryUI.SetActive(true);
    }

    public void OnClickCloseMenuBtn()
    {
        MenuUI.SetActive(false);
        InGameUI.SetActive(true);
    }

    public void OnClickCloseInventoryBtn()
    {
        InventoryUI.SetActive(false);
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
