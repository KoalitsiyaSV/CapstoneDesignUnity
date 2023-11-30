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

    public GameObject createNewSaveUI; //비어있는 슬롯을 눌렀을때 뜨는창
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

    //3개의 슬롯중 알맞은 슬롯을 불러오기
    public void slot(int number) //슬롯에 들어갈함수
    {
        DataManager.instance.selectedSlotNumber = number;

        if (isSaveExist[number])
        { 
            StartGame();
        }
        else
        {
            createNewSaveUI.SetActive(true); //새로운 세이브 데이터 생성창 활성화
        }
    }

    public void StartGame()
    {
        DataManager.instance.latestSlotNumber = DataManager.instance.selectedSlotNumber;
        PlayerPrefs.SetInt("latestSlotNumber", DataManager.instance.latestSlotNumber); // 게임 시작시 가장 최근에 플레이한 데이터의 슬롯번호가 프리펩에 저장 
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
        //슬롯별로 저장된 데이터가 존재하는지 판단
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
                slotText[i].text = "새 게임";
            }
        }
    }

    public void OnClickDeleteButton(int number) //삭제 버튼에 들어갈 함수
    {
        if (isSaveExist[number])
        {
            DataManager.instance.deleteSlotNumber = number;   
            confirmDeleteUI.SetActive(true);
        } 
    }

    public void OnClickConfirmDeleteBtn() //데이터 삭제 확인   
    { 
        DataManager.instance.DeleteData();
        isSaveExist[DataManager.instance.deleteSlotNumber] = false;

        if(DataManager.instance.latestSlotNumber == DataManager.instance.deleteSlotNumber) //가장 최근에 진행한 데이터와 삭제한 데이터가 같으면
        {
            DataManager.instance.latestSlotNumber = -1; //초기화
        }

        confirmDeleteUI.SetActive(false);
        PrintSlot(); 
    }

    public void OnClickCancelDeleteButton() //데이터 삭제 취소
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
