using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;



//½½·Ôº° ÀúÀå
public class DataManager : MonoBehaviour
{
    //½Ì±ÛÅæ
    public static DataManager instance;

    public string path; //ÀúÀå °æ·Î 
    public int selectedSlotNumber;
    public int deleteSlotNumber;
    public int latestSlotNumber;

    public PlayerData selectedSlotPlayerData = new PlayerData();

    public void Awake()
    {
        #region ½Ì±ÛÅæ
        if (instance == null)
        {
            instance = this;
        }
        else if( instance != this) 
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(instance.gameObject);
        #endregion

        path = Application.persistentDataPath;
        Debug.Log(path);
    }

    
    public void SaveData() // ÀúÀå
    {
        string data = JsonUtility.ToJson(selectedSlotPlayerData);
        File.WriteAllText(path + "/save" + selectedSlotNumber.ToString(), data);
    }

    public void LoadData() // ºÒ·¯¿À±â
    {
        string data = File.ReadAllText(path + "/save" + selectedSlotNumber.ToString());
        selectedSlotPlayerData = JsonUtility.FromJson<PlayerData>(data);
    }

    public void DeleteData() // µ¥ÀÌÅÍ »èÁ¦
    {
        File.Delete(path + "/save" + deleteSlotNumber.ToString());
    }

    public void DataClear() // µ¥ÀÌÅÍ ÃÊ±âÈ­
    {
        deleteSlotNumber = -1;
        selectedSlotNumber = -1;
        selectedSlotPlayerData = new PlayerData();
    }
}
