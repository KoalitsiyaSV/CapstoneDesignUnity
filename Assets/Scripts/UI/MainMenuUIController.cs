using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;
using System;
using Unity.VisualScripting;

public class MainMenuUIController : MonoBehaviour
{
    private const int SLOT_COUNT = 3;

    public GameObject createNewSaveUI; //����ִ� ������ �������� �ߴ�â
    public GameObject confirmDeleteUI;
    public GameObject dataSlotUI;
    public GameObject creditUI;
    public GameObject noEmptySlotExplainUI;
    public GameObject noSaveDataExplainUI;
    

    
    public TextMeshProUGUI[] slotText;
    public TextMeshProUGUI newPlayerName;

    bool[] isSaveExist = new bool[SLOT_COUNT];
    


    void Start()
    {
        DataManager.instance.latestSlotNumber = PlayerPrefs.GetInt("latestSlotNumber");
        PrintSlot();
        DataManager.instance.DataClear();
    }
    
    private void Update()
    {
        Debug.Log("playerName : " + DataManager.instance.selectedSlotPlayerData.playerName);
        Debug.Log("selectedSlot : " + DataManager.instance.selectedSlotNumber);
        Debug.Log("latestSlotNumber : " + DataManager.instance.latestSlotNumber);
    }

    //3���� ������ �˸��� ������ �ҷ�����
    public void slot(int number) //���Կ� ���Լ�
    {
        DataManager.instance.selectedSlotNumber = number;

        if (isSaveExist[number])
        { 
            StartGame();
        }
        else
        {
            createNewSaveUI.SetActive(true); //���ο� ���̺� ������ ����â Ȱ��ȭ
        }
    }

    public void StartGame()
    {
        DataManager.instance.latestSlotNumber = DataManager.instance.selectedSlotNumber;
        PlayerPrefs.SetInt("latestSlotNumber", DataManager.instance.latestSlotNumber); // ���� ���۽� ���� �ֱٿ� �÷����� �������� ���Թ�ȣ�� �����鿡 ���� 
        DataManager.instance.LoadData();
        SceneManager.LoadScene("Village");
    }

    public void OnClickConfirmCreateNewSaveBtn()
    {
        if (!isSaveExist[DataManager.instance.selectedSlotNumber])
        {
            DataManager.instance.selectedSlotPlayerData.playerName = newPlayerName.text;
            DataManager.instance.SaveData();
        }
    }

    public void PrintSlot()
    {
        //���Ժ��� ����� �����Ͱ� �����ϴ��� �Ǵ�
        for (int i = 0; i < SLOT_COUNT; i++)
        {
            if (File.Exists(DataManager.instance.path + "/save" + $"{i}"))
            {
                isSaveExist[i] = true;
                DataManager.instance.selectedSlotNumber = i;
                DataManager.instance.LoadData();

                slotText[i].text = DataManager.instance.selectedSlotPlayerData.playerName + DataManager.instance.selectedSlotPlayerData.playerHP;
            }
            else
            {
                slotText[i].text = "�� ����";
            }
        }
    }

    public void OnClickDeleteButton(int number) //���� ��ư�� �� �Լ�
    {
        if (isSaveExist[number])
        {
            DataManager.instance.deleteSlotNumber = number;   
            confirmDeleteUI.SetActive(true);
        } 
    }

    public void OnClickConfirmDeleteBtn() //������ ���� Ȯ��   
    { 
        DataManager.instance.DeleteData();
        isSaveExist[DataManager.instance.deleteSlotNumber] = false;

        if(DataManager.instance.latestSlotNumber == DataManager.instance.deleteSlotNumber) //���� �ֱٿ� ������ �����Ϳ� ������ �����Ͱ� ������
        {
            DataManager.instance.latestSlotNumber = -1; //�ʱ�ȭ
        }

        confirmDeleteUI.SetActive(false);
        PrintSlot(); 
    }

    public void OnClickCancelDeleteButton() //������ ���� ���
    {
        DataManager.instance.deleteSlotNumber = -1;
        confirmDeleteUI.SetActive(false);
    }

    public void OnClickNewGameBtn()
    {
        if (isSaveExist[0] == false)
        {
            createNewSaveUI.SetActive(true);
            DataManager.instance.selectedSlotNumber = 0;
        }
        else if (isSaveExist[1] == false)
        {
            createNewSaveUI.SetActive(true);
            DataManager.instance.selectedSlotNumber = 1;
        }
        else if (isSaveExist[2] == false)
        {
            createNewSaveUI.SetActive(true);
            DataManager.instance.selectedSlotNumber = 2;
        }
        else
        {
            noEmptySlotExplainUI.SetActive(true);
        }
    }

    public void OnClickContinueBtn()
    {
        if (DataManager.instance.latestSlotNumber == -1)
        {
            noSaveDataExplainUI.SetActive(true);
        }
        else
        {
            DataManager.instance.selectedSlotNumber = DataManager.instance.latestSlotNumber;
            DataManager.instance.LoadData();
            SceneManager.LoadScene("Village");
        }
       
    }

    public void OnClickLoadGameBtn()
    {
        dataSlotUI.SetActive(true);
    }

    public void OnClickCreditBtn()
    {
        creditUI.SetActive(true);
    }

    public void OnClickCloseBtn()
    {
        dataSlotUI.SetActive(false);
        creditUI.SetActive(false);
        createNewSaveUI.SetActive(false);
        noEmptySlotExplainUI.SetActive(false);
        noSaveDataExplainUI.SetActive(false);
    }

    public void OnClickExitBtn()
    {
        Application.Quit();
    }

}
