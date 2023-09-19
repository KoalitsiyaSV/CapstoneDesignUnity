using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;
using System;

public class Select : MonoBehaviour
{
    public GameObject creatNewSave; //비어있는 슬롯을 눌렀을때 뜨는창
    public TextMeshProUGUI[] slotText;
    public TextMeshProUGUI newPlayerName;

    bool[] isSaveExist = new bool[3];


    void Start()
    {
        //슬롯별로 저장된 데이터가 존재하는지 판단
        for (int i = 0; i < 3; i++)
        {
            if (File.Exists(DataManager.instance.path + $"{i}"))
            {
                isSaveExist[i] = true;
                DataManager.instance.selectedSlot = i;
                DataManager.instance.LoadData();
              
                slotText[i].text = DataManager.instance.selectedSlotPlayerData.name + DateTime.Now.ToString("  yy-MM-dd-HH-mm");
            }
            else
            {
                slotText[i].text = "비어있음";
            }
        }
        DataManager.instance.DataClear();
    }

    //슬롯이 3개인데 어떵게 알맞은 슬롯을 불러올거냐
    public void slot(int number) //슬롯에 들어갈함수
    {
        DataManager.instance.selectedSlot = number;

        
        if (isSaveExist[number]) 
        { 
            DataManager.instance.LoadData();
            StartGame();
        }
        else
        {
            CreatNewSaveData();
        }
        
    }

    public void CreatNewSaveData()
    {
        creatNewSave.gameObject.SetActive(true); //새로운 세이브 데이터 생성
    }

    public void StartGame()
    {
        if (!isSaveExist[DataManager.instance.selectedSlot])
        {
            DataManager.instance.selectedSlotPlayerData.name = newPlayerName.text;
            DataManager.instance.SaveData();
        }
        SceneManager.LoadScene("Village");
    }
}
