using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;



// �����ϴ� ���
// 1. �����ҵ����Ͱ� ���� 
// 2. �����͸� json���� ��ȯ
// 3. json�� �ܺο� ����

// �ҷ����� ���
// 1. �ܺο� ����� json�� �ҷ���
// 2. json���� �����ͷ� ��ȯ
// 3. �ҷ��� �����͸� ���


//���Ժ� ����
public class PlayerData
{
    //�̸�, ����, ����, �������� ����
    public string name;
    public int level;
    public int coin; 
    public int item;
}


public class DataManager : MonoBehaviour
{
    //�̱���
    public static DataManager instance;

    public string path; //���� ��� 
    public int selectedSlot;
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

        path = Application.persistentDataPath + "/save";
        Debug.Log(path);
    }

    public void SaveData() //����
    {
        string data = JsonUtility.ToJson(selectedSlotPlayerData);
        File.WriteAllText(path + selectedSlot.ToString(), data); 
    }

    public void LoadData() //�ҷ�����
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
