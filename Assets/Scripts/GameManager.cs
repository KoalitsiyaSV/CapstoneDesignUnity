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
    private float SlideSpeed = 1.5f;

    [Header("Cursor")]
    private bool isCursorActivated;

    //test
    [Header("test")]
    //public float PullMinX;
    //public float PullMaxY;

    [SerializeField] Texture2D CursorImage;

    private bool isButtonGroupPulled;
    private Vector2 targetPosition;
    private float cameraHaldWidth;

    // Start is called before the first frame update
    void Start()
    {
        cameraHaldWidth = Screen.width / 2;
        targetPosition = ButtonGroup.anchoredPosition;

        Cursor.SetCursor(CursorImage, Vector2.zero, CursorMode.ForceSoftware);
    }

    // Update is called once per frame
    void Update()
    {
        // ���콺 ����
        Cursor.lockState = CursorLockMode.Confined;

        if(Input.GetKeyDown(KeyCode.LeftAlt)) isCursorActivated = true;
        if(Input.GetKeyUp(KeyCode.LeftAlt)) isCursorActivated = false;

        if(!isCursorActivated)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        // ��ư �����̵� ���
        ButtonGroup.anchoredPosition = Vector2.Lerp(ButtonGroup.anchoredPosition, targetPosition, SlideSpeed * Time.deltaTime);
        
        //if(isMenuOpen)
        //{
        //    ButtonGroup.anchoredPosition = Vector2.Lerp(ButtonGroup.anchoredPosition, targetPosition, SlideSpeed * Time.deltaTime);
        //}
        //else
        //{
        //    ButtonGroup.anchoredPosition = Vector2.Lerp(ButtonGroup.anchoredPosition, targetPosition, SlideSpeed * Time.deltaTime);
        //}
    }

    //�����̵� ��ư Ŭ�� �� �̺�Ʈ
    public void OnClickSlideBtn()
    {
        isButtonGroupPulled = !isButtonGroupPulled;

        if (isButtonGroupPulled)
        {
            // ��ư���� ���� ��ġ ���
                targetPosition = new Vector2(targetPosition.x - cameraHaldWidth, targetPosition.y);
        }
        else
        {
            // ��ư���� ����� ��ġ ���
                targetPosition = new Vector2(targetPosition.x + cameraHaldWidth, targetPosition.y);
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