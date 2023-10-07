using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;



// 저장하는 방법
// 1. 저장할데이터가 존재 
// 2. 데이터를 json으로 변환
// 3. json을 외부에 저장

// 불러오는 방법
// 1. 외부에 저장된 json을 불러옴
// 2. json를을 데이터로 변환
// 3. 불러온 데이터를 사용


//슬롯별 저장
public class PlayerData
{
    //이름, 레벨, 코인, 착용중인 무기
    public string name;
    public int level;
    public int coin; 
    public int item;
}


public class DataManager : MonoBehaviour
{
    //싱글톤
    public static DataManager instance;

    public string path; //저장 경로 
    public int selectedSlot;
    public PlayerData selectedSlotPlayerData = new PlayerData();

    
    public void Awake()
    {
        #region 싱글톤
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

        path = Application.persistentDataPath + "/save";
        Debug.Log(path);
    }

    public void SaveData() //저장
    {
        string data = JsonUtility.ToJson(selectedSlotPlayerData);
        File.WriteAllText(path + selectedSlot.ToString(), data); 
    }

    public void LoadData() //불러오기
    {
        string data = File.ReadAllText(path + selectedSlot.ToString());
        selectedSlotPlayerData = JsonUtility.FromJson<PlayerData>(data);
    }
    public void DataClear()
    {
        selectedSlot = -1;
        selectedSlotPlayerData = new PlayerData();
    }
    
}
