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

    //�����̵� ��ư Ŭ�� �� �̺�Ʈ
    public void OnClickSlideBtn()
    {
        isMenuOpen = !isMenuOpen;

        if (isMenuOpen)
        {
            // ��ư���� ���� ��ġ ���
                targetPosition = new Vector2(targetPosition.x + PullMinX, targetPosition.y);
        }
        else
        {
            // ��ư���� ����� ��ġ ���
                targetPosition = new Vector2(targetPosition.x + PullMaxY, targetPosition.y);
        }
    }

    //�ΰ��� �޴� ��ư Ŭ�� �� �̺�Ʈ
    public void OnClickMenuBtn()
    {
        MenuUI.SetActive(true);
        InGameUI.SetActive(false);
    }

    //�޴�â->���� ���� ��ư
    public void OnClickExitBtn()
    {
        OnApplicationQuit();
    }

    //�޴�â->�޴� �ݱ� ��ư
    public void OnClickCloseBtn()
    {
        MenuUI.SetActive(false);
        InGameUI.SetActive(true);
    }

    //���� ����
    public void OnApplicationQuit()
    {
        Application.Quit();
    }
}