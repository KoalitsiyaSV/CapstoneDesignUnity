using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    Dictionary<int, string[]> dialogueData; 

    public TextMesh talkText;
    public GameObject gameObj;

    // Start is called before the first frame update
    void Awake()
    {
        dialogueData = new Dictionary<int, string[]>();

        GenerateDate();
    }

    private void GenerateDate()
    {
        dialogueData.Add(1000, new string[] {"���谡 ��忡 ���� ���� ȯ���մϴ�.",
                                                                   "������ � ���������� ������ ����Ͻó���?"});
    }

    public string GetDialogue(int id, int dialogueIndex)
    {
        return dialogueData[id][dialogueIndex];
    }
}
