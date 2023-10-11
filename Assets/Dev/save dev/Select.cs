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
    public GameObject creatNewSave; //����ִ� ������ �������� �ߴ�â
    public TextMeshProUGUI[] slotText;
    public TextMeshProUGUI newPlayerName;

    bool[] isSaveExist = new bool[3];


    void Start()
    {
        //���Ժ��� ����� �����Ͱ� �����ϴ��� �Ǵ�
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
                slotText[i].text = "�������";
            }
        }
        DataManager.instance.DataClear();
    }

    //������ 3���ε� ��� �˸��� ������ �ҷ��ðų�
    public void slot(int number) //���Կ� ���Լ�
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
        creatNewSave.gameObject.SetActive(true); //���ο� ���̺� ������ ����
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
