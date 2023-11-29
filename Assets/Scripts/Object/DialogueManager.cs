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
        dialogueData.Add(1000, new string[] {"모험가 길드에 오신 것을 환영합니다.",
                                                                   "오늘은 어떤 던전으로의 모험을 희망하시나요?"});
    }

    public string GetDialogue(int id, int dialogueIndex)
    {
        return dialogueData[id][dialogueIndex];
    }
}
