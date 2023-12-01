using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;



//���Ժ� ����
public class DataManager : MonoBehaviour
{
    //�̱���
    public static DataManager instance;

    public string path; //���� ��� 
    public int selectedSlotNumber;
    public int deleteSlotNumber;
    public int latestSlotNumber;

    public PlayerData selectedSlotPlayerData = new PlayerData();

    public void Awake()
    {
        #region �̱���
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

    
    public void SaveData() // ����
    {
        string data = JsonUtility.ToJson(selectedSlotPlayerData);
        File.WriteAllText(path + "/save" + selectedSlotNumber.ToString(), data);
    }

    public void LoadData() // �ҷ�����
    {
        string data = File.ReadAllText(path + "/save" + selectedSlotNumber.ToString());
        selectedSlotPlayerData = JsonUtility.FromJson<PlayerData>(data);
    }

    public void DeleteData() // ������ ����
    {
        File.Delete(path + "/save" + deleteSlotNumber.ToString());
    }

    public void DataClear() // ������ �ʱ�ȭ
    {
        deleteSlotNumber = -1;
        selectedSlotNumber = -1;
        selectedSlotPlayerData = new PlayerData();
    }
}
